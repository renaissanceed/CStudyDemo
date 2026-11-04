using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 俄罗斯方块
{
    internal class BlockInfo
    {
        //方块信息坐标容器
        private List<Postion[]> postionList;

        //提供给外部根据索引找位置 获取偏移信息的
        public Postion[] this[int index]
        {
            get
            {
                if(index < 0)
                {
                    return postionList[0];
                }
                else if(index >= postionList.Count)
                {
                     return postionList[postionList.Count];
                }
                else
                {
                    return postionList[index];
                }
            }
        }

        public BlockInfo(E_DrawType e_DrawType) 
        {
            //初始化list
            postionList = new List<Postion[]>();

            AddBlock(e_DrawType);
 
        }

        private void AddBlock(E_DrawType Type)
        {
            switch (Type)
            {
                case E_DrawType.Cube:
                    postionList.Add(new Postion[3]{
                    new Postion(2,0),
                    new Postion(0,1),
                    new Postion(2,1)
                    });
                    break;
                case E_DrawType.Line:
                    postionList.Add(new Postion[3]{
                    new Postion(0,-1),
                    new Postion(0,1),
                    new Postion(0,2)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-4,0),
                    new Postion(-2,0),
                    new Postion(2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(0,-2),
                    new Postion(0,-1),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,0),
                    new Postion(2,0),
                    new Postion(4,0)
                    });
                    break;
                case E_DrawType.Tank:
                    postionList.Add(new Postion[3]{
                    new Postion(-2,0),
                    new Postion(2,0),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(0,-1),
                    new Postion(-2,0),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,0),
                    new Postion(0,-1),
                    new Postion(-2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(0,-1),
                    new Postion(0,1),
                    new Postion(2,0)
                    });
                    break;
                case E_DrawType.Left_Ladder:
                    postionList.Add(new Postion[3]{
                    new Postion(0,-1),
                    new Postion(2,0),
                    new Postion(2,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,1),
                    new Postion(2,0),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,-1),
                    new Postion(0,1),
                    new Postion(-2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,-1),
                    new Postion(0,-1),
                    new Postion(-2,0)
                    });
                    break;
                case E_DrawType.Right_Ladder:
                    postionList.Add(new Postion[3]{
                    new Postion(0,-1),
                    new Postion(-2,0),
                    new Postion(-2,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,-1),
                    new Postion(2,0),
                    new Postion(0,-1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,-1),
                    new Postion(0,1),
                    new Postion(2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,1),
                    new Postion(0,1),
                    new Postion(-2,0)
                    });
                    break;
                case E_DrawType.Left_Long_Ladder:
                    postionList.Add(new Postion[3]{
                    new Postion(-2,-1),
                    new Postion(0,-1),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,1),
                    new Postion(2,0),
                    new Postion(-2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(0,1),
                    new Postion(0,-1),
                    new Postion(2,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,-1),
                    new Postion(-2,0),
                    new Postion(2,0)
                    });
                    break;
                case E_DrawType.Right_Long_Ladder:
                    postionList.Add(new Postion[3]{
                    new Postion(2,-1),
                    new Postion(0,-1),
                    new Postion(0,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(2,-1),
                    new Postion(2,0),
                    new Postion(-2,0)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(0,1),
                    new Postion(0,-1),
                    new Postion(-2,1)
                    });
                    postionList.Add(new Postion[3]{
                    new Postion(-2,1),
                    new Postion(-2,0),
                    new Postion(2,0)
                    });
                    break;
            }
        }

        //提供给外部一个获取当前几种形状的属性，只有1种和4种形态
        public int Count { get => postionList.Count; }
    }
}
