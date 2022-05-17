using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace jarin_whatsapp_csharp
{
    


    internal class Program
    {

        private static string DestinationNumber="09122222222"; // Destination Number you want to send to!
        private static string Message = "Hello World!"; //Message you want to send


        private static readonly HttpClient client = new HttpClient();
        static async Task Main(string[] args)
        {
            ResponseMsgSendJson jsonDes = await SendMsg(); // send message request
            Console.WriteLine(string.Format("Status:{0}", jsonDes.status)); // write status to console

            if (jsonDes.status == "100") // If the status is 100 it means every thing is ok!
            {
                int timeout = 120; // About 120 seconds wait for sending message
                while (true)
                {
                    ResponseMsgStatusJson jsonDes2 = await GetMsgStatus(jsonDes.code);// Get status of message request
                    Console.WriteLine(jsonDes2.ToString()); // Write request response to console
                    if (jsonDes2.status=="100" && jsonDes2.messageReport != null && jsonDes2.messageReport.Count > 0) // If any report is present
                    {
                        //If our destination phone number reached!
                        if (jsonDes2.messageReport.Find(x => x.DestPhoneNumber == DestinationNumber && x.Status == "sent") != null)
                        {
                            Console.WriteLine("ALL OK!!!!");
                            break;
                        }
                    }
                    await Task.Delay(1000);// 1 s delay for next request
                    timeout--;
                    if(timeout==0)//Check for timeout
                    {
                        Console.WriteLine("Timeout!");
                        break;
                    }
                }

            }

            Console.ReadLine();
        }


        class ResponseMsgSendJson
        {
            public string code;
            public string status;
        }

        static async Task<ResponseMsgSendJson> SendMsg()
        {
            //Fill body of post request
            var values = new Dictionary<string, string>
              {
                  { "phoneNumber", "09121111111" },   //your user name
                  { "passWord", "12345678" }, //your password
                { "destPhoneNumbers", DestinationNumber }, // destination number(s)
                { "text", Message },    //Massege
                { "lines", "09133333333" } // Your registered Whats-App line number(s)
              };

            var content = new FormUrlEncodedContent(values);

            //url for send message
            var response = await client.PostAsync("http://wp.jarin.ir/Api/SendMessage.php", content);// wait for send request

            var responseString = await response.Content.ReadAsStringAsync();//wait for get response

            //deserialize json response
            ResponseMsgSendJson jsonDes = JsonConvert.DeserializeObject<ResponseMsgSendJson>(responseString);

            return jsonDes;
        }


        class messageReport
        {
            public string DestPhoneNumber;
            public string Status;
        }


        class ResponseMsgStatusJson
        {
            public string messageStatus;
            public List<messageReport> messageReport;
            public string status;

            public override string ToString()
            {
                string reports = "";
                if(messageReport != null && messageReport.Count>0)
                {
                    foreach(var report in messageReport)
                    {
                        reports+=string.Format("{0}:{1} | ",report.DestPhoneNumber,report.Status);
                    }
                }
                else
                {
                    reports = "No Reports";
                }

                string str = string.Format("Status:{0} , MsgStatus:{1} , reports:{{{2}}}",status ,messageStatus,reports);
                
                return str;
            }
        }


        static async Task<ResponseMsgStatusJson> GetMsgStatus(string msgId)
        {
            //fill post request body
            var values = new Dictionary<string, string>
              {
                  { "phoneNumber", "09121111111" }, //your user name
                  { "passWord", "12345678" }, // your password
                { "messageId", msgId }, // the message id given by send massage request
              };

            var content = new FormUrlEncodedContent(values);

            //wait for send request
            var response = await client.PostAsync("http://wp.jarin.ir/Api/MessageStatus.php", content);

            //wait for response
            var responseString = await response.Content.ReadAsStringAsync();
            //some possible resonses
            //{"messageStatus":"approve","messageReport":[],"status":100}
            //{"messageStatus":"approve","messageReport":[{"DestPhoneNumber":"0913xxxxxxx","Status":"sending"}],"status":100}
            try
            {
                ResponseMsgStatusJson jsonDes = JsonConvert.DeserializeObject<ResponseMsgStatusJson>(responseString);
                return jsonDes;
            }
            catch
            {
                return null;
            }
        }

    }
}
