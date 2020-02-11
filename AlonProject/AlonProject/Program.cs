using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlonProject
{
    class Program
    {
        public static Folder root = new Folder(@"C:\");

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Alon Shani Project, if you want to exit type exit ");
            Console.WriteLine("The commands are those : ");
            Console.WriteLine("'mkdir' to create folder");
            Console.WriteLine("'mkfile' to create file");
            Console.WriteLine("'ls' to get list of all data are availble under you path");
            Console.WriteLine("'cd' to access to spesific folder folder");
            Console.WriteLine("'cd..' to revert one folder");

            string readline = String.Empty;
            Folder currentFolder = root;
            do
            {
                try
                {
                    Console.Write("{0}>", currentFolder.FullPath);
                    readline = Console.ReadLine();
                    string[] segments = readline.Split(' ');

                    if (segments.Length >= 1)
                    {
                        switch (segments[0].ToLower())
                        {
                            // Create directory
                            case "mkdir":
                                if (!string.IsNullOrEmpty(segments[1]))
                                {
                                    currentFolder.CreateFolder(segments[1]);
                                }
                                else Console.WriteLine("Please input folder name");
                                break;

                            // Create a file
                            case "mkfile":
                                if (!string.IsNullOrEmpty(segments[1]))
                                {
                                    currentFolder.CreateFile(segments[1]);
                                }
                                else Console.WriteLine("Please input file name");
                                break;

                            // List of all files and folders
                            case "ls":
                                Console.WriteLine(currentFolder);
                                break;

                            // Entered to spesific folder
                            case "cd":
                                currentFolder = currentFolder.GetSubFolder(segments[1]);
                                break;

                            // Reverse one folder
                            case "cd..":
                                if (currentFolder.ParentFolder != null)
                                {
                                    currentFolder = currentFolder.ParentFolder;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(String.Format("Error: {0}", ex.Message));
                }
            }

            while (!readline.ToLower().Equals("exit"));
        }
    }
}
