using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
   /// <summary>
   /// 蛇身体类型枚举
   /// </summary>
    enum E_BobyType
    {
        /// <summary>
        /// 蛇头
        /// </summary>
        Hard,
        /// <summary>
        /// 蛇身体
        /// </summary>
        Boby,
    }
    internal class SnakeBoby : GameObject
    {
        public E_BobyType type;

        public SnakeBoby(E_BobyType type , int x, int y)
        {
            this.pos = new Position(x, y);
            this.type = type;
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x , pos.y);
            Console.ForegroundColor = 
                type ==  E_BobyType.Hard ? ConsoleColor.Yellow : ConsoleColor.Green;
            Console.Write(type == E_BobyType.Hard ? "●" : "◎");
        }
    }
}
