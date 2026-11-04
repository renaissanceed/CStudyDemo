using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;

namespace 项目
{
    #region 格子相关

    #region 格子枚举
    /// <summary>
    /// 格子类型 枚举
    /// </summary>
    enum E_Gz_Type 
    {
        /// <summary>
        /// 普通格子
        /// </summary>
        Noraml,
        /// <summary>
        /// 炸弹格子
        /// </summary>
        Boom,
        /// <summary>
        /// 暂停格子
        /// </summary>
        pause,
        /// <summary>
        /// 随机格子
        /// </summary>
        Tunnel,
        /// <summary>
        /// 终点格子
        /// </summary>
        Finish,

    }
    #endregion

    #region 生成位置
    /// <summary>
    /// 位置结构体
    /// </summary>
    struct Vector2 
    {
        public int x;
        public int y;

        public Vector2(int x, int y) 
        {
            this.y = y;
            this.x = x;
        }
    }
    #endregion

    #region 格子结构体
    /// <summary>
    /// 格子类型结构体
    /// </summary>
    struct Gz 
    {
        //格子类型
        public E_Gz_Type type;
        //格子位置
        public Vector2 pos;

        //初始化
        public Gz(int x,int y, E_Gz_Type type) 
        { 
            this .type = type;
            pos.x = x;
            pos.y = y;
        }
        #region 画格子函数
        public void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            switch (type)
            {
                //普通格子
                case E_Gz_Type.Noraml:
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("□");
                    break;
                //炸弹格子
                case E_Gz_Type.Boom:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("●");
                    break;
                //暂停格子
                case E_Gz_Type.pause:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("‖");
                    break;
                //随机格子
                case E_Gz_Type.Tunnel:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("¤");
                    break;
                //终点格子
                case E_Gz_Type.Finish:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("∏");
                    break;
            }
            #endregion
        }
    }
    #endregion

    #endregion

    #region 地图相关

    #region 画主地图
    struct Map
    {
        public Gz[] grids;
        public int[] tnnelNum = new int[20];
        public int j = 0;

        public Map(int x,int y,int num)
        {
            grids=new Gz[num];
            //随机数
            Random random = new Random();
            int randomNum;
            //x的步长
            int xStep = 2;
            int indexX = 0, indexY = 0;

            for (int i = 0; i < num; i++)
            {
                //定义X，Y的变化量
                //初始化格子类型
                randomNum= random.Next(0,101); 
                //设置格子类型，80%的概率为普通格子并且首尾两个格子必须为普通格子
                if(randomNum < 80 || i == 0 || i == num-1)
                {
                    grids[i].type = E_Gz_Type.Noraml;
                }
                //9%概率为炸弹格子
                else if (randomNum >= 80 && randomNum < 88)
                {
                    grids[i].type = E_Gz_Type.Boom;
                }
                //6%概率为暂停格子
                else if (randomNum  >= 88 && randomNum < 95)
                {
                    grids[i].type = E_Gz_Type.pause;
                }
                //5%概率为随机格子
                else 
                {
                    //记录随机格子在地图上的格子位置
                    tnnelNum[j] = i;
                    grids[i].type = E_Gz_Type.Tunnel;
                    j++;
                }

                grids[i].pos=new Vector2(x,y);

                if(indexX ==10)
                {
                    y += 1;
                    //记录Y轴累计的格子
                    ++indexY;
                    //重置累加次数
                    if (indexY == 2)
                    { 
                        indexX = 0;
                        indexY = 0;
                        //反向步长
                        xStep=  -xStep;
                    }
                       
                }
                else
                {   
                     x += xStep;
                    //记录Y轴累计的格子
                    ++indexX;
                }

            }
        }


        public void Draw()
        {
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].Draw();
            }
        }

    }
    #endregion

    #endregion

    #region 玩家相关

    #region 玩家类型枚举
    /// <summary>
    /// 玩家类型枚举
    /// </summary>
    enum E_playerType 
    {
        /// <summary>
        /// 玩家1
        /// </summary>
        player1,
        /// <summary>
        /// 玩家2
        /// </summary>
        player2,

    }
    #endregion

    #region 玩家结构体
    struct Player
    {
        //玩家类型
        public E_playerType type;
        //当前格子大地图的索引
        public int nowIndex;
        //暂停标识
        public bool  isPause;

        public Player(int index, E_playerType type)
        {
            nowIndex = index;
            this.type = type;
            isPause = false;
        }

        #region 绘制玩家在主地图
        public void Draw(Map mapInfo,int color, E_PlayerColor e_PlayerColor)
        {
            //得到位置信息
            Gz grid = mapInfo.grids[nowIndex];
            //设置位置
            Console.SetCursorPosition(grid.pos.x,grid.pos.y);
            //画 设置颜色 设置图标
            switch (type)
            {
                case E_playerType.player1:
                    DrawColor(e_PlayerColor, color);
                    Console.Write("★");
                    break;
                case E_playerType.player2:
                    DrawColor(e_PlayerColor, color);
                    Console.Write("▲");
                    break;
            }

        }
        #endregion

        #region 绘制玩家选择颜色
        public void DrawColor(E_PlayerColor e_PlayerColor,int color = 0)
        {
            e_PlayerColor = (E_PlayerColor)color;
            switch (e_PlayerColor)
            {
                case E_PlayerColor.white:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case E_PlayerColor.yellow:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case E_PlayerColor.Magenta:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case E_PlayerColor.red:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_PlayerColor.blue:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_PlayerColor.green:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }
        #endregion

    }
    #endregion

    #endregion

    internal class Program
    {
        //设置全局常量长和宽
        const int w = 50, k = 30;
        static void Main(string[] args)
        {
            #region 控制台初始化
            //设置长宽
            ConsoleInit(w, k);
            //设置初始场景
            E_SceneType nowSceneType = E_SceneType.begin;
            //设置玩家默认颜色为白色
            E_PlayerColor playerColor = E_PlayerColor.white;
            //玩家选择的颜色参数
            int colorOne = 0, colorTwo = 0;
            #endregion

            #region 关卡转换
            while (true)
            {
                switch (nowSceneType)
                {
                    case E_SceneType.begin:
                        //开始场景
                        Console.Clear();
                        GameBegin(ref nowSceneType);
                        break;
                    case E_SceneType.instruction:
                        //说明场景
                        Console.Clear();
                        GameInstruction(ref colorOne, ref colorTwo, ref nowSceneType);
                        break;
                    case E_SceneType.game:
                        //游戏场景
                        Console.Clear();
                        GameScene(colorOne, colorTwo, ref nowSceneType, playerColor);
                        break;
                    case E_SceneType.end:
                        //游戏结束
                        GameOver(ref nowSceneType);
                        Console.Clear();
                        break;
                }
            }
            #endregion

        }
        #region 控制台初始化
        static void ConsoleInit(int w, int k)
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w, k);
            Console.SetBufferSize(w, k);
        }
        #endregion

        #region 开始场景
        static void GameBegin(ref E_SceneType nowSceneType)
        {
            Console.SetCursorPosition(w / 2 - 5, 8);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.Write("双人飞行棋");
            bool IsQuitBegin = false;
            //当前选项编号
            int nowIndex = 0;
            while (true)
            {
                Console.SetCursorPosition(w / 2 - 4, 13);
                Console.ForegroundColor = nowIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("开始游戏");

                Console.SetCursorPosition(w / 2 - 4, 15);
                Console.ForegroundColor = nowIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        --nowIndex;
                        if (nowIndex < 0)
                        {
                            nowIndex = 0;
                        }
                        break;

                    case ConsoleKey.S:
                        ++nowIndex;
                        if (nowIndex > 0)
                        {
                            nowIndex = 1;
                        }
                        break;

                    case ConsoleKey.J:
                        if (nowIndex == 0)
                        {
                            //进入游戏
                            nowSceneType = E_SceneType.instruction;
                            IsQuitBegin = true;
                        }
                        else
                        {
                            //退出游戏
                            Environment.Exit(0);
                        }
                        break;

                }
                if (IsQuitBegin)
                    break;
            }
        }
        #endregion

        #region 说明书场景
        static void GameInstruction(ref int colorOne, ref int colorTwo, ref E_SceneType nowSceneType)
        {
            #region 颜色输出
            static void color()
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("白色0 ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("黄色1 ");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("紫色2 ");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("红色3 ");

                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("蓝色4 ");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("绿色5 ");
            }
            #endregion

            #region 游戏提示
            Console.SetCursorPosition(w - 1, 5);
            Console.ForegroundColor = ConsoleColor.White; ;

            Console.WriteLine("游戏为双人游戏，分为玩家1和玩家2\n玩家1是★，玩家2是▲\n");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("玩家1 按下J操控，玩家2 按下小键盘数字键0操控\n以下是玩家选择颜色\n");

            Console.ForegroundColor = ConsoleColor.Red;
            color();
            Console.ForegroundColor = ConsoleColor.White;
            #endregion

            bool isColor = true;//判断1号玩家输入情况
            int iscatch = 0;//判断玩家输错的次数

            while (true)
            {
                try
                {
                    #region 1号玩家输入
                    if (isColor)//判断1号玩家是否已经输入
                    {
                        Console.CursorVisible = true;
                        if (iscatch % 3 == 0)//每错误三次输入提示
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("首先是1号玩家★，选择输入对应的颜色数字");
                        }

                        colorOne = int.Parse(Console.ReadLine());//存入对应的颜色数字

                        if (colorOne < 0 || colorOne > 5)//判断是否在0-5之外
                        {
                            iscatch++;//累加错误次数
                            throw new ArgumentOutOfRangeException();//直接跳转到catch，输入错误信息
                        }
                        else if (colorOne > 0 && colorOne < 5)
                            isColor = false;
                    }
                    #endregion
                    #region 2号玩家输入
                    if (iscatch % 3 == 0)//每错误三次输入提示
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("首先是2号玩家▲，选择输入对应的颜色数字");
                    }

                    colorTwo = int.Parse(Console.ReadLine());//存入对应的颜色数字

                    if (colorTwo < 0 || colorTwo > 5)//判断是否在0-5之外
                    {
                        iscatch++;//累加错误次数
                        throw new ArgumentOutOfRangeException();//直接跳转到catch，输入错误信息
                    }
                    #endregion

                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine("\n按下任意键继续_\n");
                    Console.ReadKey(true);
                    nowSceneType = E_SceneType.game;
                    //切换游戏关卡
                    break;
                }
                catch
                {
                    bool is_selectcolor = true;//暂停提醒
                    iscatch++;
                    if (iscatch % 3 == 0) //判断输出次数，防止看不到提示
                    {
                        Console.Clear();
                        color();
                        //重新输出颜色提示
                        Console.ForegroundColor = ConsoleColor.White;
                        is_selectcolor = false;
                    }

                    if (is_selectcolor)//每3次错误输入，暂停一次提醒
                    {
                        Console.WriteLine("\n请输入0~5的数字\n");
                        is_selectcolor = true;
                    }
                }
            }

        }
        #endregion

        #region 游戏场景
        static void GameScene(int colorOne, int colorTwo, ref E_SceneType nowSenceType, E_PlayerColor e_PlayerColor = E_PlayerColor.white)
        {
            
            RedWall(colorOne, colorTwo, e_PlayerColor);
            //绘制边缘红墙 

            Map map = new Map(14, 2, 100);
            map.Draw();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(11, 2);
            Console.Write("起");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(23, 18);
            Console.Write("终");
            //绘制地图

            Player player1 = new Player(0, E_playerType.player1);
            Player player2 = new Player(0, E_playerType.player2);
            DrawPlayer(player1, player2, map, colorOne, colorTwo, e_PlayerColor);
            //绘制玩家

            bool isGameOver = false;
            //游戏结束标志

            bool isPlayerRange = true;
            //玩家先后扔色子顺序

            Console.SetCursorPosition(2, k - 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("游戏开始，玩家1按J先扔色子");

            #region 扔色子逻辑
            while (true)
            { 
                switch (Console.ReadKey(true).Key)
                {
                    #region 玩家1输入逻辑
                    case ConsoleKey.J:
                        if (isPlayerRange)
                        //玩家1扔色子
                        {
                            //扔色子逻辑
                            isGameOver = RandomMove(w, k, colorOne, colorTwo,
                                ref player1, map, e_PlayerColor);
                            //绘制地图
                            map.Draw();
                            //绘制玩家
                            DrawPlayer(player1, player2, map,
                                colorOne, colorTwo, e_PlayerColor);
                            //玩家先后扔色子
                            isPlayerRange = false;
                        }
                        break;
                    #endregion

                    #region 玩家2输入逻辑
                    case ConsoleKey.NumPad0:
                        //玩家2扔色子
                        if (isPlayerRange == false)
                        //玩家2后扔色子
                        {
                            //扔色子逻辑
                            isGameOver = RandomMove(w, k, colorOne, colorTwo,
                                ref player2, map, e_PlayerColor);
                            //绘制地图
                            map.Draw();
                            //绘制玩家
                            DrawPlayer(player1, player2, map,
                                colorOne, colorTwo, e_PlayerColor);
                            //玩家先后扔色子
                            isPlayerRange = true;
                        }
                        break;
                    #endregion
                }
                if (isGameOver)
                {
                    //卡住程序
                    Console.ReadKey(true);
                    //改变关卡
                    nowSenceType = E_SceneType.end;
                    //跳出循环
                    break;
                }

            }
                    #endregion  
        }
        #endregion

        #region 结束场景
        static void GameOver(ref E_SceneType nowSceneType)
        {
            Console.Clear();
            Console.SetCursorPosition(w / 2 - 4, 8);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("游戏结束");
            bool IsQuitBegin = false;
            //当前选项编号
            int nowIndex = 0;
            while (true)
            {
                Console.SetCursorPosition(w / 2 - 5, 13);
                Console.ForegroundColor = nowIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("回到主菜单");

                Console.SetCursorPosition(w / 2 - 4, 15);
                Console.ForegroundColor = nowIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
                Console.Write("退出游戏");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        --nowIndex;
                        if (nowIndex < 0)
                        {
                            nowIndex = 0;
                        }
                        break;

                    case ConsoleKey.S:
                        ++nowIndex;
                        if (nowIndex > 0)
                        {
                            nowIndex = 1;
                        }
                        break;

                    case ConsoleKey.J:
                        if (nowIndex == 0)
                        {
                            //回到主菜单
                            nowSceneType = E_SceneType.begin;
                            IsQuitBegin = true;
                        }
                        else
                        {
                            //退出游戏
                            Environment.Exit(0);
                        }
                        break;

                }
                if (IsQuitBegin)
                    break;
            }
        }
        #endregion

        #region 绘制红墙和提示信息
        static void RedWall(int colorOne, int colorTwo, E_PlayerColor e_PlayerColor)
        {

            Console.ForegroundColor = ConsoleColor.Red;
            //画横着的四行
            for (int i = 0; i < w; i += 2)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                //顶上的墙
                Console.SetCursorPosition(i, k - 1);
                Console.Write("■");
                //底部的墙
                Console.SetCursorPosition(i, k - 10);
                Console.Write("■");
                //第二行墙
                Console.SetCursorPosition(i, k - 5);
                Console.Write("■");
                //第三行的墙
            }
            //画剩下竖着的两行
            for (int i = 0; i < k; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                //左边竖着的墙
                Console.SetCursorPosition(w - 2, i);
                Console.Write("■");
                //右边竖着的墙
            }
            //文字信息
            Console.SetCursorPosition(2, k - 9);
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("□:普通格子");

            Console.SetCursorPosition(2, k - 8);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("‖:暂停，一回合不动\t");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("●:炸弹，倒退5格");

            Console.SetCursorPosition(2, k - 7);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("¤:时空隧道，随机（倒退，暂停，换位置）");

            Console.SetCursorPosition(2, k - 6);
            #region 同步玩家选择颜色
            e_PlayerColor = (E_PlayerColor)colorOne;
            for (int i = 0; i < 3; i++)
            {
                if (i == 1)
                    e_PlayerColor = (E_PlayerColor)colorTwo;

                switch (e_PlayerColor)
                {
                    case E_PlayerColor.white:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case E_PlayerColor.yellow:
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        break;
                    case E_PlayerColor.Magenta:
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        break;
                    case E_PlayerColor.red:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case E_PlayerColor.blue:
                        Console.ForegroundColor = ConsoleColor.Blue;
                        break;
                    case E_PlayerColor.green:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                }

                switch (i)
                {
                    case 0:
                        Console.Write("★:玩家1   ");
                        break;
                    case 1:
                        Console.Write("▲:玩家2    ");
                        break;
                }

            }
            #endregion
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("◎:玩家1和玩家2重合");

        }
        #endregion

        #region 绘制玩家
        static void DrawPlayer(Player player1, Player player2, Map map, int colorOne, int colorTwo, E_PlayerColor e_PlayerColor)
        {
            //重合时
            if (player1.nowIndex == player2.nowIndex)
            {
                Gz gz = map.grids[player1.nowIndex];
                Console.SetCursorPosition(gz.pos.x, gz.pos.y);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("◎");
            }
            //不重合时
            else
            {
                player1.Draw(map, colorOne, e_PlayerColor);
                player2.Draw(map, colorTwo, e_PlayerColor);
            }
        }
        #endregion

        #region 大地图扔色子

        static void ClearInfo()
        {
            //擦除之前信息
            for (int i = 2; i <= 4; i++)
            {
                Console.SetCursorPosition(2, k - i);
                Console.Write("                                  ");
            }
        }
        /// <summary>
        /// 扔色子函数
        /// </summary>
        /// <param name="w">宽</param>
        /// <param name="h">高</param>
        /// <param name="p">玩家对象</param>
        /// <param name="map">大地图</param>
        /// <param name="miniMap">小地图</param>
        /// <returns>默认没有到终点</returns>
        static bool RandomMove(int w, int h, int ColorOne, int ColorTwo, ref Player p, Map map, E_PlayerColor e_PlayerColor)
        {

            //擦除之前信息
            ClearInfo();

            //将提示信息和玩家选择的颜色同步
            if (p.type == E_playerType.player1)
            {
                p.DrawColor(e_PlayerColor, ColorOne);
            }
            else
            {
                p.DrawColor(e_PlayerColor, ColorTwo);
            }

            //玩家暂停
            if (p.isPause)
            {
                Console.SetCursorPosition(2, k - 4);
                Console.Write("处于暂停点，{0}需要暂停一回合",
                    p.type == E_playerType.player1 ? "★" : "▲");
                Console.SetCursorPosition(2, k - 3);
                Console.Write("{0}请按继续游戏",
                    p.type == E_playerType.player2 ? "★" : "▲");
                p.isPause = false;
                return false;
            }
            //扔1-6的数字，加上位移的格子
            Random r = new Random();
            int randomNum = r.Next(1, 7);
            p.nowIndex += randomNum;

            //打印扔出的点数的信息
            Console.SetCursorPosition(2, k - 4);
            Console.Write("{0}扔出点数为：{1}",
                p.type == E_playerType.player1 ? "★" : "▲", randomNum);

            //判断是否到达终点
            if (p.nowIndex >= map.grids.Length - 1)
            {
                p.nowIndex = map.grids.Length - 1;

                ClearInfo();
                Console.SetCursorPosition(2, k - 4);
                if (p.type == E_playerType.player1)
                {
                    Console.Write("游戏结束！玩家1胜利");
                }
                else
                {
                    Console.Write("游戏结束！玩家2胜利");
                }
                Console.SetCursorPosition(2, k - 3);
                Console.Write("按任意键结束游戏");
                return true;
            }
            else
            {
                Gz grid = map.grids[p.nowIndex];
                switch (grid.type)
                {
                    case E_Gz_Type.Noraml:
                        //普通格子不用处理 
                        Console.SetCursorPosition(2, k - 3);
                        Console.Write("你到达了一个安全位置");
                        Console.SetCursorPosition(2, k - 2);
                        Console.Write("{0}请按继续游戏",
                            p.type == E_playerType.player2 ? "★" : "▲");
                        break;
                    case E_Gz_Type.Boom:
                        //炸弹格子退格
                        p.nowIndex -= 5;
                        //防止溢出到起点还小的值
                        if (p.nowIndex < 0)
                        {
                            p.nowIndex = 0;
                        }
                        Console.SetCursorPosition(2, k - 3);
                        Console.Write("你踩到了炸弹，退后5格");
                        Console.SetCursorPosition(2, k - 2);
                        Console.Write("{0}请继续游戏",
                            p.type == E_playerType.player2 ? "★" : "▲");
                        break;
                    case E_Gz_Type.pause:
                        //暂停格子原地不动一回合
                        //改成true表明下一回暂停不动
                        p.isPause = true;
                        Console.SetCursorPosition(2, k - 3);
                        Console.Write("你到达了暂停点");
                        Console.SetCursorPosition(2, k - 2);
                        Console.Write("{0}请按继续游戏",
                            p.type == E_playerType.player2 ? "★" : "▲");
                        break;
                    case E_Gz_Type.Tunnel:
                        //随机格子，随机事件
                        randomNum = r.Next(1, 91);
                        if (randomNum < 40)
                        {
                            //40%随机退后三格
                            p.nowIndex -= 3;
                            if (p.nowIndex < 0)
                            {
                                p.nowIndex = 0;
                            }
                            Console.SetCursorPosition(2, k - 3);
                            Console.Write("你踩到了随机格子，退后3格");

                        }
                        else if (randomNum <= 40)
                        //40%随机暂停
                        {
                            p.isPause = true;
                            Console.SetCursorPosition(2, k - 3);
                            Console.Write("你踩到了随机格子，暂停一回合");
                        }
                        else
                        {
                            //20%触发随机换全图位置
                            randomNum = r.Next(0, map.j);
                            p.nowIndex = map.tnnelNum[randomNum];

                            Console.SetCursorPosition(2, k - 3);
                            Console.Write("你踩到了随机格子，随机换位");

                        }
                        Console.SetCursorPosition(2, k - 2);
                        Console.Write("{0}请继续游戏",
                            p.type == E_playerType.player2 ? "★" : "▲");
                        break;
                }
            }
            return false;
        }
        #endregion

    }

    #region 场景转换枚举
    /// <summary>
    /// 游戏场景转换
    /// </summary>
    enum E_SceneType
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        begin,
        /// <summary>
        /// 说明场景
        /// </summary>
        instruction,
        /// <summary>
        /// 游戏场景
        /// </summary>
        game,
        /// <summary>
        /// 游戏结束
        /// </summary>
        end,
 
    }
    #endregion

    #region 玩家颜色选择枚举
    /// <summary>
    /// 玩家颜色选择
    /// </summary>
    enum E_PlayerColor
    {
    white,
    yellow,
    Magenta,
    red,
    blue,
    green,
    }
    #endregion

}
