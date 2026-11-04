using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    abstract class GameObject : IDraw
    {
        //游戏对象位置
        public Position pos;

        //可以继承接口后，把接口中的行为变成一个抽象行为
        //供子类实现，因为是一个抽象的行为，所以子类必须去实现，有点像是转移了的感觉
        public abstract void Draw();
    }
}
