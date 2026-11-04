using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    class GameScene : ISceneUpdate
    {
        Map map;

        Snake snake;

        Food food;

        int updateIndex = 0;

        public GameScene()
        {
           map = new Map();
           snake = new Snake(40 , 10);
           food  = new Food(snake);
        }

        public void Update()
        {
            if (updateIndex % Game.level == 0)
            {
                map.Draw();
                food.Draw();

                snake.Move();
                snake.Draw();

                //检测是否撞墙
                if(snake.CheckEnd(map))
                {
                    //结束逻辑
                    Game.ChangeScene(E_SceneType.End);
                }

                snake.CheckEatFood(food);
                
                updateIndex = 1;
            }
            updateIndex++;

            //在控制台中检测玩家输入 让程序不再被卡住
            //判断有没有键盘输入，如果有就返回true，不是就返回false
            if (Console.KeyAvailable)
            {
                //检测输入输出 不能再 间隔帧里面去处理 应该每次都检测 这样才准确
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.W:
                        snake.ChangeDir(E_MoveDir.up);
                        break;
                    case ConsoleKey.A:
                        snake.ChangeDir(E_MoveDir.left);
                        break;
                    case ConsoleKey.S:
                        snake.ChangeDir(E_MoveDir.down);
                        break;
                    case ConsoleKey.D:
                        snake.ChangeDir(E_MoveDir.right);
                        break;
                }
            }

        }
    }
}
