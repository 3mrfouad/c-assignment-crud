using System;
using System.Collections.Generic; // to include lists    
using System.IO;

namespace c_assignment_crud_3mrfouad_methods
{
    class CRUD_Methods
    {

        //-------------------
        //Import Records
        //-------------------
        /*         public static ExportRecords(List<string> strList)
                {
                    string fileName;
                    Console.Write("/nEnter a file name to export Records to Database");
                    fileName = Console.ReadLine(); // add filename validation
                    foreach (string str in strList)
                    {
                        System.IO.File.WriteAllText(@"DataBaseRecords.txt", str, "\n");
                    }
                    Console.Write("Records were successfully exported");

                } */
        //-------------------
        //Export Records
        //-------------------
        public static void ImportRecords(List<string> strList)
        {

            int i = 0;
            char tryAnotherFileName;
            string fileName;
            Console.Clear();
            

            do
            {   
                tryAnotherFileName = 'N';
                Console.Clear();
                Console.Write("\nEnter a file name to import Records to Database: ");
                fileName = Console.ReadLine().Trim(); // add filename validation
                try
                {
                    var file = new StreamReader(fileName);
                    do

                    {
                        strList.Add(file.ReadLine());
                        //Console.Write("\nInside For Loop\n"); for troubleshootin
                        i++;
                    } while (i < 10 && file.ReadLine() != null);

                    file.Close(); // add file close validation
                    Console.Clear();
                    Console.WriteLine("\n[" + fileName + "] Records were successfully imported");
                    ReadRecords(strList, 3);
                }
                catch
                {
                    Console.WriteLine("\nThe file name can't be found, make sure your enter a correct file name including its extension\nExample: RecordsFile.txt");
                    Console.WriteLine("\nPress <y> to try another file name\nOr press anykey to go to main menu");
                    tryAnotherFileName = Char.ToUpper(Console.ReadKey(true).KeyChar);
                }

            } while (tryAnotherFileName == 'Y');

        }
        //-------------------
        //Search Record for Duplicates Method
        //-------------------
        public static bool SearchRecord(List<string> strList, string recordValue)
        {
            List<string> tempStr = new List<string>();
            bool existingRecordFlag;
            bool validRecordID;
            int recordID;
            tempStr = strList;
            foreach (string str in tempStr)
            {
                str.ToLower();
            }
            recordID = tempStr.IndexOf(recordValue.ToLower());
            validRecordID = recordID == -1 ? false : true;
            if (validRecordID)
            {
                existingRecordFlag = true;
            }
            else
            {

                existingRecordFlag = false;

            }
            return existingRecordFlag;
        }
        //-------------------
        //Detele Record Method
        //-------------------
        public static void DeleteRecord(List<string> strList)
        {
            ReadRecords(strList, 0);
            Console.Write("\n1. Delete using record ID\n2. Delete using record value\n3. Exit\n");
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
                                Console.Write("\n\nEnter a record ID to delete:");
                                validRecordID = int.TryParse(Console.ReadLine(), out recordID); // futher validation needed
                                if (validRecordID)
                                {
                                    if (recordID > 0 && recordID <= strList.Count)
                                    {
                                        strList.RemoveAt(recordID - 1);
                                        strList.Sort();
                                        // CreateRecord(strList, recordID, 2);
                                        if (strList.Count == 0)
                                        {
                                            Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                            Console.ReadKey(true);

                                            Console.Write("\n1. Delete another record\n2. Return to main menu\n");
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
                                                        DeleteRecord(strList);
                                                        break;
                                                    default:
                                                        break;
                                                }

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
                                Console.Write("\n\nEnter a record Value to delete:");
                                recordValue = Console.ReadLine(); //futher validation needed
                                recordID = strList.IndexOf(recordValue);
                                validRecordID = recordID == -1 ? false : true;
                                if (validRecordID)
                                {
                                    recordID++;
                                    if (recordID > 0 && recordID <= strList.Count)
                                    {
                                        strList.RemoveAt(recordID - 1);
                                        strList.Sort();
                                        // CreateRecord(strList, recordID, 2);
                                        if (strList.Count == 0)
                                        {
                                            Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                            Console.ReadKey(true);
                                        }
                                        else
                                        {
                                            Console.Write("\n1. Edit another record\n2. Return to main menu\n");
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
                                                        DeleteRecord(strList);
                                                        break;
                                                    default:
                                                        break;
                                                }

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
        //-------------------
        //Menu Options Method
        //-------------------
        public static void MenuOptions(List<string> strList)
        {
            bool progExitFlg = false;
            int menuChoice;
            bool validMenuChoice;
            do
            {
                //Display menu options 1,2, and 3
                Console.Clear();
                Console.WriteLine("\nSelect Menu Option: \n1. Enter new records\n2. Display records\n3. Edit records\n4. Delete records\n5. Import Database\n6. Export Database\n7. Exit");
                //Validate user input to be a valid 1,2 or 3
                // Using ReadKey method to enhance the UX, the user won't need to press enter, and the letter won't be displayed on the console
                validMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);

                if (validMenuChoice) // If valid digit was pressed by the user
                {
                    switch (menuChoice)
                    {
                        case 1: // record entry mode
                            CreateRecord(strList, 0, 1);
                            break;
                        case 2: // records display mode
                            ReadRecords(strList, 1);
                            break;
                        case 3: // edit record mode
                            if (strList.Count != 0)
                            {
                                UpdateRecords(strList);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                Console.ReadKey(true);
                            }
                            break;
                        case 4:
                            if (strList.Count != 0)
                            {
                                DeleteRecord(strList);
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                Console.ReadKey(true);
                            }
                            break;
                        case 5:
                            ImportRecords(strList);
                            break;
                        /*  case 6:
                             ExportRecords(strList);
                             break; */
                        case 7: // exit the program
                            progExitFlg = true;
                            break;
                        default: // in case of integer other than 1, 2 or 3
                            Console.Clear();
                            Console.WriteLine("\n\nValidation Error: invalid menu choice\nPress anykey to go back to the main menu\n");
                            Console.ReadKey(true);
                            break;
                    }
                }
                else // in case nondigit key pressed by by the user
                {
                    Console.Clear();
                    Console.WriteLine("\n\nValidation Error: unexpected input\nPress any key to go back to the main menu\n");
                    Console.ReadKey(true);
                }
            } while (!progExitFlg);
        }

        //------------------------
        //Update Database Records
        //------------------------

        public static void UpdateRecords(List<string> strList)
        {
            bool editMenuExitFlg = false;
            int menuChoice, subMenuChoice;
            bool validMenuChoice, validSubMenuChoice;
            int recordID = 0;
            bool validRecordID, exitFlag = false;
            string recordValue;
            ReadRecords(strList, 0);

            Console.Write("\n1. Edit using record ID\n2. Edit using record value\n3. Exit\n");

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
                                        exitFlag = CreateRecord(strList, recordID, 2);
                                        if (!exitFlag)
                                        {
                                            Console.Write("\n1. Edit another record\n2. Return to main menu\n");
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
                                                        UpdateRecords(strList);
                                                        break;
                                                    default:
                                                        break;
                                                }

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
                            } while ((recordID > strList.Count + 1 || !validRecordID) && strList.Count > 0 && !exitFlag);
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
                                        CreateRecord(strList, recordID, 2);
                                        Console.Write("\n1. Edit another record\n2. Return to main menu\n");
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
                                                    UpdateRecords(strList);
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



        //-----------------------
        //Read (Dispaly) Records
        //-----------------------
        public static void ReadRecords(List<string> strList, int clearConsole)
        {
            if (strList.Count != 0)
            {

                if (clearConsole < 3)
                {
                    Console.Clear();
                }
                Console.WriteLine("\nThe Database has: {0}", strList.Count + " records");
                for (int i = 0; i < strList.Count; i++)
                {
                    Console.WriteLine("Record [{0}", (i + 1) + "]" + "    Name:" + strList[i]);
                }
                if (clearConsole == 1 || clearConsole == 3)
                {
                    Console.WriteLine("Press any key to proceed");
                    Console.ReadKey(true);
                }
            }
            else // in case the records are empty, let the user know
            {
                Console.Clear();
                Console.WriteLine("\nEmtpy Database, no records to display\nPress any key to go back to the main menu\n");
                Console.ReadKey(true);
            }

        }

        //------------------------------------------
        //Create Record, Validate User Input Method
        //------------------------------------------
        public static bool CreateRecord(List<string> strList, int recordID, int newOrEdit)

        {
            if (newOrEdit == 1)
            {
                Console.Clear();
                Console.WriteLine("\nYour next record ID is: {0}", strList.Count + 1); // for testing
            }

            Console.WriteLine("\nWhen finished, Type <Exit> to return to the main menu\n");
            // variables definition
            bool digtdetct = false, exitFlag = false, existingRecordFlag = false;
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
                            existingRecordFlag = SearchRecord(strList, tempStr);
                            if (!existingRecordFlag)
                            {
                                if (newOrEdit == 1)
                                {
                                    strList.Add(tempStr);
                                    strList.Sort();
                                }
                                else if (newOrEdit == 2 && recordID == 0)
                                {
                                    strList.Add(tempStr);
                                    strList.Sort();
                                    exitFlag = true;
                                }
                                else
                                {
                                    strList[recordID - 1] = tempStr;
                                    strList.Sort();
                                    exitFlag = true;
                                }
                            }
                            else
                            {
                                Console.WriteLine("\nValidation Error: Record already exits, try again or type <Exit> if finished\n");
                                existingRecordFlag = false;
                            }
                        }
                        else // in case exit was entered, stop the data entry mode
                        {
                            exitFlag = true;
                        }

                    } while (digtdetct);

                } while (!exitFlag && strList.Count < 10); // list count validation less than 10 records
            }
            return exitFlag;
        }


    }


}