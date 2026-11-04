using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    /// <summary>
    /// 场景类型枚举
    /// </summary>
    enum E_SceneType
    {
        /// <summary>
        /// 开始场景
        /// </summary>
        Start,
        /// <summary>
        /// 游戏场景
        /// </summary>
        Game,
        /// <summary>
        /// 选择场景
        /// </summary>
        Select,
        /// <summary>
        /// 结束场景
        /// </summary>
        End,
    }

    class Game
    {
        //游戏窗口
        public const int w = 80;
        public const int h = 20;
       
        //当前的游戏窗口
        public static ISceneUpdate nowScene;

        //选择难度
        public static int level;

        //得分
        public static int score;

        public Game() 
        {
            Console.CursorVisible = false;
            Console.SetWindowSize(w , h + 1 );
            Console.SetBufferSize(w , h + 1);

            ChangeScene(E_SceneType.Start);
        }

        public void Start()
        {
            //游戏主循环负责场景 更新
            while (true)
            {
                //判断当前场景不为空 就执行
                if (nowScene != null)
                {
                    nowScene.Update();
                }
            }
        }

        public static void ChangeScene(E_SceneType e_Scene)
        {
            //切换场景需要把上一个场景的内容全部擦除
            Console.Clear();

            switch (e_Scene)
            {
                case E_SceneType.Start:
                    nowScene = new BeginScene();
                    break;
                case E_SceneType.Game:
                    nowScene = new GameScene();
                    break;
                case E_SceneType.End:
                    nowScene = new EndScene();
                    break;
                case E_SceneType.Select:
                    nowScene = new SelectScene();
                    break;
            }
        }
    }
}
