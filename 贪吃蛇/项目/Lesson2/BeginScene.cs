using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    internal class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene() 
        {
            strTitle = "贪吃蛇";
            strOne = "开始游戏";
            strDescription = "      结束游戏";
            ExtraDisplay();
        }
        public override void EnterJDoSomthing()
        {
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Select);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        void ExtraDisplay()
        {
            //显示操作方法
            Console.SetCursorPosition(0, 20);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("WASD控制上下左右，J确定");
        }
    }
}
