using System;
using System.Buffers;
using System.Linq;

namespace Reflexe
{
    class Program
    {
        private static ReadOnlySpan<byte> CRLF => new byte[] { (byte)'\r', (byte)'\n' };

        public static void ReadLines(ReadOnlySequence<byte> sequence)
        {
            var reader = new SequenceReader<byte>(sequence);

            while (!reader.End)
            {
                if (!reader.TryReadToAny(out ReadOnlySpan<byte> line, CRLF, advancePastDelimiter: false))
                {
                    // Couldn't find another delimiter
                    // ...
                }

                if (!reader.IsNext(CRLF, advancePast: true))
                {
                    // Not a good CR/LF pair
                    // ...
                }

                // line is valid, process
                ProcessLine(line);
            }
        }

        private static void ProcessLine(ReadOnlySpan<byte> line)
        {            
            Console.WriteLine(System.Text.Encoding.Default.GetString(line.ToArray()));
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var message = "Hello \r\n this \r\n is \r\n examplele\r\n";
            var sequence = new ReadOnlySequence<byte>(message.Select(x => (byte)x).ToArray());
            ReadLines(sequence);
        }
    }
}
