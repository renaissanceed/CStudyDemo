using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
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
                Game.score = 0;
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
