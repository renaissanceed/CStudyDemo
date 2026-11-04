using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    internal class BeginScene : BeginOrEndBaseScene
    {
        public BeginScene() 
        {
            strTitle = "俄罗斯方块";
            strOne = "开始游戏";
            strDescription = "      结束游戏";
            ExtraDisplay();
        }
        public override void EnterJDoSomthing()
        {
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Game);
            }
            else
            {
                Environment.Exit(0);
            }
        }

        void ExtraDisplay()
        {
            //显示操作方法
            Console.SetCursorPosition(0, 34);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("WASD控制上下左右，J确定\n按J键 控制方块 向左 变形，" +
                "按K键 控制方块 向右 变形");
        }
    }
}
