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

            string path = @"C:\Users\DivyeshB\Documents\Visual Studio 2017\Projects\csvReader\Grid Disconnection Data for Hackfest.csv";

            string newPath = @"C:\Users\DivyeshB\Documents\Visual Studio 2017\Projects\csvReader\Grid Disconnection Data for Hackfest_Reformatted.csv";

            //Reading in csv file using StreamReader
            using (StreamReader SR = new StreamReader(File.OpenRead(path)))
            {

                //Iterating each row
                while (!SR.EndOfStream)
                {
                    //Reading ech line 1 by 1.Storing each line in the rows array
                    var line = SR.ReadLine();

                    //Splitting lines based on delimiter
                    var split = line.Split('{');

                    if (split.Length == numberOfColumns)
                    {
                        using (StreamWriter SW = File.AppendText(newPath))
                        {
                            SW.WriteLine(String.Join("{", split));

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
                                SW.WriteLine(String.Join("{", temp));
                            }

                            temp = null;
                        }

                    }
                }
            }
        }
    }
}


/*
 * 
 * Another Approach which I tried - Partially complete nut it is OVERKILL.
 * Had to save the Original Excel file AS CSV UTF-8 file in order for the code to work. (Did this on my home machine).
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
*/
