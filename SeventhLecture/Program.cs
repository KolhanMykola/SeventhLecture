using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace SeventhLecture
{
    class Program
    {
        static FileInfo file = null;
        delegate FileStream Create();
        delegate FileInfo Copy(string path= @"E:\SigmaCourses\7\11.txt", bool a = true);
        delegate void Delete();

        static void Main(string[] args)
        {
            string path = null;
            Console.WriteLine("Please input file operation create / delete / copy < file path >");
            string input = Console.ReadLine();
            string[] splited = input.Split(' ');

            if (splited.Length != 2)
            {
                Console.WriteLine("Entered data with one space");
                Console.ReadLine();
                return;
            }
            
            string command = splited[0];
            path = splited[1];
            file = new FileInfo(path);            

            switch (command)
            {
                case "create":
                    {
                        Create create = file.Create;
                        Confirm(create);
                        break;
                    }
                case "copy":
                    {
                        Copy copy = file.CopyTo;
                                /// Confirm(copy((@"E:\SigmaCourses\7\11.txt", true))                         
                                /// executed(copy file) here, but not in Confirm method                        
                        Confirm(copy);
                        break;
                    }
                case "delete":
                    {
                        Delete delete = file.Delete;
                        Confirm(delete);
                        break;
                    }
                default:
                    {
                        Console.WriteLine("Unrecognized command");
                        break;
                    }
            }            

            Console.ReadLine();
        }

        private static void Confirm(dynamic method)
        {
            try
            { 
                if (file.Exists)
                {
                    Console.WriteLine("Do you want to {0}? y/n", method.Method);
                    string write = Console.ReadLine();
                    switch (write.ToLower())
                    {
                        case "y":                            
                            method();
                            Console.WriteLine("File was {0} at {1}", method.Method, method.Target);
                            break;                            
                        case "n":                            
                            Console.WriteLine("File wasn`t {0}", method.Method);
                            break;                           
                        default:
                            Console.WriteLine("Unrecognized command");
                            break;
                    }
                }
                else
                {
                    method();//on copy|delete check?                   
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("File is running by anothet application and can`t be deleted or changed");
                Console.ReadLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Uncorrect path to file");
                Console.ReadLine();
            }
            catch (PathTooLongException)
            {
                Console.WriteLine("Path is longer than 256 chars");
                Console.ReadLine();
            }
        }
    }
}
