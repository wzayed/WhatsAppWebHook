using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using System.Text.Json;
using WhatsWebHook.Data;
using WhatsWebHook.Functions;
using WhatsWebHook.Models;
using static System.Net.Mime.MediaTypeNames;


namespace WhatsWebHook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReadnReplysController : ControllerBase
    {
        readonly AppDbContext  _context;
        readonly RestSharp_fns _rest_fns;
        public ReadnReplysController(AppDbContext dbcontext, RestSharp_fns rest_fns)
        {
             _context = dbcontext;
            _rest_fns = rest_fns;
            
        }
       public List<string> _fruit = new List<string> {
            "Mango",
            "Banana",
            "Apple"};
        [HttpGet("/TheHook")]
       public ActionResult<string> YumyYumyget()
        {
            var my_mode = ControllerContext.HttpContext.Request.Query["hub.mode"];

            var my_verify_token = ControllerContext.HttpContext.Request.Query["hub.verify_token"];
            if(ControllerContext.HttpContext.Request.Query["hub.verify_token"].ToString() == "AseyahSafa")
            { 
               // Console.WriteLine(my_verify_token);
                return Ok(ControllerContext.HttpContext.Request.Query["hub.challenge"].ToString());
            }
            else
            {
              //  Console.WriteLine(my_verify_token);
                return Forbid(ControllerContext.HttpContext.Request.Query["hub.challenge"].ToString());
              
            }
        }
       [HttpPost("/TheHook")]       
        public async Task<IActionResult> receivemsgAsync( JsonDocument  json_Document)
        {
           
            JsonElement root = json_Document.RootElement;
            JsonElement entry = root.GetProperty("entry")[0];
            JsonElement changes = entry.GetProperty("changes")[0];
            JsonElement value = changes.GetProperty("value");
            JsonElement message = value.GetProperty("messages")[0];            
            JsonElement contact = value.GetProperty("contacts")[0];
            contact = contact.GetProperty("profile");
            contact = contact.GetProperty("name");
            JsonElement msg_id = message.GetProperty("id");
            JsonElement from_no = message.GetProperty("from");
            JsonElement msg_type = message.GetProperty("type");
            JsonElement msg_body = new JsonElement();
            if (msg_type.ToString() == "text") {             
                msg_body = message.GetProperty("text");
                msg_body = msg_body.GetProperty("body");                            
                }           
            
  

            ///////////////////////////////////////////////////////////////////////////////////////////////////////
            // Adding to database///////////////////////////////////////////////////////////////////////////////////
            string status_string = "";
            string return_str ="";
            var usr =  await _context.user.Where(p=> p.phone_number == from_no.ToString()).FirstOrDefaultAsync<Users>();
  
            if (usr is null)
            {
                usr = new Users()
                {
                    phone_number = from_no.ToString(),
                    UserName = contact.ToString() ,
                    status_id = 0,
                    status_name = ""
                };
                _context.Add(usr);
             await   _context.SaveChangesAsync();
            }
            else          
                if(usr.status_name == "main")
            {
                switch (msg_body.ToString())
                {

                    case "1":
                        status_string = "*Info@Tech-Valley.Tech*\\n*+971-52-296-6061 - +971-56-940-2217*";
                        usr.status_name = "";
                        break;
                    case "2":
                        status_string = "We Provide services in :\\n*Software development and optimization.*\\n*Database management, migration and optimization.*\\n*Integration Solutions among software systems, different databases.*";
                        usr.status_name = "";
                        break;
                    case "3":
                        status_string = "*Write your inquiry/comment and we will reply when needed.*";
                        usr.status_name = "comment_by_user";
                        break;
                    default:
                        status_string = "Please, Enter a Valid Input *(1, 2, or 3).*";
                        break;
                }              

            }
            else if(usr.status_name == "comment_by_user")
            {
                status_string = "Thanks for your comment. It will be *attentively attented to*.";
                usr.status_name = "";
                UserComments usrComment = new UserComments
                {
                    IdUser = usr.IdUser,
                    user_comment = msg_body.ToString(),
                    msg_id = msg_id.ToString()
                };
               _context.Add(usrComment);
                try { 
              await  _context.SaveChangesAsync();
                }
                catch(Exception e)
                {

                }
            }
            else
            {
                usr.status_name = "main";       
                status_string = "";
            }
           await  _context.SaveChangesAsync();


            return_str = MessageProcess.replyMessage(from_no.ToString(), msg_type.ToString(), msg_body.ToString(), msg_id.ToString(), contact.ToString(), status_string, _rest_fns);

       

            Messages msg = new Messages()
            {
                full_message = return_str,
                original_message = JsonSerializer.Serialize(json_Document),
                IdUser = usr.IdUser
            };
            _context.Add(msg);
            await _context.SaveChangesAsync();

            return Ok(return_str);        
        } 

    }
}
