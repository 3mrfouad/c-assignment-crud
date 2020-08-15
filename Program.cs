using System;
using System.Collections.Generic; // to include lists

namespace c_assignment_crud_3mrfouad
{
    class Program
    {
        static void Main(string[] args)
        {
            // Req#1: Allow the user to input data (names).
            List<string> names = new List<string>();
            menuOptns(names);
            //Console.WriteLine();
            //REQ#2: Allow the user to display the dataset with the items numbered

        }
        /*Menu*/


        static void menuOptns(List<string> strList)
        {
           bool progExitFlg=false;
           int menuChoice;
           bool validInt;
            do
            {
                Console.WriteLine("\n\nSelect Menu Option: \n1. Enter Names to Database\n2. Display Names Database\n3. Exit");
                validInt = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);
                if (validInt)
                {
                    switch (menuChoice)
                    {
                        case 1:
                            getUsrInpt(strList);
                            break;
                        case 2:
                            if (strList.Count!=0)
                               { 
                                Console.WriteLine("\nCapacity: {0}", strList.Count);
                                for (int i = 0; i < strList.Count; i++)
                                {
                                    Console.WriteLine("Record [{0}", (i + 1)+"]" + "   Name:" + strList[i]);
                                }
                               }
                               else
                               {
                                    Console.WriteLine("\nEmtpy Records, try menu option 1 to add new records");
                               }
                            break;
                        case 3:
                            progExitFlg = true;
                            break;
                        default:
                            Console.WriteLine("\nValidation Error: invalid menu choice");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Validation Error: unexpected input");
                }
            }while(!progExitFlg);
        }

        /* Get user input*/
        static void getUsrInpt(List<string> strList)

        {
            int listCntr = 0;
            Console.WriteLine("\nCapacity: {0}", strList.Capacity); // for testing
            bool digDetected = false, exitFlag = false;
            string tempStr = "";
            // validating if user entered exit sequance
            do
            {
                //exitFlag = getUsrInpt(names);
                listCntr++;

                do
                {
                    digDetected = false;
                    Console.Write("Please Enter a Name:");
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
                        Console.WriteLine("\nValidation Error: invalid character used\n");
                    }
                    else if (tempStr.ToLower() != "exit")
                    {
                        strList.Add(tempStr);
                    }
                    else
                    {
                        exitFlag = true;
                    }

                } while (digDetected);

                //return exitFlag;

            } while (listCntr < 10 && !exitFlag);

        }
    }
}