﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;


namespace testJson
{
    class TestJSON
    {
        static void Main(string[] args)
        {
            string json = @"
                            [{'airport':
                                {'code':'ATL','name':'Atlanta, GA: Hartsfield-Jackson Atlanta International'},
                                'statistics':{
                                    'flights':{
                                        'cancelled':5,'on time':561,'total':752,'delayed':186,'diverted':0},
                                        '# of delays':{'late aircraft':18,'weather':28,'security':2,'national aviation system':105,'carrier':34},
                                        'minutes delayed':{'late aircraft':1269,'weather':1722,'carrier':1367,'security':139,'total':8314,'national aviation system':3817}},
                                'time':{'label':'2003/6','year':2003,'month':6},
                                'carrier':{'code':'AA','name':'American Airlines Inc.'},
                              'EventProcessedUtcTime':'2017-05-04T15:08:34.1722594Z','PartitionId':0,'EventEnqueuedUtcTime':'2017-05-04T15:08:32.7370000Z'}
                            ]";

            IList<JObject> jsonObjectsList = new List<JObject>();
            //string jsonPath = null;


            using (JsonTextReader reader = new JsonTextReader(new StringReader(json)))
            {
                while (reader.Read())
                {
                    //Creating a Json Token. (The Load method will achieve this for us)
                    var jsonToken = JToken.Load(reader);

                    //Need to check to see if the user has specified a Path or not
                    if (!string.IsNullOrEmpty(json))
                    {
                        //Using the 'as' keyword to do a conversion from JToken to JObject
                        var j = jsonToken as JObject;

                        //SelectTokens allows the user to query JSON using a single string path to a desired JToken. This makes dynamic queries really easy.
                        //Using path query to allow us to extract child tokens in nested JSON Structures
                        j.SelectTokens(json).OfType<JObject>();

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

                                Console.WriteLine("Name: {0}, Value: {1}", name, value);
                                Console.ReadLine();
                            }
                        }
                    }
                }
            }
        }
    }
}
