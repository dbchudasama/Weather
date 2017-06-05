using Microsoft.Analytics.Interfaces;
using Microsoft.Analytics.Types.Sql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace MyJsonExtractor
{
	[SqlUserDefinedExtractor]

	//Implementing interface IExtractor
	public class MyJsonExtractor : IExtractor

	{
		private string pathOfRow;

		//Constructor for MyJsonExtractor
		public MyJsonExtractor(string pathOfRow = null)
		{
			this.pathOfRow = pathOfRow;
			
		}
		
		//Here overriding IEnumerable as we wish to use collections
		public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
		{

			//The column deliminator
			char col_delim = ',';
			String line;

			var reader = new StreamReader(input.BaseStream);
			
			while ((line = reader.ReadLine()) != null)
			{
				//Parse json a single token at a time
				var token = line.Split(col_delim);
				
				//Iterating over Objects of type JObject. Objects are here represented as rows.
				//foreach (JObject j in ChildObject(token, this.pathOfRow) )
				{
					// All fields are represented as columns
					//this.RowConversion(o, output);
				}
				

				yield return output.AsReadOnly();
			}
		}

		private static IEnumerable<JObject> ChildObject(JToken root, string path)
		{
			// Path specified
			if (!string.IsNullOrEmpty(path))
			{
				return root.SelectTokens(path).OfType<JObject>();
			}

			// Single JObject
			var r = root as JObject;
			if (r != null)
			{
				return new[] { r };
			}

			// Multiple JObjects
			return root.Children().OfType<JObject>();
		}


	}

}