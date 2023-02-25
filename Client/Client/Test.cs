﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Client.ConnectServer;

namespace Client
{
    internal class Test
    {
        // constants
        const int SUCCESS = 0;
        const int FAILURE = 1;

        private static string path = @".\MyTest.txt";

        public static void TryStartClient(string IP_address, int IP_port)
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("[[ Test 0 ]] Connect to the Server");

            if (StartClient(IP_address, IP_port) == SUCCESS)
            {
                string logConnected = "Socket connected to " + IP_address + ":" + IP_port;
                SendLog(Level.Info, logConnected);
                Console.WriteLine("Result: Success");
            }
            else
            {
                Console.WriteLine("Result: Fail");
                Console.WriteLine("-> Fail to connect to " + IP_address + ":" + IP_port);
            }

        }

        public static void Test1()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("[[ Test 1 ]] Create a file");


            if (File.Exists(path))
            {
                Console.WriteLine("Warning: File exists already. Delete the file.");
                SendLog(Level.Warning, "File exists already.");

                try
                {
                    File.Delete(path);
                    SendLog(Level.Info, "Delete the file.");
                }
                catch (Exception)
                {
                    SendLog(Level.Error, "Fail to delete the file in " + path);
                }
            }

            try
            {
                using (FileStream fs = File.Create(path))
                {
                    AddText(fs, "This is some text");
                    AddText(fs, "This is some more text,");
                    AddText(fs, "\r\nand this is on a new line");
                    AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                    for (int i = 1; i < 120; i++)
                    {
                        AddText(fs, Convert.ToChar(i).ToString());

                    }
                }

                SendLog(Level.Info, "Success to create a file in " + path);
                Console.WriteLine("Result: Success");
            }
            catch (Exception)
            {
                Console.WriteLine("Result: Fail");
                Console.WriteLine("-> Fail to create a file in " + path);
                SendLog(Level.Error, "Fail to create a file in " + path);
            }

        }

        public static void Test2()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("[[ Test 2 ]] Delete the file");

            try
            {
                File.Delete(path);

                SendLog(Level.Info, "Delete the file.");
                Console.WriteLine("Result: Success");

            }
            catch (Exception)
            {
                SendLog(Level.Error, "Fail to delete the file in " + path);
                Console.WriteLine("Result: Fail");

            }

        }

        public static void Test3()
        {
            Console.WriteLine("---------------------------------------------------");
            Console.WriteLine("[[ Test 3 ]] Open a file that doesn't exist");



            Console.WriteLine("---------------------------------------------------");
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

    }
}