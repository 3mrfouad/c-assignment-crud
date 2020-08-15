using System;
using System.Collections.Generic; // to include lists

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

            //Creating list of dataset records
            List<string> names = new List<string>();
            Console.WriteLine("\nWelcome to CRUD Database\nThrough this program, you can enter, edit, view the Database records\nNote:Total number of basic Database records is 10");
            //Calling menu options method
            menuOptns(names);

        }

        //-------------------
        //Menu options method
        //-------------------
        static void menuOptns(List<string> strList)
        {
            bool progExitFlg = false;
            int menuChoice;
            bool validInt;
            do
            {
                //Display menu options 1,2, and 3
                Console.WriteLine("\nSelect Menu Option: \n1. Enter Names to Database\n2. Display Names Database\n3. Exit");
                //Validate user input to be a valid 1,2 or 3
                // Using ReadKey method to enhance the UX, the user won't need to press enter, and the letter won't be displayed on the console
                validInt = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);

                if (validInt) // If valid digit was pressed by the user
                {
                    switch (menuChoice)
                    {
                        case 1: // data entry mode
                            getUsrInpt(strList);
                            break;
                        case 2: // data display mode
                            displayRecords(strList);
                            break;
                        case 3: // exit the program
                            progExitFlg = true;
                            break;
                        default: // in case of integer other than 1, 2 or 3
                            Console.WriteLine("\nValidation Error: invalid menu choice");
                            break;
                    }
                }
                else // in case nondigit key pressed by by the user
                {
                    Console.WriteLine("Validation Error: unexpected input");
                }
            } while (!progExitFlg);
        }

        //-----------------------------------
        //Display Database Records
        //-----------------------------------
                static void displayRecords(List<string> strList)
                {
                    if (strList.Count != 0)
                            {
                                Console.WriteLine("\nCapacity: {0}", strList.Count);
                                for (int i = 0; i < strList.Count; i++)
                                {
                                    Console.WriteLine("Record [{0}", (i + 1) + "]" + "    Name:" + strList[i]);
                                }
                            }
                            else // in case the records are empty, let the user know
                            {
                                Console.WriteLine("\nEmtpy Records, try menu option 1 to add new records");
                            }
                }

        //-----------------------------------
        //Get and validate user inputs method
        //-----------------------------------
        static void getUsrInpt(List<string> strList)

        {
            Console.WriteLine("\nCapacity: {0}", strList.Count); // for testing
            Console.WriteLine("\nType <Exit> to end");
            // variables definition
            bool digtdetct = false, exitFlag = false;
            string tempStr = "";
            // validating if user entered exit sequance

            if (strList.Count == 10) // error Msg. in case of data entery while the list is already fully populated with 10 records
            {
                Console.WriteLine("\nRecords Maxed Out, try again later when we update the program with delete,edit option\n");
            }
            else // otherwise, there is space for new records to be added
            {

                do
                {

                    do
                    {
                        digtdetct = false; // bool to identify if characters other than letters exists within the entered names
                        Console.Write("Please Enter a Name:");
                        tempStr = Console.ReadLine(); //store user input in temporary string
                        tempStr = tempStr.Trim(); // clean up white spaces leading/trialing
                        for (int i = 0; i < tempStr.Length; i++) // search for non letters (or white spaces) within the name
                        {
                            if (!(Char.IsLetter(tempStr[i]) || Char.IsWhiteSpace(tempStr[i])))
                            {
                                digtdetct = true;
                            }
                        }

                        if (digtdetct) // in case non letters were entered by the user
                        {
                            Console.WriteLine("\nValidation Error: invalid character was used\n");
                        }
                        else if (String.IsNullOrWhiteSpace(tempStr)) // in case of just white spaces entered by the user
                        {
                            Console.WriteLine("\nValidation Error: empty record was entered\n");
                        }
                        else if (tempStr.ToLower() != "exit") // in case of all validation pass and the name is not exit
                        {
                            strList.Add(tempStr);
                        }
                        else // in case exit was entered, stop the data entry mode
                        {
                            exitFlag = true;
                        }

                    } while (digtdetct);

                } while (!exitFlag && strList.Count < 10); // list count validation less than 10 records
            }

        }
    }
}