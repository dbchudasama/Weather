using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace csvReader
{
	class Program
	{
		static void Main(string[] args)
		{
            //Initialising empty array
            string[] temp = null;

            List<string> temp_row = new List<string>();

            //Columns
            int numColumns = 24;

            //Path for input file
            String path = @"C:\Users\gujra\OneDrive\Documents\Weather_Richard\Grid Disconnection Data for Hackfest_ACTIVE2.csv";

            //Path for output file
            String newPath = @"C:\Users\gujra\OneDrive\Documents\Weather_Richard\Grid Disconnection Data for Hackfest_Reformatted3.tsv";

            //Reading in csv file using StreamReader
            using (StreamReader SR = new StreamReader(File.OpenRead(path)))
            {
                
                //While the Stream Reader reads to the end of the file
                while (!SR.EndOfStream)
                {
                    //Reading each line one by one
                    //var line = SR.ReadLine();
                    var text = SR.ReadToEnd();

                    //line = line.Replace("┬á", "");
                    //Now splitting the line based on the delimiter
                    var haveSplit = text.Split('\t');

                    //Splitting the haveSplite on the word 'IN'
                    var headerLine = haveSplit.Where(c => c.Contains("IN"));
                    //Selecting Index value of the headerLine
                    var indexHeader = headerLine.Select(e => Array.IndexOf(haveSplit,e));
                    //Mapping to a tuple in a key value pair
                    var headerPair = headerLine.Zip(indexHeader, (val, idx) => new Tuple<string, int>(val, idx));

                    //Looping over an array of tuples
                    foreach (Tuple<string, int> item in headerPair.Take(2))
                    {
                        //If not a multiple of 22
                        if (item.Item2 % 22 == 0)
                            Console.WriteLine(item.Item1);
                    
                        var loop = haveSplit.Where(c =>
                        {
                            //Taking the array of the haveSplit
                            var idx = Array.IndexOf(haveSplit, c);
                            //Making sure it is not greater than 22 as this is the maximum elements in a row
                            return (idx > item.Item2 && idx < item.Item2 + 22);
                        });
                        foreach (String e in loop.Take(22))
                        {
                            //temp = temp.Concat(e).ToArray<String>();
                            Console.WriteLine(e);
                        }
                    }

                    //Comparison to check the number of split elements match the number of columns
                    if (haveSplit.Length == numColumns)
                    {
                        //Using StreamWriter to append text to file
                        using (StreamWriter SW = File.AppendText(newPath))
                        {
                            //If the number of elements in the string match the number of columns
                            SW.WriteLine(String.Join("	", haveSplit));
                            //Console.WriteLine();
                        }
                    }
                    //However, if we don't get the number of string elements to match the number of columns then...
                    else
                    {
                        //Firstly check if the temp array is empty
                        if(temp == null)
                        {
                            //If it is then initialise it to the value of the split string
                            temp = haveSplit;
                        }
                        else
                        {
                            //Else concatinate the split string to the temp array and overwrite
                            temp = temp.Concat(haveSplit).ToArray<String>();
                           
                        }
                        
                        //Now checking to see if the temp array length matches the number of columns
                        if(temp.Length == numColumns)
                        {
                            //Once again using the Stream Writer to write the new concatenated string line of the correct size to file
                            using (StreamWriter SW = File.AppendText(newPath))
                            {
                                //Write to file
                                SW.WriteLine(String.Join("	", temp));
                            }

                            //Setting temp = null as initially done. This will mean that it will keep looping in this 'else' statement until string size matches the number of columns.
                            temp = null;
                        }
                        //ERROR HANDLING - This should never happen!
                        else if (temp.Length > numColumns)
                        {
                            ////Using StreamWriter to append text to file
                            //using (StreamWriter SW = File.AppendText(newPath))
                            //{
                            //    //If the number of elements in the string match the number of columns
                            //    SW.WriteLine(String.Join("	", haveSplit));
                            //    //Console.WriteLine();
                            //}
                            //Console.WriteLine(String.Join("	"
                            //    , temp));
                            //Console.WriteLine(haveSplit.Length);
                            //Console.ReadLine();
                            //throw new Exception("Length too big!");

                            //Console.WriteLine(x);
                            Console.ReadLine();

                        }
                    }
                }

            }

            
            //Close the StreamReader
            //SR.Close();


            /*
            for(int i = 0; i < 1822; i++)
            {
                Console.WriteLine(delim);
            }

            Console.ReadLine();
            */

        }
    }
}