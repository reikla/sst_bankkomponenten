using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DllConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            MyStruct myStruct = new MyStruct();

            for (int i = 0; i < 10; i++)
            {
                var number = ComponentApi.GetNumber();
                Console.WriteLine($"Value of Number from DLL: {number}");

                Console.WriteLine($"Value of String before Manipulate from DLL: {myStruct.mystr}");
                try
                {
                    var errorcode = ComponentApi.ManipulateStruct(ref myStruct);
                    if (errorcode != 0)
                    {
                        Console.WriteLine($"Error Code:{errorcode}");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception happened: {e.Message}");
                }
                Console.WriteLine($"Value of Struct after Manipulate from DLL: {myStruct.mystr}");
                myStruct.mystr = "Und zurück";

            }
        }
    }
}
