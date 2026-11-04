using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;

namespace 俄罗斯方块
{
    class GameScene : ISceneUpdate
    {
        public Map map;

        BlockWorker worker;

        Thread inputThread;

        private bool isRunning;

        //得分
        public static int? score;

        public GameScene()
        {
            score = null;
            map = new Map(this);
            worker = new BlockWorker();
            inputThread = new Thread(CheakInputThread);
            //设置成后台线程 生命周期随着主线程决定
            inputThread.IsBackground = true;
            //开启线程
            inputThread.Start();

            isRunning = true;
        }

        private void CheakInputThread()
        {
            while (isRunning)
            {
                //判断是否按键输入
                if (Console.KeyAvailable)
                {
                    //为了避免影响主线程 在输入后再加锁
                    lock (worker)
                    {
                        switch (Console.ReadKey(true).Key)
                        {
                            case ConsoleKey.J:
                                //判断能不能变形
                                if (worker.CanChange(E_ChangeType.Left, map))
                                    worker.Change(E_ChangeType.Left);
                                break;
                            case ConsoleKey.K:
                                if (worker.CanChange(E_ChangeType.Right, map))
                                    worker.Change(E_ChangeType.Right);
                                break;
                            case ConsoleKey.A:
                                if (worker.CanMove(E_ChangeType.Left, map))
                                    worker.MoveLR(E_ChangeType.Left);
                                break;
                            case ConsoleKey.D:
                                if (worker.CanMove(E_ChangeType.Right, map))
                                    worker.MoveLR(E_ChangeType.Right);
                                break;
                            case ConsoleKey.S:
                                //方块自动往下动
                                if (worker.CanAutoMove(map))
                                    worker.AutoMove();
                                break;
                        }
                    }  
                }   
            }
        }
        
        public void Update()
        {
            lock (worker)
            {
                //地图绘制
                map.Draw();
                //搬运工
                worker.Draw();
                //方块自动往下动
                if (worker.CanAutoMove(map))
                    worker.AutoMove();
                //绘制得分
                DrawScore();
            }
            //用线程休眠的形式
            Thread.Sleep(150);
        }

        public void StopThread()
        {
            isRunning = false;
            inputThread = null;
        }

        public void DrawScore()
        {
            Console.SetCursorPosition(0, 34);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("得分：");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(score);
        }
    }
}
