using Microsoft.OpenApi.Models;
using RestSharp;
using System.Text.Json;
namespace WhatsWebHook.Functions
{
    public static class MessageProcess
    {
        public static string  replyMessage(string from_no, string msg_type,string msg_body, string msg_id, string contact_name)
        {

            // use dependency injection and make the dependancy singleton

            var msg_url = "https://graph.facebook.com/v13.0/100704419435623/messages";

        /*    var client = new RestClient(msg_url);
            client.Timeout = -1; */
            
            var options = new RestClientOptions(msg_url)
            {
                ThrowOnAnyError = true,
                MaxTimeout = -1                
            };
            var client = new RestClient(options);

            var request = new RestRequest();
            request.Method = Method.Post;


            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer EAALDinrOTh4BAGS3GZAeAjagdrUFzFyE0uXqtZAHKEUUYyK9p9P9VtpsMx3ZApdDWuVLbT0cFkWZCkCWhd9IYv12vkJWFYmZArGxLqE6DQCKEWMYvwBH5RLRsmumZCZCDCT3RnkdUw1HvOkzu9LuObTG493bhSqoSdasoeOxXLWdPeyxe3v3fdYghyLZCZAZBNlPMXkicoTcrmHAZDZD");


            string sender_name = contact_name;
            sender_name = sender_name == "" ? "." : ",*" + sender_name + "*.";
                string msg_heart = "Welcome to *Tech Valley*.This is *Walid Zayed* Thanks for contacting us" + sender_name + "\\nYou sent a message of type *" +  msg_type  + 
                                  "*, from Number *" + from_no + "* and message text  " + msg_body + "\\n" +
                                  "1️⃣ Contact Information\\n" +
                                  "2️⃣ Our Services\\n" + 
                                  "3️⃣ Contact us";

                    var body = "{" + "\n" +
                                  "\"messaging_product\": \"whatsapp\"," + "\n" +
                                  "\"recipient_type\": \"individual\"," + "\n" +
                                  "\"to\": \"" + from_no + "\"," + "\n" +
                                  "\"context\": {" + "\n" +
                                  "\"message_id\": \"" + msg_id + "\"" + "\n" +
                                  "            }," + "\n" +
                                  "\"type\": \"text\"," + "\n" +
                                  "\"text\": {" + "\n" +
                                  "          \"preview_url\": false," + "\n" +
                                  "          \"body\": \"" + msg_heart + "\"" + "\n" +
                                  "        }" + "\n" +
                                  " }";

            //   request.AddParameter("application/json", body, ParameterType.RequestBody);
            request.AddJsonBody(body, "application/json");

            //  IRestResponse response = client.Execute(request);
            var response = client.PostAsync(request);
          
          //  Console.WriteLine(response.ToString());
            return body;
        }
    }
}
