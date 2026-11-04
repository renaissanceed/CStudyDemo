using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    class  SelectScene : BeginOrEndBaseScene
    {

        public SelectScene() 
        {
            strOne = "简单（一倍积分）";
            strDescription = "  困难（二倍积分）";
            strTitle = "  选择难度";
        }

        public override void EnterJDoSomthing()
        {
            if (nowSelIndex == 0)
            {
                Game.level = 13000;
            }
            else
            {
                Game.level = 9000;
            }

            Game.ChangeScene(E_SceneType.Game);
        }

    }
}
