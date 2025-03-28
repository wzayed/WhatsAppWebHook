﻿using Microsoft.OpenApi.Models;
using RestSharp;
using System.Text.Json;
namespace WhatsWebHook.Functions
{
    public static class MessageProcess
    {
        public static string  replyMessage(string from_no, string msg_type,string msg_body, string msg_id, string contact_name, string status_string, RestSharp_fns rest_fns)
        {

            // use dependency injection and make the dependancy singleton

            string  msg_url = "https://graph.facebook.com/v13.0/100704419435623/messages";



            string sender_name = contact_name;
            string img_link = "https://i.imgur.com/WBUravr.png";

            sender_name = sender_name == "" ? "." : ",*" + sender_name + "*.";
            string msg_heart = "Thanks" + sender_name + " for contacting *Tech Valley.* \\n" +
                           //   "From Number *" + from_no + "*\\n" +
                              "1️⃣ Contact Information\\n" +
                              "2️⃣ Our Services\\n" +
                              "3️⃣ Contact us";
            string type_and_info = "";


            if(status_string.Trim() == "") {
                type_and_info =  "\"type\": \"image\"," + "\n" +
                              "\"image\": {" + "\n" +
                              "          \"caption\": \"" + msg_heart + "\"," + "\n" +
                              "          \"link\": \"" + img_link + "\"" + "\n" +
                              "        }" + "\n" ;
                }
            else
            {
                //Add here the text message as a reply for the selected value
                type_and_info = "\"type\": \"text\"," + "\n" +
                            "\"text\": {" + "\n" +
                            "          \"body\": \"" + status_string + "\"" + "\n" + 
                            "        }" + "\n";
            }




            var body = "{" + "\n" +
                                  "\"messaging_product\": \"whatsapp\"," + "\n" +
                                  "\"recipient_type\": \"individual\"," + "\n" +
                                  "\"to\": \"" + from_no + "\"," + "\n" +
                                  "\"context\": {" + "\n" +
                                  "\"message_id\": \"" + msg_id + "\"" + "\n" +
                                  "            }," + "\n" +
                                    type_and_info +
                                  " }";

            
       
            rest_fns.msg_url = msg_url;
            rest_fns.body = body;

            rest_fns.post_async();
            return body;
        }
    }
}


/* The last perfectly working text message version
 *                  var body = "{" + "\n" +
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
 * */
// the one with white BK  string img_link = "https://i.imgur.com/0yv0CuQ.png";        
//Without the phone Number    string img_link = "https://i.imgur.com/slMpMfI.png";
/*


            var options = new RestClientOptions(msg_url)
            {
                ThrowOnAnyError = true,
                MaxTimeout = -1                
            };
*/
/*           var client = new RestClient(options);

           var request = new RestRequest();
           request.Method = Method.Post;
*/

/*
  request.AddHeader("Content-Type", "application/json");
  request.AddHeader("Authorization", "Bearer EAALDinrOTh4BAGS3GZAeAjagdrUFzFyE0uXqtZAHKEUUYyK9p9P9VtpsMx3ZApdDWuVLbT0cFkWZCkCWhd9IYv12vkJWFYmZArGxLqE6DQCKEWMYvwBH5RLRsmumZCZCDCT3RnkdUw1HvOkzu9LuObTG493bhSqoSdasoeOxXLWdPeyxe3v3fdYghyLZCZAZBNlPMXkicoTcrmHAZDZD");
  */

//   request.AddParameter("application/json", body, ParameterType.RequestBody);
/*            request.AddJsonBody(body, "application/json"); */

//  IRestResponse response = client.Execute(request);

//         var response = client.PostAsync(request);

//  Console.WriteLine(response.ToString());

/*        Old Message Working very good but it is long
         string msg_heart = "Welcome to *Tech Valley*.This is *Walid Zayed* Thanks for contacting us" + sender_name + "\\nYou sent a message of type *" + msg_type +
                              "*, from Number *" + from_no + "* and message text is( " + msg_body + ")\\n" +
                              "1️⃣ Contact Information\\n" +
                              "2️⃣ Our Services\\n" +
                              "3️⃣ Contact us";*/