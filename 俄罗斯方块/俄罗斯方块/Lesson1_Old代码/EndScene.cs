using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    internal class EndScene : BeginOrEndBaseScene
    {
        public EndScene()
        {
            strTitle = "游戏结束";
            strOne = "回到主菜单";
            strDescription = "      结束游戏";
        }
        public override void EnterJDoSomthing()
        {
            if (nowSelIndex == 0)
            {
                Game.ChangeScene(E_SceneType.Start);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
