using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace 项目
{

    class Food : GameObject
    {
        public Food(Snake snake) 
        {
           RandomPos(snake);
        }

        public override void Draw()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(pos.x, pos.y);
            Console.Write("★");
        }
        //随机位置的行为，和蛇的位置有关系
        public void RandomPos(Snake snake)
        {
            //随机位置
            Random random = new Random();
            int x = random.Next(2, Game.w / 2 - 1) * 2;
            int y = random.Next(2, Game.h - 4);
            pos = new Position(x, y);

            //显示得分
            Score();

            //得到蛇身体位置 
            //如果重合就会进入if语句
            if (snake.CheckSamePos(pos))
            {
                //调用自己重新随机生成
                RandomPos(snake);
            }

        }

        void Score()
        {
            Console.SetCursorPosition(4, 19);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("得分：{0}", Game.score);
        }
    }
}
