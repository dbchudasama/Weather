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
    //Implementing Interface IExtractor
	public class CustomCSVExtractor : IExtractor
    {
        //Overriding interface method 'Extract'
        public override IEnumerable<IRow> Extract(IUnstructuredReader input, IUpdatableRow output)
        {
            IList<String> jsonObjectsAsString = new List<String>();
            string jsonPath;
            
            //Read in the JSON File using the JSONTextReader as provided by Newtonsoft.json Library
            using (JsonTextReader jtr = new JsonTextReader(new StreamReader(input.BaseStream)))
            {
                //While the json reader reads to the end of the json file
                while (jtr.Read())
                {
                        var jsonValue = jtr.Value;
                        //Error Handling. Value pulls back the text value of the JToken in subject
                        if (jsonValue == null)
                        {
                            //Missing Value
                            Console.WriteLine("Unable to return the text value for:" + jsonValue + ". Are you missing a Json Value?");
                        }
                        else
                        {

                            //Creating a Json Token. (The Load method will achieve this for us)
                            var jsonToken = JToken.Load(jtr);

                            //SelectTokens allow s the user to query JSON using a single string path to a desired JToken. This makes dynamic queries really easy
                            //jsonToken.SelectTokens(jsonPath).OfType<JObject>();

                            //Creating a new instance of JsonSerializer
                            JsonSerializer js = new JsonSerializer();

                            //Deserialising json structure 
                            string json = js.Deserialize<String>(jtr);

                            //Now adding the deserialised json to a List
                            jsonObjectsAsString.Add(json);

                            //Iterate over each JSON Object
                            foreach (String jsonStringObject in jsonObjectsAsString)
                            {
                                //Set this as the output as a Key, Value pair
                                output.Set<string>(jsonObjectsAsString.Count, jsonStringObject);
                            }
                        }
                }
            }

            //Output as ReadOnly
            yield return output.AsReadOnly();
        }
    }

}