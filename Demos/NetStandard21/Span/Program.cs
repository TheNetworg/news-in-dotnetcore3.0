using System;
using System.Runtime.InteropServices;

namespace Span
{
    class Program
    {
        static void Span1()
        {
            // Create a span over an array.
            var array = new byte[100];
            var arraySpan = new Span<byte>(array);

            byte data = 0;
            for (int ctr = 0; ctr < arraySpan.Length; ctr++)
                arraySpan[ctr] = data++;

            int arraySum = 0;
            foreach (var value in array)
                arraySum += value;

            Console.WriteLine($"The sum is {arraySum}");
            // Output:  The sum is 4950
        }

        static void Span2()
        {
            // Create a span from native memory.
            var native = Marshal.AllocHGlobal(100);
            Span<byte> nativeSpan;
            unsafe
            {
                nativeSpan = new Span<byte>(native.ToPointer(), 100);
            }
            byte data = 0;
            for (int ctr = 0; ctr < nativeSpan.Length; ctr++)
                nativeSpan[ctr] = data++;

            int nativeSum = 0;
            foreach (var value in nativeSpan)
                nativeSum += value;

            Console.WriteLine($"The sum is {nativeSum}");
            Marshal.FreeHGlobal(native);
            // Output:  The sum is 4950
        }

        static void Span3()
        {
            // Create a span on the stack.
            byte data = 0;
            Span<byte> stackSpan = stackalloc byte[100];
            for (int ctr = 0; ctr < stackSpan.Length; ctr++)
                stackSpan[ctr] = data++;

            int stackSum = 0;
            foreach (var value in stackSpan)
                stackSum += value;

            Console.WriteLine($"The sum is {stackSum}");
            // Output:  The sum is 4950
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Span1();
            Span2();
            Span3();
        }
    }
}
