using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    struct Postion
    {
        public int x;
        public int y;

        public Postion(int x, int y)
        {
            this.y = y;
            this.x = x;
        }

        //重载==运算符，让两个Postion可以判断是否相等
        public static bool operator ==(Postion p1,Postion p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
                return true;

            return false;
        }

        //重载!=运算符，让两个Postion可以判断是否不等
        public static bool operator !=(Postion p1, Postion p2)
        {
            if (p1.x == p2.x && p1.y == p2.y)
                return false;

            return true;
        }

        //重载+运算符，让两个Postion可以直接相加
        public static Postion operator +(Postion p1, Postion p2)
        {
            Postion pos= new Postion(p1.x + p2.x , p1.y + p2.y);
            return pos;
        }
    }
}
