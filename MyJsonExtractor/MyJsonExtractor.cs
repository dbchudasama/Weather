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
            IList<JObject> jsonObjectsList = new List<JObject>();
            string jsonPath = null;
     

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

                        //Need to check to see if the user has specified a Path or not
                        if (!string.IsNullOrEmpty(jsonPath))
                        {
                            //Using the 'as' keyword to do a conversion from JToken to JObject
                            var j = jsonToken as JObject;

                            //SelectTokens allows the user to query JSON using a single string path to a desired JToken. This makes dynamic queries really easy.
                            //Using path query to allow us to extract child tokens in nested JSON Structures
                            j.SelectTokens(jsonPath).OfType<JObject>();

                            //Children() will return a collection of child tokens of the specific token. Multiple Nested JObject Values 
                            j.Children().OfType<JObject>();

                            //Now adding the jsonObject to the JObject List
                            jsonObjectsList.Add(j);

                            //Iterate over each JSON Object in the list
                            foreach (JObject p in jsonObjectsList)
                            {
                                //Fetching the property 'name' of the JObject. This is the key. JProperty models a name-value pair contained within a JObject. JObjext is basically a JSON Object.
                                //Each JObject has a collection of JProperties which are it's children, while each JProperty has a Name and a single child, it's value.
                                foreach (JProperty jp in p.Properties())
                                {
                                    //Fetching the name property of the JSON Object
                                    string name = jp.Name;
                                    //Fetching the value of the JObject
                                    JToken value = jp.Value;

                                    //Set this as the output as a Key, Value pair
                                    output.Set<object>(name,value);
                                } 
                            }
                        }
                    }
                }

                //Output as ReadOnly
                yield return output.AsReadOnly();
            }
        }

    }
}