
using System;
using System.IO;

namespace fortuneMaker
{
    class Program
    {
        static void Main(string[] args)
        {
            // Simple script to read the file of fortunes (https://github.com/ianli/fortune-cookies-galore/blob/master/fortunes.txt) and
            // rewrite them into 10 separeate python files each containing 150 fortunes, while I am att it I will check the length of each fortune
            // and write them as a python dict item that contains 5 lines of data preformated for a ssd1306 OLED display. The
            // dictionary items will be names F<NNN> i.e. F023 = { "l1" : "Everybody is", "l2" : "ignorant..." , "l3" : "only on different" , "l4":"subjects", "l5":""} 
            // If the fortune can not be formated to fit (5 lines of 16 characters = 80 characters max) then it is skipped
            // Some longer fortunes may still not fit depending on word breaks and if the words can fit in the 16 character line.
            String line;
            int line80Counter = 0;
            int fileNumber = 1;
            int fileDictCounter = 1;
            try
            {
                // 
                StreamReader sr = new StreamReader("fortune.txt");
                StreamWriter file = new StreamWriter("fortune" + fileNumber.ToString() + ".py");
                //Read the first line of text
                line = sr.ReadLine();
                //Continue to read until you reach end of file
                while (line != null)
                {
                    //write the line to console window
                    if (line.Length <=80)
                    {
                        if (lineToDict(line) != "")
                        {
                            Console.WriteLine(line); 
                            Console.WriteLine(lineToDict(line));

                            if (fileDictCounter % 151 == 0) { 
                                file.Close();
                                fileNumber++;
                                fileDictCounter = 1;
                                file = new StreamWriter("fortune" + fileNumber.ToString() + ".py");
                            }
                            file.WriteLine("F" + fileDictCounter.ToString() + "={" + lineToDict(line) + "}");
                            fileDictCounter++;
                            line80Counter++;
                        }

                    }
                    //Read the next line
                    line = sr.ReadLine();
                }
                //close the file
                sr.Close();
                file.Close();

            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
            }
            finally
            {
                Console.WriteLine("Lines less than 80 characters: " + line80Counter.ToString());
                Console.ReadLine();
            }
        }

        static string lineToDict(string fortune) {

            string[] fortuneWords;
            fortuneWords = fortune.Split(" ");
            string dict = "";
            string dictLine = "";
            int lineNumber = 1;
            foreach(string word in fortuneWords)
            {
                string cleanword = word.Replace("'", "").Replace("\"", "");
                
                if (cleanword.Length > 16)
                {
                    return "";
                }
                if (dictLine.Length + 1 + cleanword.Length <= 16) {
                    dictLine += " " + cleanword;
                }
                else
                {
                    if (lineNumber>1)
                    {
                        dict += ",";
                    }
                    dict += "'L" + lineNumber.ToString() + "' : '" + dictLine + "'";
                    dictLine = cleanword;
                    lineNumber++;
                    if (lineNumber>5)
                    {
                        return "";
                    }
                }

            }
            // Add dictionary line if it contains content
            if (dictLine.Length > 0)
            {
                if (lineNumber > 1)
                {
                    dict += ",";
                }
                dict += "'L" + lineNumber.ToString() + "' : '" + dictLine + "'";
            }
            // Finally add missing lines to get to 5 line in the dictionary item (easy logic to display result on OLED)
            for(int i = lineNumber; i < 5; i++) {
                dict += ",'L" + (i+1).ToString() + "' : ''";
            }

            return dict;

        }




    }
}
