using System;
using System.Collections.Generic; // to include lists
using c_assignment_crud_3mrfouad_methods;
using System.Threading.Tasks;
using System.IO;
//using c_assignment_crud_3mrfouad_methods_music;

namespace c_assignment_crud_3mrfouad
{
    class Program
    {
        /* 
        ---------------
        CRUD DOCSTRING
        ---------------
            Title:C# Introduction Assignment – CRUD Assignment

            Purpose: The program is for small database managment is offers menu to
            the user to choose between entering new records (N.B., up to 10, then made dynamic), displaying the existing records,
            editing and deleting from exiting records. Also, if has file management features allowing import and export features to 
            store database on the hard drive for long term storage.

            Note: I've kept the methods in separate file (CRUD.Methods.cs) for better organization.

            Author: Amr Fouad

            Date of last edit: August 17, 2020 03:30 am

            Resources:
            In the project folder, Resources.txt
            */

        static void Main(string[] args)
        {
            int i = 0;
            Console.Clear();
            //Creating list of dataset records
            List<string> names = new List<string>();
            //Upgrade the console color scheme
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\nWelcome to CRUD Database\nThrough this program, you can enter, edit, view the Database records\nNote:Total number of basic Database records is 10, yet made dynamic in later upgrades");
            do // music at the begining :-)
            {
                c_assignment_crud_3mrfouad_methods_music.Sample.PlayMusic();
                Task.Delay(3000);
                i++;
            }
            while(i<=1);
            Console.WriteLine("\nPress any key to Proceed");
            Console.ReadKey();
            //Calling menu options method
            CRUD_Methods.MenuOptions(names);
            Console.ResetColor();
        }

    }
}
