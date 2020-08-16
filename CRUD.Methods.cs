using System;
using System.Collections.Generic; // to include lists    

namespace c_assignment_crud_3mrfouad_methods
{
    class CRUD_Methods
    {
        //-------------------
        //Menu options method
        //-------------------
        public static void menuOptns(List<string> strList)
        {
            bool progExitFlg = false;
            int menuChoice;
            bool validMenuChoice;
            do
            {
                //Display menu options 1,2, and 3
                Console. Clear();
                Console.WriteLine("\nSelect Menu Option: \n1. Enter new records\n2. Display records\n3. Edit records \n4. Exit");
                //Validate user input to be a valid 1,2 or 3
                // Using ReadKey method to enhance the UX, the user won't need to press enter, and the letter won't be displayed on the console
                validMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);

                if (validMenuChoice) // If valid digit was pressed by the user
                {
                    switch (menuChoice)
                    {
                        case 1: // record entry mode
                            newRecordInpt(strList, 0, 1);
                            break;
                        case 2: // records display mode
                            displayRecords(strList, true);
                            break;
                        case 3: // edit record mode
                            if (strList.Count != 0)
                            {
                                editRecords(strList);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to edit\nPress <ENTER> to go back to the main menu");
                                Console.ReadLine();
                            }
                            break;
                        case 4: // exit the program
                            progExitFlg = true;
                            break;
                        default: // in case of integer other than 1, 2 or 3
                            Console.Clear();
                            Console.WriteLine("\n\nValidation Error: invalid menu choice\nPress <ENTER> to go back to the main menu");
                            Console.ReadLine();
                            break;
                    }
                }
                else // in case nondigit key pressed by by the user
                {
                    Console.Clear();
                    Console.WriteLine("\n\nValidation Error: unexpected input\nPress <ENTER> to go back to the main menu");
                    Console.ReadLine();
                }
            } while (!progExitFlg);
        }

        //-----------------------------------
        //Edit Database Records
        //-----------------------------------

        public static void editRecords(List<string> strList)
        {
            displayRecords(strList, false);
            Console.Write("\n1. Edit using record ID\n2. Edit using record value\n3. Exit\n");
            bool editMenuExitFlg = false;
            int menuChoice, subMenuChoice;
            bool validMenuChoice, validSubMenuChoice;
            int recordID = 0;
            bool validRecordID;
            string recordValue;
            do
            {
                validMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);
                if (validMenuChoice) // If valid digit was pressed by the user
                {
                    switch (menuChoice)
                    {
                        case 1: // edit using record ID
                            do
                            {
                                Console.Write("\n\nEnter a record ID to edit:");
                                validRecordID = int.TryParse(Console.ReadLine(), out recordID); // futher validation needed
                                if (validRecordID)
                                {
                                    if (recordID > 0 && recordID <= strList.Count)
                                    {
                                        newRecordInpt(strList, recordID, 2);
                                        Console.Write("\n1. Edit another record\n2. Return to main menu");
                                        validSubMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out subMenuChoice);
                                        if (!validSubMenuChoice)
                                        {
                                            Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                                        }
                                        else
                                        {
                                            switch (subMenuChoice)
                                            {
                                                case 1:
                                                    editRecords(strList);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("\nValidation Error: Record doesn't exit");
                                        validRecordID = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nValidation Error: unexpected input, non-numerical value is used");
                                }
                            } while ((recordID > strList.Count + 1 || !validRecordID) && strList.Count > 0);
                            editMenuExitFlg = true;
                            break;
                        case 2: // edit using record value
                            do
                            {
                                Console.Write("\n\nEnter a record Value to edit:");
                                recordValue = Console.ReadLine(); //futher validation needed
                                recordID = strList.IndexOf(recordValue);
                                validRecordID = recordID == -1 ? false : true;
                                if (validRecordID)
                                {
                                    recordID++;
                                    if (recordID > 0 && recordID <= strList.Count)
                                    {
                                        newRecordInpt(strList, recordID, 2);
                                        Console.Write("\n1. Edit another record\n2. Return to main menu");
                                        validSubMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out subMenuChoice);
                                        if (!validSubMenuChoice)
                                        {
                                            Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                                        }
                                        else
                                        {
                                            switch (subMenuChoice)
                                            {
                                                case 1:
                                                    editRecords(strList);
                                                    break;
                                                default:
                                                    break;
                                            }

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("\nValidation Error: Record doesn't exit");
                                        validRecordID = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nValidation Error: Record doesn't exit");
                                }
                            } while ((recordID > strList.Count + 1 || !validRecordID) && strList.Count > 0);
                            editMenuExitFlg = true;
                            break;

                        case 3: // exit the program
                            editMenuExitFlg = true;
                            break;
                        default: // in case of integer other than 1, 2 or 3
                            Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                            break;
                    }
                }
                else // in case nondigit key pressed by by the user
                {
                    Console.WriteLine("Validation Error: unexpected input, try agin:");
                }
            } while (!editMenuExitFlg);
        }

        //-----------------------------------
        //Display Database Records
        //-----------------------------------
        public static void displayRecords(List<string> strList, bool clearConsole)
        {
            if (strList.Count != 0)
            {
                Console. Clear();
                Console.WriteLine("\nThe Database has: {0}", strList.Count + " records");
                for (int i = 0; i < strList.Count; i++)
                {
                    Console.WriteLine("Record [{0}", (i + 1) + "]" + "    Name:" + strList[i]);
                }
            if (clearConsole)
            {
                Console.WriteLine("Press <ENTER> to proceed");
                Console.ReadLine();
            }
            }
            else // in case the records are empty, let the user know
            {
                Console.Clear();
                Console.WriteLine("\nEmtpy Database, no records to display\nPress <ENTER> to go back to the main menu");
                Console.ReadLine();
            }
            
        }

        //-----------------------------------
        //Get and validate user inputs method
        //-----------------------------------
        public static void newRecordInpt(List<string> strList, int recordID, int newOrEdit)

        {
            if (newOrEdit == 1)
            {
                Console. Clear();
                Console.WriteLine("\nYour next record ID is: {0}", strList.Count+1); // for testing
            }

            Console.WriteLine("\nWhen finished, Type <Exit> to return to the main menu");
            // variables definition
            bool digtdetct = false, exitFlag = false;
            string tempStr = "";
            // validating if user entered exit sequance

            if (strList.Count == 10 && newOrEdit == 1) // error Msg. in case of data entery while the list is already fully populated with 10 records
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
                            if (newOrEdit == 1)
                            {
                                strList.Add(tempStr);
                            }
                            else if (newOrEdit == 2 && recordID == 0)
                            {
                                strList.Add(tempStr);
                                exitFlag = true;
                            }
                            else
                            {
                                strList[recordID - 1] = tempStr;
                                exitFlag = true;
                            }
                        }
                        else // in case exit was entered, stop the data entry mode
                        {
                            exitFlag = true;
                        }

                    } while (digtdetct);

                } while (!exitFlag && strList.Count < 10); // list count validation less than 10 records
            }
            return;
        }
    }
}