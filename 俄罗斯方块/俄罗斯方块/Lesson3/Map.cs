using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    internal class Map : IDraw
    {
        //固定墙壁
        private List<DrawObject> walls = new List<DrawObject>();
        //动态墙壁
        public List<DrawObject> dynamicWalls = new List<DrawObject>();

        public int w;
        public int h;

        private GameScene nowGameScene;

        //记录每一行有多少个小方块的容器
        private int[] recordInfo;

        private bool isDisplayScore = true;

        //初始化固定墙壁
        public Map(GameScene gameScene) 
        {
            this.nowGameScene = gameScene;

            h = Game.h - 3;

            //动态墙壁的宽容量
            w = 0;

            for (int i = 0; i < Game.w; i+=2)
            {
                walls.Add(new DrawObject(E_DrawType.Wall , i , h));
                ++w;
            }

            w = w - 2;
            for (int i = 0; i < h; i ++)
            {
                walls.Add(new DrawObject(E_DrawType.Wall, 0, i));
                walls.Add(new DrawObject(E_DrawType.Wall, Game.w - 2, i));
            }

            //每行记录器，默认为0
            recordInfo = new int[h];
        }

        //清除动态墙壁
        public void ClearDraw()
        {
            //绘制动态墙壁
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].ClearDraw();
            }
        }

        public void Draw()
        {
            //绘制固定墙壁
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].Draw();
            }

            //绘制动态墙壁
            for (int i = 0; i < dynamicWalls.Count; i++)
            {
                dynamicWalls[i].Draw();
            }
        }

        /// <summary>
        /// 提供外部添加动态方块的函数
        /// </summary>
        public void AddWalls(List<DrawObject> walls)
        {
            for (int i = 0; i < walls.Count; i++)
            {
                //传递方块进来时 把类型改成墙壁类型
                walls[i].ChangType(E_DrawType.DynamicWalls);
                dynamicWalls.Add(walls[i]);

                //是否顶满了，判断是否需要结束
                if (walls[i].pos.y <= 0)
                {
                    //关闭输入线程
                    this.nowGameScene.StopThread();
                    //场景切换到结束界面
                    Game.ChangeScene(E_SceneType.End);
                    return;
                }

                //进行动态墙壁的计数
                recordInfo[h - walls[i].pos.y - 1] += 1;
            }
            //先擦除
            ClearDraw();

            //检测是否移除
            CheckClear();

            //再画
            Draw();
        }

        //检测是否需要去掉一行
        public void CheckClear()
        {
            //记录待删除的方块
            List<DrawObject> delList = new List<DrawObject>();

            //遍历每一行是否满了
            for (int i = 0; i < recordInfo.Length; i++)
            {
                //如果满了，就清除这一行的所有方块
                if (recordInfo[i] == w)
                {
                    if (isDisplayScore)
                    {
                        GameScene.score = 10;
                        isDisplayScore = false;
                    }
                    else if (!isDisplayScore)
                    {
                        GameScene.score += 10;
                    }

                    //移除这一行的方块
                    for(int j = 0; j< dynamicWalls.Count;j++)
                    {
                        if(i == h - dynamicWalls[j].pos.y - 1)
                        {
                            delList.Add(dynamicWalls[j]);
                        }
                        //把上一行的方块移动到下一行
                        //如果当前的位置，在这行上面，就该把小方块下移
                        else if (h - dynamicWalls[j].pos.y - 1 > i)
                        {
                            dynamicWalls[j].pos.y++;
                        }
                    }

                    //移除待删除的小方块
                    for (int j = 0; j < delList.Count; j++)
                    {
                        dynamicWalls.Remove(delList[j]);
                    }

                    //记录方块数量的数组迁移
                    for (int j = i;j < recordInfo.Length - 1; j++)
                    {
                        recordInfo[j] = recordInfo[j + 1];
                    }

                    //清空数组最后一行
                    recordInfo[recordInfo.Length - 1] = 0;

                    //再次检测一行满没满，防止出现多行满了，不清除的情况
                    CheckClear();
                    break;
                }
            }
        }
    }
}
