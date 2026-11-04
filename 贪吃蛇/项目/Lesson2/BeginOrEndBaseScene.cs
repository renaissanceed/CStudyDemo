using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    abstract class BeginOrEndBaseScene : ISceneUpdate
    {

        //当前选中的索引位置
        protected int nowSelIndex = 0;

        //标题
        protected string strTitle;

        //结束游戏
        protected string strDescription;

        //开始游戏或者回到开始界面
        protected string strOne;

        //按下J键后执行的抽象函数
        public abstract void EnterJDoSomthing();

        public void Update()
        {
            //开始或结束场景的逻辑
            //选择当前的选项 监听 键盘输入 wsj
            Console.ForegroundColor = ConsoleColor.White;

            //显示标题
            Console.SetCursorPosition(Game.w/2 - strTitle.Length , 5);
            Console.Write(strTitle);

            //显示下方的选项
            //第一行的信息
            Console.SetCursorPosition(Game.w/2 - strOne.Length , 8);
            Console.ForegroundColor = nowSelIndex == 0 ? ConsoleColor.Red
                :ConsoleColor.White;
            Console.Write(strOne);

            //第二行的信息
            Console.SetCursorPosition(Game.w - 50, 10);
            Console.ForegroundColor = nowSelIndex == 1 ? ConsoleColor.Red
                : ConsoleColor.White;
            Console.Write(strDescription);

            //检测输入
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.W: 
                    --nowSelIndex;
                    if (nowSelIndex < 0)
                    { 
                        nowSelIndex = 0; 
                    }
                    break;
                case ConsoleKey.S:
                    ++nowSelIndex;
                    if (nowSelIndex > 1)
                    {
                        nowSelIndex = 1;
                    }
                    break;
                case ConsoleKey.J: 
                    EnterJDoSomthing();
                    break;
            }
        }
    }
}
