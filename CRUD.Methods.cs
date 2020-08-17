using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;



namespace c_assignment_crud_3mrfouad_methods
{
    class CRUD_Methods
    {

        //---------------------------------------
        //Export Records Method (Write to a File)
        //---------------------------------------
        public static void ExportRecords(List<string> strList)
        {
            //Variables definition
            char tryAnotherFileName;
            string fileName;
            Console.Clear();
            do
            {   //get file name from user
                tryAnotherFileName = 'N';
                Console.Clear();
                Console.Write("\nHint: Enter ExportRecords.txt\nEnter a file name to export records to Database: ");
                fileName = Console.ReadLine().Trim(); // trim white spaces
                // filename validation
                if (new FileInfo(fileName).Exists == true) // check if file exists
                {
                    Console.WriteLine("The file already exits", fileName);
                    Console.WriteLine("\nSelect an Action:\n[1] OVERRIDDE\n[2] Try Differnt File Name\n[3] Cancel and Return to Main Menu");
                    //get user input (first key pressed)
                    char fileOverride = Console.ReadKey(true).KeyChar;
                    switch (fileOverride)
                    {
                        case '1': // confirm user selection to override (dangerous action)
                            Console.WriteLine("\nAre you sure? this action can't be undone! press [y] to confirm\nOr press anykey to exit");
                            if (Char.ToLower(Console.ReadKey(true).KeyChar) == 'y')
                            {
                                strList.Sort(); // sort list before writing to the file
                                using (StreamWriter file = new StreamWriter(fileName)) // open the file for writing
                                {
                                    for (int i = 0; i < strList.Count; i++) // loop to write each item from the list to new line in the file
                                    {
                                        file.WriteLine(strList[i]);
                                    }
                                }
                                // close file after writing
                                // Let the user know that all went well
                                Console.WriteLine("\nRecords were successfully exported to [" + fileName + "]");
                                ReadRecords(strList, 3); // show the records on the console
                            }
                            break;
                        case '2': // option 2 try don't override and try another file name
                            tryAnotherFileName = 'Y';
                            break;
                        case '3': // exit
                            break;
                        default: // invalid choice, no loop as it is dangerous action and if the user is not paying attention better to return him to the main menu
                            Console.WriteLine("\nValidation Error: invalid input");
                            Console.WriteLine("\nPress anykey to return to main menu");
                            Console.ReadKey(true);
                            break;
                    }
                }
                else // in case the file doesn't exit, create it and write to it, repeated code from above
                {
                    using (StreamWriter file = new StreamWriter(fileName))
                    {
                        strList.Sort();
                        for (int j = 0; j < strList.Count; j++)
                        {
                            file.WriteLine(strList[j]);
                        }
                    }
                    //close file after writing
                    Console.WriteLine("\nRecords were successfully exported to [" + fileName + "]");
                    ReadRecords(strList, 3);
                }
            } while (tryAnotherFileName == 'Y');
        }
        //---------------------------------------
        //Import Records Method (Read from a File)
        //---------------------------------------
        public static void ImportRecords(List<string> strList)
        {   //Variables definition
            int i = 0;
            char tryAnotherFileName;
            string fileName;
            Console.Clear();
            do
            {
                tryAnotherFileName = 'N';
                Console.Clear();
                //get file name from user
                Console.Write("\n\nHint: Enter ImportRecords.txt\nEnter a file name to import records to Database: ");
                fileName = Console.ReadLine().Trim(); // trim white spaces
                try
                {
                    var file = new StreamReader(fileName); // open the file for writing
                    if (new FileInfo(fileName).Length != 0) // check if the file has content > 0 kbyte
                    {
                        string[] fileLines = System.IO.File.ReadAllLines(fileName); // read the file content to a string
                        foreach (string line in fileLines) //offload the string thru foreach into the list
                        {
                            strList.Insert(i, line);
                            i++;
                        }
                        file.Close(); //close the file after read
                        //let the user know that all went well and the the records were imported
                        Console.WriteLine("\nRecords were successfully imported to [" + fileName + "]");
                        ReadRecords(strList, 3); // display the imported records
                    }
                    else if (new FileInfo(fileName).Length == 0) // file doesn't have content == 0byte
                    {//let the user know that nothing to be imported from the file
                        Console.WriteLine("\n[" + fileName + "] has no records to import");
                        Console.WriteLine("\nPress [y] to try another file name\nOr press anykey to go to main menu");
                        tryAnotherFileName = Char.ToUpper(Console.ReadKey(true).KeyChar);
                    }
                }
                catch (Exception ex) // when file name doesn't exit, prompt the user to enter another one or exit
                {
                    Console.WriteLine("\n" + ex.Message);
                    Console.WriteLine("\nMake sure your enter a correct file name including its extension\nExample: recordsFile.txt");
                    Console.WriteLine("\nPress [y] to try another file name\nOr press anykey to go to main menu");
                    tryAnotherFileName = Char.ToUpper(Console.ReadKey(true).KeyChar);
                }
            } while (tryAnotherFileName == 'Y'); // try another file loop
        }
        //-----------------------------------------------
        //Search Validation for Duplicate Records Method
        //-----------------------------------------------
        public static bool SearchRecord(List<string> strList, string recordValue)
        {   //variables definition, including temp string list
            bool validRecordID;
            int recordID;
            // temp string list to perform the .tolower operations without missing up the original records
            List<string> tempStr = strList.ToList();
            for (int i = 0; i < tempStr.Count; i++) //foreach(string str in tempStr)
            {
                tempStr[i] = tempStr[i].ToLower(); // Converting the temp string to lower case
            }
            recordID = tempStr.IndexOf(recordValue.ToLower()); // get the record ID
            validRecordID = recordID == -1 ? false : true; // check if valid record
            return validRecordID;// return the exiting record flag to the calling code block
        }
        //----------------------
        //Detele Records Method
        //----------------------
        public static void DeleteRecord(List<string> strList)
        {
            //variables definition
            bool editMenuExitFlg = false;
            int menuChoice, subMenuChoice;
            bool validMenuChoice, validSubMenuChoice;
            int recordID = 0;
            bool validRecordID;
            string recordValue;
            // call read records to display the available records as visual aid to the user while deleting
            ReadRecords(strList, 0);
            Console.Write("\nSelect Menu Option:\n[1] Delete Using Record ID\n[2] Delete Using Record Value\n[3] Exit\n");
            do
            {
                // validate the user menu choice & set the menuchoice valie for switch
                validMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out menuChoice);
                if (validMenuChoice) // If valid digit was pressed by the user
                {
                    switch (menuChoice)
                    {
                        case 1: // delete using record ID
                            do
                            {
                                Console.Write("\n\nEnter a record ID to delete:"); // get ID from user
                                validRecordID = int.TryParse(Console.ReadLine(), out recordID); // validate the ID exists
                                if (validRecordID)
                                {
                                    if (recordID > 0 && recordID <= strList.Count) // validate if ID within list index limits
                                    {
                                        strList.RemoveAt(recordID - 1); // delete record from list
                                        strList.Sort(); // sort the list after deleting the record
                                        // CreateRecord(strList, recordID, 2);
                                        if (strList.Count == 0) // in case of empty database, let the user not that nothing to be deleted
                                        {
                                            Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                            Console.ReadKey(true);
                                        }
                                        else // after deleting one record, prompt the user to delete another record or exit
                                        {   // get user choice to delete another record or exit
                                            ReadRecords(strList, 0); // visual aid the user by displaying list of the database
                                            Console.Write("\nSelect Menu Option:\n[1] Delete Another Record\n[2] Return to Main Menu\n");
                                            validSubMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out subMenuChoice);
                                            if (!validSubMenuChoice) // if the choice is not valid, print validation error
                                            {
                                                Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                                            }
                                            else // if valid choice call the delete method to delete another record
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
                                    else // in case the record ID doesn't exist pring error message
                                    {
                                        Console.WriteLine("\nValidation Error: Record doesn't exit");
                                        validRecordID = false;
                                    }
                                }
                                else // in case non numberic choice was entered print invalid character error
                                {
                                    Console.WriteLine("\nValidation Error: unexpected input, non-numerical value is used");
                                }
                            } while ((recordID > strList.Count + 1 || !validRecordID) && strList.Count > 0);
                            // loop as long there is records in the database and the user is requesting to delete another record
                            editMenuExitFlg = true;
                            break;
                        case 2: // edit using record value
                            do
                            {
                                Console.Write("\n\nEnter a record Value to delete:");
                                recordValue = Console.ReadLine().Trim(); //temp store in temp var
                                validRecordID = SearchRecord(strList, recordValue); // checking if the record exists & get record value
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
                                            ReadRecords(strList, 0); // visual aid the user by displaying list of the database
                                            Console.Write("\nSelect Menu Option:\n[1] Delete Another Record\n[2] Return to Main Menu\n");
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
        //--------------------
        //Menu Options Method
        //--------------------
        public static void MenuOptions(List<string> strList)
        {   //vairiables definition
            bool progExitFlg = false;
            int menuChoice;
            bool validMenuChoice;
            do
            {
                //Display menu options 1 to 7
                strList = strList.Distinct().ToList(); // To account for any un expected duplicates that might make it to the list (safe gaurd)
                Console.Clear();
                Console.WriteLine("\nSelect Menu Option: \n[1] Enter New Records\n[2] Display Records\n[3] Edit Records\n[4] Delete Records\n[5] Import Database\n[6] Export Database\n[7] Exit Program");
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
                            else // in case of empty database print nothing to edit
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                Console.ReadKey(true);
                            }
                            break;
                        case 4:// delete records mode
                            if (strList.Count != 0)
                            {
                                DeleteRecord(strList);
                            }
                            else // in case of empty database pring nothing to delete
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to edit\nPress any key to go back to the main menu\n");
                                Console.ReadKey(true);
                            }
                            break;
                        case 5: // import records from file
                            ImportRecords(strList);
                            break;
                        case 6: // export records to file
                            if (strList.Count != 0)
                            {
                                ExportRecords(strList);
                            }
                            else// in case of empty database pring nothing to export
                            {
                                Console.Clear();
                                Console.WriteLine("\nEmpty Database, no records to export\nPress any key to go back to the main menu\n");
                                Console.ReadKey(true);
                            }
                            break;
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
        //-------------------------------
        //Update (Edit) Database Records
        //-------------------------------
        public static void UpdateRecords(List<string> strList)
        {   //variables definition
            bool editMenuExitFlg = false;
            int menuChoice, subMenuChoice;
            bool validMenuChoice, validSubMenuChoice;
            int recordID = 0;
            bool validRecordID, exitFlag = false;
            string recordValue;
            ReadRecords(strList, 0); // visual aid the user by displaying list of the database
            Console.Write("\nSelect Menu Option\n[1] Edit Using Record ID\n[2] Edit Using Record Value\n[3] Exit\n");
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
                                Console.Write("\n\nEnter a record ID to edit:"); // get record ID
                                validRecordID = int.TryParse(Console.ReadLine(), out recordID); // validate ID
                                if (validRecordID) // if valid ID
                                {
                                    if (recordID > 0 && recordID <= strList.Count) // if ID is within list range
                                    {
                                        CreateRecord(strList, recordID, 2); // use create record to suplement the edit function (insert)
                                        //prompt the user if another record to be edited or exit
                                        ReadRecords(strList, 0); // visual aid the user by displaying list of the database
                                        Console.Write("\nSelect Menu Option:\n[1] Edit Another Record\n[2] Return to Main Menu\n");
                                        //validate menu option choice character
                                        validSubMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out subMenuChoice);
                                        if (!validSubMenuChoice) // print validation error if 1 or 2 not pressed
                                        {
                                            Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                                        }
                                        else // if another record to be edited, call the edit method again
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
                                    else // incase the record ID doesn't exit
                                    {
                                        Console.WriteLine("\nValidation Error: Record doesn't exit");
                                        validRecordID = false;
                                    }
                                }
                                else // in case of un expected character used by the user
                                {
                                    Console.WriteLine("\nValidation Error: unexpected input, non-numerical value is used");
                                }// loop as long as valid id, within range and user didn't exit
                            } while ((recordID > strList.Count + 1 || !validRecordID) && strList.Count > 0 && !exitFlag);
                            editMenuExitFlg = true;
                            break;
                        case 2: // edit using record value
                            do
                            {   // in case of using record value instead of ID
                                Console.Write("\n\nEnter a record Value to edit:"); // get record value
                                recordValue = Console.ReadLine().Trim(); //temp store in temp var
                                validRecordID = SearchRecord(strList, recordValue); // checking if the record exists & get value index (ID)
                                if (validRecordID) // if ID is valid
                                {
                                    recordID++;
                                    if (recordID > 0 && recordID <= strList.Count) // if ID is within list range
                                    {
                                        CreateRecord(strList, recordID, 2); // call create record (insert)
                                        // prompt the user to edit another record or exit
                                        ReadRecords(strList, 0); // visual aid the user by displaying list of the database
                                        Console.Write("\nSelect Menu Option:\n[1] Edit Another Record\n[2] Return to Main Menu\n");
                                        //validate menu choice pressed
                                        validSubMenuChoice = int.TryParse(Console.ReadKey(true).KeyChar.ToString(), out subMenuChoice);
                                        if (!validSubMenuChoice) // print errro if not valid
                                        {
                                            Console.WriteLine("\nValidation Error: invalid menu choice, try agin:");
                                        }
                                        else // otherwise call the update records method
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
                                    else // in case record doesn't exit
                                    {
                                        Console.WriteLine("\nValidation Error: Record doesn't exit");
                                        validRecordID = false;
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nValidation Error: Record doesn't exit");
                                }
                                //loop as long as ID vaild, within range and list has records to edit
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
            strList = strList.Distinct().ToList(); // To account for any un expected duplicates that might make it to the list (safe gaurd)
            if (strList.Count != 0) // if ther eis records to display
            {
                if (clearConsole < 3) // clear console flag to cater multiple code block calling this method. in some cases, the console clear is harmful
                {
                    Console.Clear();
                }// print to the user how many records available
                Console.WriteLine("\nThe Database has: {0}", strList.Count + " records");
                // for the size of the list , print the records on the console
                for (int i = 0; i < strList.Count; i++)
                {
                    Console.WriteLine("Record [{0}", (i + 1) + "]" + "    Name:" + strList[i]);
                }
                if (clearConsole == 1 || clearConsole == 3) // keep the console until user presses a key
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
            if (newOrEdit == 1) // 1 for new and 2 for edit since this method is used for new and edit methods
            {
                Console.Clear();
                Console.WriteLine("\nYour next record ID is: {0}", strList.Count + 1); // let the user know the ID of what is being entered
            } // let the user know how to exit the data entry mode
            Console.WriteLine("\nWhen finished, Type <Exit> to return to the main menu\n");
            // variables definition
            bool digtdetct = false, exitFlag = false, existingRecordFlag = false;
            string tempStr = "";
            // validating if user entered exit sequance

            /*-------------------------------------------------------------------------------------------*/
            //This block when the Rubric asked for 10 strict records
            if (strList.Count == 10 && newOrEdit == 1) // error Msg. in case of data entery while the list is already fully populated with allowed records
            {
                Console.WriteLine("\nRecords Maxed Out, try again later when we update the program with delete,edit option\nPress any key to continue");
                Console.ReadKey(true);
            }
            else // otherwise, there is space for new records to be added
            {
                /*-------------------------------------------------------------------------------------------*/
                do
                {
                    do
                    {
                        digtdetct = false; // bool to identify if characters other than letters exists within the entered names
                        Console.Write("Please Enter a Name:");
                        tempStr = Console.ReadLine().Trim(); //store user input in temporary string
                        tempStr = tempStr.Trim(); // clean up white spaces leading/trialing
                        existingRecordFlag = SearchRecord(strList, tempStr);
                        if (existingRecordFlag)
                        {
                            Console.WriteLine("\nValidation Error: Record already exits, try again or type <Exit> if finished\n");
                            existingRecordFlag = false;
                        }
                        else
                        {
                            for (int i = 0; i < tempStr.Length; i++) // search for non letters (or white spaces) within the name
                            {
                                if (!(Char.IsLetter(tempStr[i]) || Char.IsWhiteSpace(tempStr[i])))
                                {
                                    digtdetct = true; // if digit or special character detected, don't allow it in a name
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
                                    if (newOrEdit == 1) // add new record case
                                    {
                                        strList.Add(tempStr);
                                        strList.Sort();
                                    }
                                    else if (newOrEdit == 2 && recordID == 0)
                                    {
                                        strList.Add(tempStr); // add new record case for during edit index 0
                                        strList.Sort();
                                        exitFlag = true; // exit flag as it is edit, no loop needed
                                    }
                                    else // edit case, direct assignment
                                    {
                                        strList[recordID - 1] = tempStr;
                                        strList.Sort();
                                        exitFlag = true; // exit flag as it is edit, no loop needed
                                    }
                                }
                                else
                                {   //Validate if the record exits, don't allow it
                                    Console.WriteLine("\nValidation Error: Record already exits, try again or type <Exit> if finished\n");
                                    existingRecordFlag = false;
                                }
                            }
                            else // in case exit was entered, stop the data entry mode
                            {
                                exitFlag = true; // exit flag as exit was entered by user
                            }
                        }
                    } while (digtdetct);
                    // The commented code is part of the change in Rubric from 10 records to dynamically resizable
                }while (!exitFlag && strList.Count < 10) ; // list count validation less than allowed records  
            } 
            return exitFlag;
        }
    }
}
