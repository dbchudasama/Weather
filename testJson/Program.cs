using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

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

            JsonTextReader reader = new JsonTextReader(new StringReader(json));

            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Token: {0}", reader.TokenType);
                    Console.ReadLine();
                }
            }
        }
    }
}
