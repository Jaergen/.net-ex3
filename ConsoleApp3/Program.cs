using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to compress or decompress file? (c/d): ");
            var option = Console.ReadLine();
            if (option.ToLower() == "c")
            {
                Console.Write("Path for the file and filename of the file you want to compress: ");
                string path = Console.ReadLine();

                compressFile(path);
            }
            else if (option.ToLower() == "d")
            {
                Console.Write("Enter the path and filename of the file you want to decompress: ");
                string pathDecompress = Console.ReadLine();

                Console.Write("Enter the location for where your want to decompress the file: ");
                string location = Console.ReadLine();

                Console.Write("What do you want to call the file? (Remember filetype, ex. .pdf) ");
                string newName = Console.ReadLine();

                DecompressFile(pathDecompress, location, newName);
            }
            else
            {
                Console.WriteLine("Wrong option, use the letter c to compress file or the letter d to decompress");
            }
        
           
            

            
            //DecompressFile(path + fileToDeCompress);
        }

        public static void compressFile(string fileName)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (FileStream inFile = File.OpenRead(fileName))
            {
                using (FileStream outFile = File.Create(fileName + ".gz"))
                {
                    using (GZipStream compress = new GZipStream(outFile, CompressionMode.Compress))
                    {
                        inFile.CopyTo(compress);

                        Console.WriteLine("Compressed: {0}", fileName);
                        
                        stopWatch.Stop();

                        TimeSpan ts = stopWatch.Elapsed;

                        string elapsedTime = "Time it took to compress file: " + ts.Milliseconds + " ms.";
                        Console.WriteLine(elapsedTime);
                    }
                }
            }
        }

        public static void DecompressFile(string fileName, string location, string newName)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            using (FileStream inFile = File.OpenRead(fileName))
            {
                using (FileStream outFile = File.Create(location + newName))

                {
                    using (GZipStream Decompress = new GZipStream(inFile, CompressionMode.Decompress))
                    {
                        Decompress.CopyTo(outFile);

                        Console.WriteLine("Decompressed: {0}", fileName);
                        
                        stopWatch.Stop();

                        TimeSpan ts = stopWatch.Elapsed;

                        string elapsedTime = "The time it took to decompress file " + ts.Milliseconds + " ms";
                        Console.WriteLine(elapsedTime);
                    }
                }
            }
        }

    }
}
