using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    /// <summary>
    /// 绘制类型 根据不同类型改变方块的存储颜色
    /// </summary>
    enum E_DrawType
    {
        /// <summary>
        /// 动态墙壁
        /// </summary>
        DynamicWalls,
        /// <summary>
        /// 墙壁
        /// </summary>
        Wall,
        /// <summary>
        /// 正方形方块
        /// </summary>
        Cube,
        /// <summary>
        /// 直线方块
        /// </summary>
        Line,
        /// <summary>
        /// 坦克
        /// </summary>
        Tank,
        /// <summary>
        /// 左梯子方块
        /// </summary>
        Left_Ladder,
        /// <summary>
        /// 右梯子方块
        /// </summary>
        Right_Ladder,
        /// <summary>
        /// 左长梯子
        /// </summary>
        Left_Long_Ladder,
        /// <summary>
        /// 右长梯子
        /// </summary>
        Right_Long_Ladder, 

    }

    class DrawObject : IDraw
    {
        //位置信息
        public Postion pos;
        //方块类型枚举
        public E_DrawType type;
        
        public DrawObject(E_DrawType type) => this.type = type;

        public DrawObject(E_DrawType type , int x , int y):this(type)=> 
            this.pos = new Postion(x, y);

        //绘制方块对象
        public void Draw()
        {
            //屏幕外不用再绘制
            if(pos.y < 0) 
                return;

            Console.SetCursorPosition(pos.x, pos.y);

            switch (type)
            {
                case E_DrawType.DynamicWalls:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                case E_DrawType.Wall:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case E_DrawType.Cube:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    break;
                case E_DrawType.Line:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case E_DrawType.Tank:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case E_DrawType.Left_Ladder:
                case E_DrawType.Right_Ladder:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case E_DrawType.Left_Long_Ladder:
                case E_DrawType.Right_Long_Ladder:
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
            }
            Console.WriteLine("■");
        }

        //添加一个擦除自己的方法
        public void ClearDraw()
        {
            //屏幕外不用再设置
            if (pos.y < 0)
                return;

            Console.SetCursorPosition(pos.x,pos.y);
            Console.Write("  ");
        }

        /// <summary>
        /// 切换方块类型  主要用于方块下落到地图时  把方块给墙壁
        /// </summary>
        /// <param name="type"></param>
        public void ChangType(E_DrawType type) => this.type = type;
    }
}
