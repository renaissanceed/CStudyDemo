# CStudyDemo

> C# 控制台游戏合集 ｜ .NET 8 ｜ 4 个独立工程 ｜ 约 3500 行

四个不依赖任何引擎与图形库的纯 C# 控制台游戏。做这批项目的目的是**在没有引擎兜底的情况下**把语言基础与程序设计能力练扎实：帧循环要自己写、渲染要自己画、输入要自己轮询、场景切换要自己管——这些能力与具体引擎无关，换到 Unity 里同样成立。

| 工程 | 玩法 | 代码量 | 主要技术点 |
|---|---|---|---|
| 俄罗斯方块 | 方块下落、变形、消行计分 | 11 类 / 1217 行 | 多线程输入 + `lock`、运算符重载、索引器、字典形态表 |
| 贪吃蛇 | 吃食变长、撞墙撞自己结束 | 16 类 / 817 行 | 接口 + 抽象类、数组预分配、非阻塞输入、难度曲线 |
| 飞行棋 | 多人回合制、格子事件 | 988 行单文件 | 枚举 + 结构体、二维坐标换算、回合状态流转 |
| 拯救公主 | 多关卡文字冒险 | 514 行单文件 | 关卡状态机、控制台逐字符绘制、方向键交互 |

## 一、俄罗斯方块（进阶）

四个项目里工程量最大、语言特性用得最深的一个。

### 双线程模型

主线程负责渲染与下落，独立线程负责读键，两者通过 `lock` 互斥访问方块状态：

```csharp
// 主线程
lock (worker) {
    map.Draw(); worker.Draw();
    if (worker.CanAutoMove(map)) worker.AutoMove();
    DrawScore();
}
Thread.Sleep(150);          // 锁外休眠，否则输入会被阻塞 150ms

// 输入线程
inputThread = new Thread(CheakInputThread);
inputThread.IsBackground = true;
```

### 运算符重载

`Postion` 是 `struct`，重载了 `==` / `!=` / `+`：

```csharp
public static bool operator ==(Postion p1, Postion p2)
public static Postion operator +(Postion p1, Postion p2)
```

重载后碰撞检测从逐字段比对变成一行表达式：`if (nowPos == map.dynamicWalls[j].pos)`。

### 索引器与形态表

```csharp
public Postion[] this[int index] { get { ... } }
```

7 种方块共 25 套旋转偏移，全部存进 `Dictionary<E_DrawType, BlockInfo>`。变形就是改索引（`--index` / `++index`，越界回绕），再按 `blocks[0].pos + 偏移[i]` 重算三个附属格子的坐标——把「形状」定义成数据，而不是 21 个类。

### 消行算法

`Map.recordInfo` 是长度为 32 的计数数组，每落地一格给对应行 `+1`：

```csharp
recordInfo[h - pos.y - 1] += 1;      // 行号从底部算，0 是最底一行
if (recordInfo[i] == w) { ... }      // 数到 23（行容量）即判定满行
```

消行后：删除该行方块 → 上方方块 `pos.y++` → 计数数组整体前移 → **递归**再查一次（处理一次消多行）。用计数法而不是遍历 23×32 的二维网格，代码量与开销都小得多。

### 架构

`IDraw`（统一绘制）与 `ISceneUpdate`（统一帧更新）两个接口定义契约，`Game.nowScene` 用接口类型持有当前场景，`ChangeScene(E_SceneType)` 按枚举分派实例化，主循环只调 `nowScene.Update()`。

## 二、贪吃蛇（核心）

按 6 个迭代阶段组织代码，体现从「能跑」到「可维护」的重构过程。

### 数组代替 List

蛇身用**预分配数组 + 计数**管理，而非 `List<T>`：

```csharp
bobys = new SnakeBoby[200];   // 一次性分配
nowNum = 1;                   // 记录用到第几个
```

选择理由：地图容量可预估（蛇最长 200 格）、数组是连续内存访问更快、移动逻辑本质就是按下标搬运。代价是长度写死，超出会 `IndexOutOfRange`——这正是 `List<T>` 要解决的问题，两种方案的取舍写在这里。

### 移动算法

```csharp
for (int i = nowNum - 1; i > 0; i--)
    bobys[i].pos = bobys[i - 1].pos;   // 必须从尾到头，否则整条蛇会挤在一起
switch (dir) { case up: --bobys[0].pos.y; ... }
```

### 继承与多态

抽象出 `GameObject` 基类（持 `pos` + 虚方法 `Draw()`），`SnakeBoby`、`Wall`、`Food` 各自 `override Draw()` 画出不同字符与颜色；场景层用 `BeginOrEndBaseScene` 抽象类复用开始/结束菜单的绘制与按键逻辑，只留 `EnterJDoSomthing()` 给子类实现。

### 输入与难度

```csharp
if (Console.KeyAvailable) { ... Console.ReadKey(true) ... }   // 非阻塞，不卡帧
if (updateIndex % Game.level == 0) { /* 逻辑推进 */ }          // 难度越高逻辑帧越快
```

## 三、飞行棋（基础）

988 行单文件，按 `region` 划分为格子、玩家、骰子、渲染四块。

- `E_Gz_Type` 定义五种格子：普通、炸弹、暂停、随机隧道、终点，逐格判定触发对应效果（后退、跳过回合、传送到镜像格）；
- 自定义 `struct Vector2` 承载控制台坐标；
- 蛇形棋盘的坐标换算用二维数组与定位算法完成，玩家落点由骰子点数累加得出；
- `E_PlayerColor` / `E_playerType` 驱动多玩家轮转。

这个项目是四个里唯一没做 OOP 分层的，全部逻辑收在 `Program.Main` 里——保留它正是为了对比「有分层」和「无分层」的差别。

## 四、拯救公主（入门）

514 行单文件的多关卡文字冒险。

- `switch (NowLevel)` 组织关卡流转，每关一个内层循环负责交互与判定；
- 菜单用 `Console.SetCursorPosition` 逐字符定位绘制，`ForegroundColor` 高亮当前选中项，`CursorVisible = false` 消除闪烁；
- 方向键切换选项、回车确认，进入关卡后按提示输入指令推进剧情。

## 运行方式

四个工程互相独立，各自用 `dotnet run` 启动：

```bash
cd 俄罗斯方块/俄罗斯方块 && dotnet run
cd 贪吃蛇/项目          && dotnet run
cd 飞行棋小项目/项目     && dotnet run
cd 拯救公主小项目/NO.1   && dotnet run
```

目标框架 .NET 8，无第三方依赖。控制台窗口尺寸由代码在启动时设置（`Console.SetWindowSize` / `SetBufferSize`），若终端不支持调整会抛异常，建议用 Windows Terminal 运行。

## 目录结构

```
俄罗斯方块/俄罗斯方块/Lesson1~Lesson5    场景框架 → 绘制对象 → 地图 → 形态表 → 方块控制
贪吃蛇/项目/Lesson1~Lesson6              场景框架 → 场景实现 → 对象建模 → 玩法对象 → 地图 → 蛇
飞行棋小项目/项目/Program.cs
拯救公主小项目/NO.1/Program.cs
```

`LessonN` 目录是逐阶段的代码留存，可以看到同一功能从雏形到成型的演进过程，不是最终版本的简单分文件。

## 说明

- 全部为个人练习项目，不含引擎与第三方库；
- 控制台渲染基于 `Console.SetCursorPosition` 定点重绘，无帧缓冲，因此每次移动前需先擦除旧位置（写空格）再画新位置，否则会留下残影。
