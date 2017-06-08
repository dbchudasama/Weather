using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csvReader
{
    class Program
    {
        static void Main(string[] args)
        {
            //Array size is equal to the toal rows in csv file
            string[] temp = null;

            List<string> temp_row = new List<string>();

            //Columns
            int numberOfColumns = 23;

            string path = @"C:\Users\gujra\OneDrive\Documents\Weather_Richard\Grid Disconnection Data for Hackfest_CURRENT_USE.tsv";

            string newPath = @"C:\Users\gujra\OneDrive\Documents\Weather_Richard\Grid Disconnection Data for Hackfest_Reformatted2.csv";

            //Reading in csv file using StreamReader
            using (StreamReader SR = new StreamReader(File.OpenRead(path)))
            {

                //Iterating each row
                while (!SR.EndOfStream)
                {
                    //Reading ech line 1 by 1.Storing each line in the rows array
                    var line = SR.ReadLine();

                    //Splitting lines based on delimiter
                    var split = line.Split('	');

                    //Comparing
                    if (split.Length == numberOfColumns)
                    {
                        //Testing Purposes
                        //Console.WriteLine(line);
                        //Console.ReadLine();
                        //Writing to File
                        using (StreamWriter SW = File.AppendText(newPath))
                        {
                            //Write line
                            SW.WriteLine(line);
                        
                        }
                    }
                    else
                    {
                        if (temp == null)
                        {
                            temp = split;
                        }
                        else
                        {

                            temp = temp.Concat(split).ToArray<String>();
                        }
                        if (temp.Length == numberOfColumns)
                        {
                            using (StreamWriter SW = File.AppendText(newPath))
                            {
                                SW.WriteLine(String.Join("	", temp));
                            }

                            temp = null;
                        }

                    }
                }
            }
        }
    }
}
