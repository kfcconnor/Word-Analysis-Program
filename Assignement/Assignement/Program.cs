using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Assignement
{
    class Program
    {
        static void Main(string[] args)
        {
            bool choiceValid = false;
            int choice = 0;
            string fileName = " ";

            while (choiceValid == false) // While statement to ensure that program does not procede while there is a error in menu choice. Also used to allow analysis of multiple peices of text.
            {
                Console.Clear();
                Console.WriteLine("1.Do you want to enter the text via the keyboard?");
                Console.WriteLine("2.Do you want to read in the the text from a file?");
                Console.WriteLine("Please select which type to text you wish to analyise:");
                try
                {
                    choice = Convert.ToInt32(Console.ReadLine()); //try catch statement to catch any invalid entries and display an error message for the user.
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Invalid Choice, please enter either 1 or 2 as a choice.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    continue;
                }
                if (choice == 1) // User enters data with keyboard
                {
                    Console.Clear();
                    Console.WriteLine("Please enter the text that you wish to analyise:");
                    string typedText = Console.ReadLine(); // The text the user enters here is then used for the analysis.
                    Console.Clear();
                    int letters = noOfLetters(typedText); // This calls the method that gets the number of letters used within the program.
                    Console.WriteLine("Number of Letters: {0}", letters); // Which is then displayed for the user.
                    int Sentances = noOfSentances(typedText); // This calls the method that gets the number the sentences used by the text entered for the user.
                    Console.WriteLine("Number of Sentences: {0}", Sentances);
                    vowelsConsonants(typedText); // The following for methods are called and carry out both the calculating and displaying of the results inside their individual methods
                    upperLowercase(typedText);
                    freqOfLetters(typedText);
                    sentimentAnalysis(typedText);

                }
                else if (choice == 2) // User enters data from an external text file
                {
                    bool valid = false;
                    int loadChoice = 0;
                    string fileLocation;
                    string loadText = "";
                    while (valid == false) // ensures invalid data can't get into other parts of the data and crash the program.
                    {
                        Console.Clear();
                        Console.WriteLine("Where do you wish to open the file from?");
                        Console.WriteLine("1. From the text file folder of the installation.");
                        Console.WriteLine("2. From another folder within the computer"); // User is asked where they wish to load the data from
                        try
                        {
                            loadChoice = Convert.ToInt32(Console.ReadLine()); // This tests the choice for exceptions and returns an error if the user tries to enter the wrong choice
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show(e.Message, "Invalid Choice.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            continue; // Causes loop if invalid data is entered without the valid flag being set to true and ending the loop.
                        }
                        valid = true;
                    }
                    if (loadChoice == 1) // Loads data from the text files folder of installation.
                    {
                        valid = false;
                        while (valid == false) // loop statement to stop invalid text files being loaded.
                        {
                            fileLocation = Directory.GetCurrentDirectory(); // This gets the location of the install folder to ensure that the correct folder to read from is found.
                            Console.Clear();
                            Console.WriteLine("Please enter the name of the file you wish to analyse from the text files folder");
                            Console.WriteLine("Please note that you do not need to put a file type at the end.");
                            fileName = Console.ReadLine();
                            fileLocation += (@"\Text Files\" + fileName + ".txt"); // The string here adds the file name that the user wants to load to the file location of the install so that the folder can look for the correct file.
                            try
                            {
                                loadText = File.ReadAllText(fileLocation); // This attempts to read the file, if any exceptions are found the program will return an error saying that the file is not found
                            }
                            catch (FileNotFoundException e)
                            {
                                MessageBox.Show(e.Message, "File: " + fileName + " not found.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message, "Invalid Entry.", MessageBoxButtons.OK, MessageBoxIcon.Error); // Incase an invalid character or another exception that isn't a file not found exception is thrown this message will display and loop the program for the user.
                                continue;
                            }
                            valid = true;
                        }
                    }
                    else if (loadChoice == 2) // Load file from another program within the computer
                    {
                        valid = false;
                        while (valid == false)
                        {
                            Console.Clear(); ;
                            Console.WriteLine("Please enter the full file path of the document you wish to scan");
                            Console.WriteLine(@"It must be in the format C:\folders\file.txt with as many folders as needed");
                            fileLocation = Console.ReadLine(); // This time the full location has to be added for the user.
                            try
                            {
                                loadText = File.ReadAllText(fileLocation); // See if choice 1 for notes on the try catch statement used here.
                            }
                            catch (FileNotFoundException e)
                            {
                                MessageBox.Show(e.Message, "File: " + fileLocation + " not found.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message, "Invalid Entry.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                continue;
                            }
                            valid = true;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("Please enter the name of the word list file.");
                    Console.WriteLine("These can be found in the word list folder of your installation."); // Here is where the user inputs the name they want for the list of long words gather when analysing loaded in text files.
                    string listFileName = Console.ReadLine();
                    Console.Clear();
                    int letters = noOfLetters(loadText); // It then users the loaded in text to carry out mostly the same methods as before.
                    wordList(loadText, listFileName); // Accept for this one which is used for the creation of the long word list for the text. Saved to the user defined location.
                    Console.WriteLine("Number of Letters: {0}", letters);
                    int Sentances = noOfSentances(loadText);
                    Console.WriteLine("Number of Sentences: {0}", Sentances);
                    vowelsConsonants(loadText);
                    upperLowercase(loadText);
                    freqOfLetters(loadText);
                    sentimentAnalysis(loadText);

                }
                Console.WriteLine("Do you wish to carry out analysis on another piece of text");
                Console.WriteLine("Type q to quit or type y to continue");
                string quitChoice = " ";
                while (quitChoice != "q" && quitChoice != "y")
                {
                    quitChoice = Console.ReadLine(); //User then provides choice if they want to anyalsis another peice of text or quit the program.
                    if (quitChoice == "q")
                        choiceValid = true;
                    else if (quitChoice == "y")
                        choiceValid = false;
                }
            }
        }

        static int noOfLetters(string textAnaly)
        // This method takes the text to be analysed and then returns the number of letters within the text
        {
            int noOfLetters = 0;
            foreach (char c in textAnaly)
            {
                if (char.IsLetter(c))
                    noOfLetters++;
            }
            return noOfLetters++;
        }

        static void wordList(string textAnaly, string wordListFile)
        // This method looks through text to be analysed and creates a list of words above 7 letters in length and saves them to a user named file.
        {
            string longWords = "";
            int currentWordLen = 0;
            string currentWord = "";
            string wordListLocation = Directory.GetCurrentDirectory() + @"\Word Lists\" + wordListFile + ".txt"; // Creates the file location that will be used to store the word list.
            foreach (char c in textAnaly) // goes through the words counting the length of them and adding any with a length of 7 and above onto the long word list.
            {
                if (char.IsLetter(c))
                {
                    currentWordLen++;
                    currentWord += c;
                }
                else
                {
                    if (currentWordLen >= 7)
                    {
                        longWords += currentWord + ",";
                        currentWordLen = 0;
                        currentWord = "";
                    }
                    else
                    {
                        currentWordLen = 0;
                        currentWord = "";
                    }
                }
            }
            File.WriteAllText(wordListLocation, longWords); // Saves the word list into the file defined for the long word list.
        }

        static int noOfSentances(string textAnaly)
        // This method goes through the program counting the number of sentences within it.
        {
            int noOfSentances = 0;
            char lastChar = ' ';
            foreach (char c in textAnaly)
            {
                if (c == '.')
                    noOfSentances++;
                lastChar = c;
            }
            if (lastChar != '.')
                noOfSentances++;

            return noOfSentances;
        }

        static void vowelsConsonants(string textAnaly)
        // This method goes through the program counting the vowls and consonants within it, displaying it on the console for the users
        {
            int noOfVowels = 0;
            int noOfConsonants = 0;

            foreach (char c in textAnaly)
            {
                if (char.IsLetter(c))
                {
                    if (c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u')
                        noOfVowels++;
                    else
                        noOfConsonants++;
                }
            }
            Console.WriteLine("Number of Vowels: {0}", noOfVowels);
            Console.WriteLine("Number of Consonants: {0}", noOfConsonants);
        }

        static void upperLowercase(string textAnaly)
        // This method goes through the program counting the upper and lower case letters within it and displaying it for the user on the console.
        {
            int noOfUpper = 0,
                noOfLower = 0;

            foreach (char c in textAnaly)
            {
                if (Char.IsLetter(c))
                {
                    if (Char.IsUpper(c))
                        noOfUpper++;
                    else
                        noOfLower++;
                }
            }
            Console.WriteLine("Number of Uppercase Letters: {0}", noOfUpper);
            Console.WriteLine("Number of Lowercase Letters: {0}", noOfLower);
        }

        static void freqOfLetters(string textAnaly)
        // This method goes through the program count the number of times that each letter appears in the program, at the end displaying this as well as the percentage of the text that is made up by this letter.
        {
            string lowerTextAnaly;
            char[] letterUsed = new char[26];
            int[] freqOfLetter = new int[26];
            double[] letterPercent = new double[26]; // Arrays are used here to store the letters and number of the times they are used to reduce the number of variables needed to store them.
            int uniqueLetters = 0;
            int noOLetters = noOfLetters(textAnaly);
            bool letterFound = false;

            lowerTextAnaly = textAnaly.ToLower(); // This converts the text to lower case letters to ensure that it does not count upper and lower cases of a letter to be different letters.

            foreach (char c in lowerTextAnaly)
            {
                if (char.IsLetter(c)) // This checks if the character is a letter. If it is not it will go to the next char and carry out the same check
                {
                    for (int pos = 0; pos < uniqueLetters; pos++) // This for statement goes through the array and checks if the letter has already been added if so adding another 1 to the frequency of the letters
                    {
                        if (letterUsed[pos] == c)
                        {
                            freqOfLetter[pos]++;
                            letterFound = true;
                        }
                    }
                    if (letterFound == false) // If the letter is not already in the array it is added and the frequency is set to 1 for future letters to be added.
                    {
                        letterUsed[uniqueLetters] = c;
                        freqOfLetter[uniqueLetters] = 1;
                        uniqueLetters++;
                    }
                    letterFound = false;

                }
            }

            for (int count = 0; count < uniqueLetters; count++)
            {
                letterPercent[count] = (freqOfLetter[count] / Convert.ToDouble(noOLetters)) * 100f; // This goes through the frequencies converting them to percentages and storing them in the percentage array

            }

            Console.WriteLine("Letter       Freq of letter      Percentage");
            for (int count = 0; count < uniqueLetters; count++) // This then goes though the data displaying it onto the console for the user.
            {
                Console.WriteLine("{0}              {1}                 {2}%", letterUsed[count], freqOfLetter[count], Math.Round(letterPercent[count], 2, MidpointRounding.AwayFromZero));
            }
        }

        public static void sentimentAnalysis(string textAnaly)
        // This method goes through the file checking all the words against a word list and assigning a score to determine if the word is negative or positive. Using the average score percentage to show overall sentiment.
        {
            string filePath = Directory.GetCurrentDirectory() + @"\SentimentAnalysis\WordList.txt";
            string wordList = File.ReadAllText(filePath); ; // This loads the word list so that the method can call search the text for the specific words
            string currentWord = "";
            List<string> words = new List<string>();
            List<int> scores = new List<int>();
            bool negative = false; // This determines if a score is negative allowing for negative seniment to be possible.
            int totalScore = 0;
            string currentNumber = "";
            double scorePerSentance = 0;
            textAnaly = textAnaly.ToLower(); // Text converted to lowercase so that the list does not mistake the words to be different because there is a capital letter.

            foreach (char c in wordList) // This for each statement goes through the word list separating the words and scores and putting them into two separate lists
            {
                if (char.IsLetter(c) == true)
                {
                    currentWord = currentWord + c;
                }
                else if (c == '-')
                {
                    negative = true;
                }
                else if (char.IsNumber(c) == true)
                {
                    words.Add(currentWord);
                    currentWord = "";
                    currentNumber = Convert.ToString(c);
                    if (negative == true)
                    {
                        scores.Add(-(Convert.ToInt32(currentNumber)));
                    }
                    else
                    {
                        scores.Add((Convert.ToInt32(currentNumber)));
                    }
                    negative = false;
                }
            }

            foreach (char c in textAnaly) // This then goes through the text being analysed and carries out a binary search to find the word on the list, if it is found then it's score is added onto the total score.
            {
                if (char.IsLetter(c) == true)
                {

                    currentWord = currentWord + c;
                }
                else
                    if (c == ' ')
                    {
                        try
                        {
                            int index = words.BinarySearch(currentWord);
                            totalScore = totalScore + scores[index];
                        }
                        catch (Exception)
                        {
                        }
                        currentWord = "";
                        scorePerSentance = totalScore / Convert.ToDouble(noOfSentances(textAnaly)); // Here the total score is divided with the  number of sentences which allows a better grasp of the sentiment in the text to be gathered.
                        Console.WriteLine("Total sentiment score: {0}", totalScore);
                        Console.WriteLine("Score per sentence: {0}", scorePerSentance);

                        if (scorePerSentance <= -0.2 || scorePerSentance > -1) // this if statement assigns the sentiment to the text dependent on the score per sentance.
                            Console.WriteLine("The text is slightly negative.");
                        else if (scorePerSentance <= -1 || scorePerSentance > -2)
                            Console.WriteLine("The text is negative.");
                        else if (scorePerSentance <= -2)
                            Console.WriteLine("The text is very negative.");
                        else if (scorePerSentance > -0.2 || scorePerSentance < 0.2)
                            Console.WriteLine("The text is neutral.");
                        else if (scorePerSentance >= 0.2 || scorePerSentance < 1)
                            Console.WriteLine("The text is slightly positive.");
                        else if (scorePerSentance >= 1 || scorePerSentance < 2)
                            Console.WriteLine("The text is positive.");
                        else if (scorePerSentance >= 2)
                            Console.WriteLine("The text is very positive.");
                    }
            }
        }
    }
}
