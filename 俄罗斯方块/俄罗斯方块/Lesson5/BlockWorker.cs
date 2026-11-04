using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    /// <summary>
    /// 变形左右枚举，决定顺时针，还是逆时针
    /// </summary>
    enum E_ChangeType
    {
        Left,
        Right,
    }

    internal class BlockWorker:IDraw
    {
        //方块们
        private List<DrawObject> blocks;

        //选择一个容器装各个方块的形态信息
        private Dictionary<E_DrawType, BlockInfo> blockInfoDic;

        //记录信息
        private BlockInfo nowBlockInfo;

        //记录当前的形态索引
        private int index;

        public BlockWorker()
        {
            blockInfoDic = new Dictionary<E_DrawType, BlockInfo>()
            {
                {E_DrawType.Cube , new BlockInfo(E_DrawType.Cube)},
                {E_DrawType.Line , new BlockInfo(E_DrawType.Line)},
                {E_DrawType.Tank , new BlockInfo(E_DrawType.Tank)},
                {E_DrawType.Left_Ladder , new BlockInfo(E_DrawType.Left_Ladder)},
                {E_DrawType.Right_Ladder , new BlockInfo(E_DrawType.Right_Ladder)},
                {E_DrawType.Left_Long_Ladder, new BlockInfo(E_DrawType.Left_Long_Ladder)},
                {E_DrawType.Right_Long_Ladder , new BlockInfo(E_DrawType.Right_Long_Ladder)}
            };
            //随机方块
            RandomCreateBlock();
        }

        //绘制方块
        public void Draw()
        {
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].Draw();
            }
        }

        /// <summary>
        /// 随机创建方块
        /// </summary>
        public void RandomCreateBlock()
        {
            //随机方块类型
            Random r = new Random();
            E_DrawType  type = (E_DrawType)r.Next(2,9);

            //每次新建一个形状，都是新创建四个小正方形
            blocks = new List<DrawObject>()
            {
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type),
                new DrawObject(type)
            };
            //初始化方块位置
            blocks[0].pos = new Postion(24, -5);

            //取出方块形态信息 来进行随机
            //把取出来的方块存储起来
            nowBlockInfo = blockInfoDic[type];

            //随机几种形态中的一种，来设置方块信息
            index = r.Next(0, nowBlockInfo.Count);

            //取出其中一种的形态的坐标信息
            Postion[] pos = nowBlockInfo[index];

            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的pos是相对原点方块的坐标，需要计算
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }
        }

        //用于擦除上一个
        public void ClearDraw()
        {
            for(int i = 0;i < blocks.Count;i++)
            {
                blocks[i].ClearDraw();
            }
        }

        /// <summary>
        /// 变形相关代码
        /// </summary>
        public void Change(E_ChangeType type)
        {
            //变之前，把之前的擦除
            ClearDraw();

            switch (type)
            {
                case E_ChangeType.Left:
                    --index;
                    if(index < 0)
                        index = nowBlockInfo.Count - 1;
                    break;
                case E_ChangeType.Right:
                    ++index;
                    if (index >= nowBlockInfo.Count)
                        index = 0;
                    break;
            }
            //得到对应位置的位移信息
            Postion[] pos = nowBlockInfo[index];
            //将另外的三个小方块进行设置
            for (int i = 0; i < pos.Length; i++)
            {
                //取出来的pos是相对原点方块的坐标，需要计算
                blocks[i + 1].pos = blocks[0].pos + pos[i];
            }

            //变完之后再绘制
            Draw();
        }

        /// <summary>
        /// 判断能不能变形
        /// </summary>
        public bool CanChange(E_ChangeType e_ChangeType,Map map)
        {
            //用临时变量，记录当前索引
            int nowindex = index;
            switch (e_ChangeType) 
            {
                case E_ChangeType.Left:
                    --nowindex;
                    if (nowindex < 0)
                        nowindex = nowBlockInfo.Count - 1;
                    break;
                case E_ChangeType.Right:
                    ++nowindex;
                    if (nowindex >= nowBlockInfo.Count)
                        nowindex = 0;
                    break;
            }
            //得到对应位置的位移信息
            Postion[] pos = nowBlockInfo[nowindex];
            //判断是否超出边界，是否和动态方块重合
            Postion nowPos;
            for(int i = 0; i < pos.Length; i++)
            {
                nowPos = pos[i] + blocks[0].pos;
                if(nowPos.x < 2 || nowPos.x >= Game.w - 2||
                    nowPos.y >= map.h)
                {
                    return false;
                }
            }

            for (int i = 0; i < pos.Length; i++)
            {
                nowPos = pos[i] + blocks[0].pos;

                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if(nowPos  == map.dynamicWalls[j].pos)
                    return false;
                }
            }
            return true;
        }

        //方块的左右移动
        public void MoveLR(E_ChangeType type)
        {
            ClearDraw();

            //根据传入的类型 决定左右移动
            Postion movePos = new Postion(type== E_ChangeType.Left?-2:2 , 0);
            //遍历所有小方块
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].pos += movePos;
            } 

            Draw();
        }

        //判断是否可以进行左右移动
        public bool CanMove(E_ChangeType type, Map map)
        {
            //根据传入的类型 决定左右移动
            Postion movePos = new Postion(type == E_ChangeType.Left ? -2 : 2, 0);

            Postion nowPos;

            //遍历所有小方块
            //判断是否和墙重合
            for (int i = 0; i < blocks.Count; i++)
            {
                nowPos = blocks[i].pos + movePos;
                if (nowPos.x < 2 || nowPos.x >= Game.w - 2)
                {
                    return false;
                }
            }

            //判断是否和动态方块重合
            for (int i = 0; i < blocks.Count; i++)
            {
                nowPos = blocks[i].pos + movePos;
                for(int j = 0;j < map.dynamicWalls.Count;j++)
                {
                    if(nowPos == map.dynamicWalls[j].pos)
                        return false;
                }
            }
            return true;
        }

        //自动向下移动
        public void AutoMove()
        {
            ClearDraw();

            //自动移动一格位置
            Postion downMove = new Postion(0, 1);
            //得到所有的方块，让其向下移动
            for (int i = 0; i < blocks.Count; i++)
            {
                blocks[i].pos += downMove;
            }

            Draw();
        }

        //限制自动向下移动的范围
        public bool CanAutoMove(Map map) 
        {
            //自动移动一格位置
            Postion pos;

            Postion downMove = new Postion(0, 1);

            //边界判断
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + downMove;
                if(pos.y >= map.h)
                {
                    //给于地图动态方块
                    map.AddWalls(blocks);

                    //随机创建新方块
                    RandomCreateBlock();
                    return false;
                }
            }

            //动态方块
            //判断是否和动态方块重合
            for (int i = 0; i < blocks.Count; i++)
            {
                pos = blocks[i].pos + downMove;
                for (int j = 0; j < map.dynamicWalls.Count; j++)
                {
                    if (pos == map.dynamicWalls[j].pos)
                    {
                        //给于地图动态方块
                        map.AddWalls(blocks);

                        //随机创建新方块
                        RandomCreateBlock();
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
