using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace csvReader
{
	class Program
	{
		static void Main(string[] args)
		{
			//Read in CSV
			using (var reader = new StreamReader(File.OpenRead(@"C:\Users\gujra\OneDrive\Documents\Weather_Richard\Grid Disconnection Data for Hackfest2-3.csv")))

			{
				string[] lines;

				//While loop until reading till the end of the csv file
				while (!reader.EndOfStream)
				{
					//Read line by line
					var line = reader.ReadLine();

					//Here setting the line to split at a character, start a new line and remove any empty entries
					lines = line.Split(new char[] { '\r' }, StringSplitOptions.RemoveEmptyEntries);

					//Checking the number of rows
					int rows = lines.Length;
					//Checking the number of columns (First Row as this is the header file)
					int col = lines[0].Split(',').Length;

					//Data Array
					string[] csvData = string[rows, col];

					// Load the array.
					for (int r = 0; r < rows; r++)
					{
						string[] line_r = lines[r].Split(',');
						for (int c = 0; c < col; c++)
						{
							csvData[r, c] = line_r[c];
						}
					}

					var sw = new StreamWriter();
				}
			}

		}
	}
}