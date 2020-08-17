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
            Title:C# Introduction Assignment – CRUD Assignment

            Purpose: The program is for small database managment is offers menu to
            the user to choose between entering new records up to 10 and displaying the existing records.

            Author: Amr Fouad

            Date of last edit: August 16, 2020 12:00 am

            Resources:
            In the project folder, Resources.txt
            */

        static void Main(string[] args)
        {
            //int i = 0;
            Console.Clear();
            //Creating list of dataset records
            List<string> names = new List<string>();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\nWelcome to CRUD Database\nThrough this program, you can enter, edit, view the Database records\nNote:Total number of basic Database records is 10");
            /*do
            {
                c_assignment_crud_3mrfouad_methods_music.Sample.PlayMusic();
                Task.Delay(3000);
                i++;
            }
            while(i<=1); */ 
            Console.WriteLine("\nPress <Enter> to Proceed");
            Console.ReadKey();
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            //Calling menu options method
            CRUD_Methods.MenuOptions(names);
            Console.ResetColor();
        }

    }
}
