using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    struct Position
    {
        public int x; 
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        //贪吃蛇中肯定存在位置的比较
        //各个游戏对象，都会去比较位置是否重合

        public static bool operator ==(Position left, Position right)
        {
            if (left.x == right.x && left.y == right.y)
            { 
                return true; 
            }
            else
            { 
                return false; 
            }    
        }

        public static bool operator !=(Position left, Position right)
        {
            if (left.x == right.x && left.y == right.y)
            {
                return false;
            }
            else
            {
                return true;
            } 
        }
    }
}
