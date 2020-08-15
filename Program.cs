using System;
using System.Collections.Generic; // to include lists

namespace c_assignment_crud_3mrfouad
{
    class Program
    {
        static void Main(string[] args)
        {
            // Req#1: Allow the user to input data of your choice (names, numbers, etc).
            
            List<string> names = new List<string>();
            bool exitFlag = false;
            int listCntr = 0;
            Console.WriteLine("\nCapacity: {0}", names.Capacity);

            do
            {
                exitFlag = getUsrInpt(names);
                listCntr++;

            }while(listCntr <10 && !exitFlag);

            //REQ#2: Allow the user to display the dataset with the items numbered
            Console.WriteLine("\nCapacity: {0}", names.Count);

            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine("Record: {0} ", (i + 1) + "  Name: " + names[i]);
            }
        }

        /* Get user input*/
        static bool getUsrInpt(List<string> strList)

        {
            bool digDetected = false, exitFlag = false;
            string tempStr = "";
            // validating if user entered exit sequance
           
            do 
            {
                digDetected = false;
                Console.WriteLine("Please Enter a Name:");
                tempStr = Console.ReadLine(); //store user input in temporary string
                tempStr = tempStr.Trim();
                for (int i = 0; i < tempStr.Length; i++)
                {
                    if (!(Char.IsLetter(tempStr[i]) || Char.IsWhiteSpace(tempStr[i])))
                    {
                        digDetected = true;
                    }
                }

                if (digDetected)
                {
                    Console.WriteLine("Validation Error: illegal character");
                }
                else if (tempStr.ToLower() != "exit")
                {
                    strList.Add(tempStr);
                }
                else
                {
                    exitFlag = true;
                }

            }while(digDetected);
            
            return exitFlag;

        }
    }
}