using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    /// <summary>
    /// 移动方向枚举
    /// </summary>
    enum E_MoveDir
    {
        /// <summary>
        /// 向上
        /// </summary>
        up,
        /// <summary>
        /// 向下
        /// </summary>
        down,
        /// <summary>
        /// 向左
        /// </summary>
        left,
        /// <summary>
        /// 向右
        /// </summary>
        right,
    }

    class Snake : IDraw
    {
        SnakeBoby[] bobys;

        //记录当前蛇的长度
        int nowNum;

        //当前移动的方向
        E_MoveDir dir;

        public Snake(int x , int y)
        {
            Console.SetCursorPosition(x, y);
            bobys = new SnakeBoby[200];

            bobys[0] = new SnakeBoby(E_BobyType.Hard, x , y);
            nowNum = 1;

            dir = E_MoveDir.right;
        }

        public void Draw()
        {
            //画身子
            for (int i = 0; i < nowNum; i++)
            {
                bobys[i].Draw();
            }
        }

        public void Move()
        {
            //移动前，擦除最后一个位置 
            SnakeBoby lastBoby = bobys[nowNum - 1];
            Console.SetCursorPosition(lastBoby.pos.x, lastBoby.pos.y);
            Console.Write("  ");

            //显示长度
            LengthSnake();

            //在蛇头移动之前 从蛇尾开始 不停的 让后一个位置 等于前一个位置
            for (int i = nowNum - 1; i > 0; i--)
            {
                bobys[i].pos = bobys[i - 1].pos;
            }

            switch (dir)
            {
                case E_MoveDir.up:
                    --bobys[0].pos.y;
                    break;
                case E_MoveDir.down:
                    ++bobys[0].pos.y;
                    break;
                case E_MoveDir.left:
                    bobys[0].pos.x -= 2;
                    break;
                case E_MoveDir.right:
                    bobys[0].pos.x += 2;
                    break;
            }
        }

        public void ChangeDir(E_MoveDir dir)
        {
            //只有头部的时候，可以左转右，右转左，上转下，下转上
            //有身体的情况下，就不能直接转

            if(dir == this.dir 
                || nowNum > 1 && 
                (this.dir == E_MoveDir.left && dir == E_MoveDir.right ||
                this.dir == E_MoveDir.right && dir == E_MoveDir.left ||
                this.dir == E_MoveDir.down && dir == E_MoveDir.up ||
                this.dir == E_MoveDir.up && dir == E_MoveDir.down))
            {
                return;
            }
            //只要没有return 就记录传入的方向，按照这个方向去移动
            this.dir = dir;
        }

        public bool CheckEnd(Map map)
        {
            //是否和墙壁重合
            for(int i = 0; i < map.walls.Length; i++)
            {
                if (bobys[0].pos == map.walls[i].pos)
                {
                   return true;
                }
            }

            //是否和自身重合
            for (int i = 1; i < nowNum; i++)
            {
                if (bobys[0].pos == bobys[i].pos)
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckSamePos(Position pos)
        {
            //通过传入一个位置 来判断是否和蛇位置重合
            for (int i = 0;i < nowNum;i++)
            {
                if(bobys[i].pos == pos)
                {
                    return true; 
                }
            }
            return false;
        }

        public void CheckEatFood(Food food)
        {
            if (bobys[0].pos == food.pos)
            {
                if(Game.level == 9000)
                {
                    //困难模式多累加一次得分
                    Game.score++;
                }
                //累加得分
                Game.score++;

                //吃到了再随机生成食物位置，然后再增加蛇身体长度
                food.RandomPos(this);

                //长身体
                AddSnakeBobys();
            }
        }

        void AddSnakeBobys()
        {
            SnakeBoby frontBoby = bobys[nowNum - 1];

            //先长身体
            bobys[nowNum] = new SnakeBoby(E_BobyType.Boby, frontBoby.pos.x, frontBoby.pos.y);

            //再加长度
            nowNum++;

        }

        //用于显示当前长度
        void LengthSnake()
        {
            Console.SetCursorPosition(4, 20);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("长度：{0}", nowNum);
        }
    }
}
