using System;

namespace CompareGeneric
{
    // where表示泛型的限定条件，即泛型类中的泛型必须实现IComparable接口才有CompareTo方法。
    public class Compare<T> where T:IComparable
    {
        public static T CompareGeneric(T t1, T t2)
        {
            // >0表示大于，<0表示小于，=0表示等于。
            if (t1.CompareTo(t2) > 0) 
            {
                return t1;
            }
            return t2;
        }
    }
    class Client
    {
        static void Main1(string[] args)
        {
            Console.WriteLine(Compare<int>.CompareGeneric(1, 2));
        }
    }
}
