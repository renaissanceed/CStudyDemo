using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

namespace NO._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int NowLevel = 1;
            bool k = true;
            int w = 50, s = 30;
            Console.CursorVisible = false;
            Console.SetWindowSize(w, s);
            Console.SetBufferSize(w, s);

            while (true)
            {
                switch (NowLevel)
                { 
                case 1:
                    {
                        int nowIndex = 0;
                        Console.Clear();
                        Console.SetCursorPosition(w/2-4, s-22);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("营救蒋公主");

                        while (true)
                        {
                            Console.SetCursorPosition(w / 2 - 3, s-10);
                            if (nowIndex == 0)
                                Console.ForegroundColor = ConsoleColor.Red;
                            else if (nowIndex == 1)
                                Console.ForegroundColor = ConsoleColor.White;
                            Console.WriteLine("开始游戏");

                            Console.SetCursorPosition(w / 2 - 3, s - 8);
                            if (nowIndex == 0)
                                Console.ForegroundColor = ConsoleColor.White;
                            else if (nowIndex == 1)
                                Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("退出游戏");
                                
                           char str = Console.ReadKey(true).KeyChar;
                           switch (str)
                           {
                                case 'W':
                                case 'w':
                                    nowIndex = 0;
                                    break;

                                case 's':
                                case 'S':
                                    nowIndex = 1;
                                    break;

                                case 'j':
                                case 'J':
                                if(nowIndex == 0)
                                {
                                      NowLevel = 2;
                                      break;
                                }
                                else if(nowIndex==1)
                                {
                                      Console.Clear();
                                      Environment.Exit(0);
                                }
                                break;
                           }
                                if (NowLevel == 2)
                                    break;
                        
                        }
                    }
                        break;
                case 2:
                        {
                            Console.Clear();
                            #region 边界
                            Console.ForegroundColor = ConsoleColor.Red;
                            for (int i = 0; i < w; i += 2)
                            {
                                Console.SetCursorPosition(i, 0);
                                Console.Write("■");
                                Console.SetCursorPosition(i, s - 1);
                                Console.Write("■");
                                Console.SetCursorPosition(i, s - 8);
                                Console.Write("■");
                            }
                            for (int i = 0; i < s; i++)
                            {
                                Console.SetCursorPosition(0, i);
                                Console.Write("■");
                                Console.SetCursorPosition(w - 2, i);
                                Console.Write("■");
                            }
                            #endregion
                            #region boss属性
                            int bossX = 25;
                            int bossY = 11;
                            int bossAtkMax = 18;
                            int bossAtkMin = 6;
                            int bossDef = 4;
                            int bossDod = 4;
                            int bossHp=100;
                            string bossIcon = "★";
                            ConsoleColor bossColorF = ConsoleColor.DarkRed;
                            #endregion
                            #region 玩家属性
                            int playerX = 5;
                            int playerY = 6;
                            int gongX = 29;
                            int gongY =17;
                            bool Isgong = false;
                            int playerAtkMax =  17;
                            int playerAtkMin =  6;
                            int playerDef =  6;
                            int playerDod =  2;
                            int playerHp =  100;
                            string playerIcon = "●";
                            ConsoleColor playerColorA = ConsoleColor.White;
                            char jue;
                            bool IsPlayerAttack=false;
                            bool IsJiang = false;
                            #endregion  
                            while (true)
                            {
                                #region boss相关
                                if (bossHp>0)
                                {
                                    Console.SetCursorPosition(bossX,bossY);
                                    Console.ForegroundColor = bossColorF;
                                    Console.Write(bossIcon);
                                }
                                else if(bossHp<0)
                                {
                                    Console.SetCursorPosition(bossX, bossY);
                                    Console.Write(" ");
                                    Console.SetCursorPosition(2, s - 5);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(2, s - 2);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(2, s - 6);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(2, s - 3);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(gongX,gongY);
                                    Console.ForegroundColor= ConsoleColor.Yellow;
                                    Console.Write("●");
                                    Isgong=true;
                                    IsPlayerAttack=false ;
                                    IsJiang=true ;
                                }
                                #endregion
                                #region 玩家相关
                                if (playerHp < 0)
                                {

                                    Console.SetCursorPosition(playerX, playerY);
                                    Console.Write(" ");
                                    Console.SetCursorPosition(2, s - 5);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(2, s - 6);
                                    Console.Write("                                           ");
                                    Console.SetCursorPosition(2, s - 2);
                                    Console.Write("                                           ");
                                }
                                else if (playerHp > 0)
                                {
                                    Console.SetCursorPosition(playerX, playerY);
                                    Console.ForegroundColor = playerColorA;
                                    Console.Write(playerIcon);
                                }
                                #endregion
                                #region 主和怪
                                Random attack = new Random();
                                if (IsPlayerAttack)
                                {
                                    #region 攻击
                                    int atk = 0,atkDod=0,atkDodHp=0,bossatk=0;
                                    while (true)
                                    {
                                        #region 结束部分
                                        if (bossHp <= 0 && playerHp>=0)
                                        {
                                            Console.SetCursorPosition(2, s - 5);
                                            Console.Write("                                  ");
                                            Console.SetCursorPosition(2, s - 7);
                                            Console.ForegroundColor=ConsoleColor.DarkRed;
                                            Console.Write("按J拯救蒋公主！                          ");
                                            IsPlayerAttack =false;
                                            break;
                                        }
                                        if (playerHp <= 0 && bossHp>=0)
                                        {
                                            Console.SetCursorPosition(2, s - 3);
                                            Console.Write("                                  ");
                                            Console.SetCursorPosition(2, s - 7);
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.Write("战斗失败！按J继续                      ");
                                            IsPlayerAttack = false;
                                            break;
                                        }
                                        #endregion
                                        jue = Console.ReadKey(true).KeyChar;
                                        Console.SetCursorPosition(2, s - 7);
                                        Console.ForegroundColor = ConsoleColor.Cyan;
                                        Console.Write("战斗中！按J继续    ");
                                        atkDodHp = attack.Next(1, 3);
                                        atkDod = attack.Next(1, 10);
                                        //Console.SetCursorPosition(2, s - 4);
                                        //Console.ForegroundColor = ConsoleColor.Cyan;
                                        //Console.Write(playerHp+"p");
                                        //Console.SetCursorPosition(2, s - 3);
                                        //Console.ForegroundColor = ConsoleColor.Cyan;
                                        //Console.Write(bossHp+"b");
                                        if ((jue == 'j' || jue == 'J') && bossHp>0&&playerHp>0)
                                        {
                                            #region 玩家攻击
                                            atk = attack.Next(playerAtkMin, playerAtkMax);
                                            Console.ForegroundColor = ConsoleColor.White;
                                            if (atkDod < bossDod)
                                            {
                                                Console.SetCursorPosition(2, s - 6);
                                                Console.Write("怪物闪避了！                                 ");
                                            }
                                            else
                                            {
                                                if (atk > bossDef)
                                                {
                                                    Console.SetCursorPosition(2, s - 6);
                                                    Console.Write("                          ");
                                                        bossHp = bossHp + bossDef - atk;
                                                        Console.SetCursorPosition(2, s - 6);
                                                        Console.Write("怪物受到了" + (atk - bossDef) + "伤害！                        ");
                                                }
                                                else
                                                {
                                                    bossHp = bossHp - atkDodHp;
                                                    Console.SetCursorPosition(2, s - 6);
                                                    Console.Write("怪物防御了！受到了{0}点伤害                     ", atkDodHp);
                                                }
                                            }
                                            #endregion
                                            #region 敌人攻击
                                            bossatk = attack.Next(bossAtkMin,bossAtkMax);
                                            Console.ForegroundColor = ConsoleColor.Magenta;
                                            if (atkDod < playerDod)
                                            {
                                                Console.SetCursorPosition(2, s - 5);
                                                Console.Write("你闪避了！                                 ");
                                            }
                                            else
                                            {
                                                if (bossatk > playerDef)
                                                {
                                                    Console.SetCursorPosition(2, s - 5);
                                                    Console.Write("                          ");
                                                        playerHp = playerHp + playerDef - bossatk;
                                                        Console.SetCursorPosition(2, s - 5);
                                                        Console.Write("你受到了" + (bossatk - playerDef) + "伤害！                        ");
                                                }
                                                else
                                                {
                                                    playerHp = playerHp - atkDodHp;
                                                    Console.SetCursorPosition(2, s - 5);
                                                    Console.Write("你防御了！受到了{0}点伤害                     ", atkDodHp);
                                                }
                                            }
                                            #endregion
                                        
                                        }

                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region 移动
                                    jue = Console.ReadKey(true).KeyChar;
                                    Console.SetCursorPosition(playerX, playerY);
                                    Console.Write(" ");
                                    switch (jue)
                                    {
                                            #region wasd
                                            case 'w':
                                        case 'W':
                                            {
                                                --playerY;
                                                if (playerY < 1)
                                                    playerY = 1;
                                                else if (bossX == playerX && bossY == playerY && bossHp > 0)
                                                    {
                                                    ++playerY;
                                                    }
                                                if (gongX== playerX && gongY == playerY && Isgong==true)
                                                {
                                                    ++playerY;
                                                }

                                            }

                                            break;
                                        case 's':
                                        case 'S':
                                            {
                                                ++playerY;
                                                if (playerY > Console.BufferHeight - 9)
                                                    playerY = Console.BufferHeight - 9;
                                                else if (bossX == playerX && bossY == playerY && bossHp > 0)
                                                    {
                                                    --playerY;
                                                }
                                                if (gongX == playerX && gongY == playerY && Isgong == true)
                                                {
                                                    --playerY;
                                                }
                                            }
                                            break;
                                        case 'a':
                                        case 'A':
                                            {
                                                playerX -= 2;
                                                if (playerX < 2)
                                                    playerX = 2;
                                                else if (bossX == playerX && bossY == playerY && bossHp > 0)
                                                {
                                                    playerX += 2;
                                                }
                                                if (gongX == playerX && gongY == playerY && Isgong == true)
                                                {
                                                    playerX += 2;
                                                }
                                            }
                                            break;
                                        case 'd':
                                        case 'D':
                                            {
                                                playerX += 2;
                                                if (playerX > Console.BufferWidth - 4)
                                                    playerX = Console.BufferWidth - 4;
                                                else if (bossX == playerX && bossY == playerY  && bossHp > 0)
                                                {
                                                    playerX -= 2;
                                                }
                                                if (gongX == playerX && gongY == playerY && Isgong == true)
                                                {
                                                    playerX -= 2;
                                                }
                                            }
                                            break;
                                            #endregion
                                            case 'j':
                                        case 'J':
                                            {
                                                if ((bossX == playerX && (bossY == playerY - 1 || bossY == playerY + 1)
                                                || bossY == playerY && (bossX == playerX - 2 || bossX == playerX + 2))
                                                && bossHp > 0)
                                                {
                                                    Console.SetCursorPosition(2, s - 7);
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                    Console.WriteLine("开始战斗！按J继续");
                                                    Console.SetCursorPosition(2, s - 6);
                                                    Console.WriteLine("玩家血量为" + playerHp + "\t\tBoss血量为" + bossHp);
                                                    IsPlayerAttack = true;
                                                }
                                                if((gongX == playerX && (gongY == playerY - 1 || gongY== playerY + 1)
                                                ||gongY == playerY && (gongX == playerX - 2 || gongX == playerX + 2))
                                                &&Isgong==true)                                          
                                                {
                                                        NowLevel = 3;
                                                }
                                                if (playerHp <= 0)
                                                {
                                                    NowLevel = 4;
                                                    break;
                                                }
                                            }
                                            break;
                                    }
                                    #endregion
                                }
                                if (NowLevel == 3 || NowLevel==4)
                                    break;

                                #endregion
                            }

                        }
                    break;
                case 3:
                        {
                            int nowIndex = 0;
                            Console.Clear();
                            Console.SetCursorPosition(w/2-3, s/2);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("任务胜利！");

                            while (true)
                            {
                                Console.SetCursorPosition(w / 2 - 3, s-10);
                                if (nowIndex == 0)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else if (nowIndex == 1)
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("返回主页");

                                Console.SetCursorPosition(w / 2 - 3, s - 8);
                                if (nowIndex == 0)
                                    Console.ForegroundColor = ConsoleColor.White;
                                else if (nowIndex == 1)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("退出游戏");

                                char st = Console.ReadKey(true).KeyChar;
                                switch (st)
                                {
                                    case 'W':
                                    case 'w':
                                        nowIndex = 0;
                                        break;

                                    case 's':
                                    case 'S':
                                        nowIndex = 1;
                                        break;

                                    case 'j':
                                    case 'J':
                                        if (nowIndex == 0)
                                        {
                                            NowLevel = 1;
                                            break;
                                        }
                                        else if (nowIndex == 1)
                                        {
                                            Console.Clear();
                                            Environment.Exit(0);
                                        }
                                        break;
                                }
                                if (NowLevel == 1)
                                    break;

                            }
                        }
                        break;
                case 4:
                        {
                            int nowIndex = 0;
                            Console.Clear();
                            Console.SetCursorPosition(w / 2 - 3, s / 2);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("任务失败！");

                            while (true)
                            {
                                Console.SetCursorPosition(w / 2 - 3, s - 10);
                                if (nowIndex == 0)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                else if (nowIndex == 1)
                                    Console.ForegroundColor = ConsoleColor.White;
                                Console.WriteLine("返回主页");

                                Console.SetCursorPosition(w / 2 - 3, s - 8);
                                if (nowIndex == 0)
                                    Console.ForegroundColor = ConsoleColor.White;
                                else if (nowIndex == 1)
                                    Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("退出游戏");

                                char st = Console.ReadKey(true).KeyChar;
                                switch (st)
                                {
                                    case 'W':
                                    case 'w':
                                        nowIndex = 0;
                                        break;

                                    case 's':
                                    case 'S':
                                        nowIndex = 1;
                                        break;

                                    case 'j':
                                    case 'J':
                                        if (nowIndex == 0)
                                        {
                                            NowLevel = 1;
                                            break;
                                        }
                                        else if (nowIndex == 1)
                                        {
                                            Console.Clear();
                                            Environment.Exit(0);
                                        }
                                        break;
                                }
                                if (NowLevel == 1)
                                    break;

                            }
                        }
                        break;
                }
            }


        }
    }
}
