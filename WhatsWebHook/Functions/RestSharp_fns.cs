using Microsoft.Extensions.Options;
using RestSharp;

namespace WhatsWebHook.Functions
{

    public class RestSharp_fns : IRestSharp_fns
    {
        public string msg_url { get; set; }        
        public string  body { get; set; }
        public RestClientOptions RC_options;

        RestRequest request;
        RestClient client;
        public RestSharp_fns()
        {
            var RC_options = new RestClientOptions("https://graph.facebook.com/v13.0/100704419435623/messages")
            {
                ThrowOnAnyError = true,
                MaxTimeout = -1
            };
            client = new RestClient(RC_options);

            request = new RestRequest();
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Bearer EAALDinrOTh4BAGS3GZAeAjagdrUFzFyE0uXqtZAHKEUUYyK9p9P9VtpsMx3ZApdDWuVLbT0cFkWZCkCWhd9IYv12vkJWFYmZArGxLqE6DQCKEWMYvwBH5RLRsmumZCZCDCT3RnkdUw1HvOkzu9LuObTG493bhSqoSdasoeOxXLWdPeyxe3v3fdYghyLZCZAZBNlPMXkicoTcrmHAZDZD");

        }
        public async Task<RestResponse> post_async()
        {


            //Playground


            //  request.AddParameter("application/json", body, ParameterType.RequestBody);

            //request.Parameters.AddParameter()
            /*request.Parameters.RemoveParameter
            ParameterType.RequestBody*/
            //End of playGround
            //   request.AddJsonBody(body, "application/json");

            Parameter bodyParameter;
            try
            {
                bodyParameter = request.Parameters.OfType<BodyParameter>().Single();

                request.RemoveParameter(bodyParameter);
            }
            catch (Exception e) { }

            request.AddJsonBody(body, "application/json");

            request.Method = Method.Post;
            var response = await client.PostAsync(request);
            return response;
            
        }
    }
}
