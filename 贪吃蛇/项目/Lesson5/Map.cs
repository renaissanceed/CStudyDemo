using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 项目
{
    class Map : IDraw 
    {
        public Wall[] walls;

        public Map() 
        {
            //初始化墙的总数量
            walls = new Wall[Game.w + (Game.h - 3) * 2];

            //计数画了几个墙
            int index = 0;

            for (int i = 0; i < Game.w; i += 2)
            {
                //画横向的第一堵墙
                walls[index] = new Wall(i, 0);
                index++;

                //画横向的第二堵墙
                walls[index] = new Wall(i, Game.h - 2);
                index++;
            }

            for (int i = 1; i < Game.h - 2; i++)
            {
                //画纵向的第一堵墙
                walls[index] = new Wall(0, i);
                index++;

                //画纵向的第二堵墙
                walls[index] = new Wall(Game.w - 2, i);
                index++;
            }

        }
        
        public void Draw()
        {
            for (int i = 0; i < walls.Length; i++)
            {
                walls[i].Draw();
            }
        }
    }
}
