using System;
using System.Collections.Generic;

namespace Foreach
{
    class Client
    {
        public static void Main1(string[] args)
        {
            List<int> ls = new List<int>(new int[] { 1, 2, 3, 4, 5 });
            foreach (int item in ls)
            // foreach是只读的，只能读取元素，不能在遍历过程中对集合元素进行修改、删除和增加。
            //for (int i = 0; i < ls.Count; i++)
            {
                ls.Add(item);
            }
        }
    }
}
