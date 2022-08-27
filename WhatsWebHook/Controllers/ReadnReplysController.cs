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
        AppDbContext  _context;
        public ReadnReplysController(AppDbContext dbcontext)
        {
             _context = dbcontext;
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
        public  IActionResult receivemsgAsync( JsonDocument  json_Document)
        {

            /*    System.IO.File.WriteAllText("webapi2.txt", "HiHi");
                System.IO.File.WriteAllText("webapi.txt", JsonSerializer.Serialize(data));

            // Console.WriteLine(JsonSerializer.Serialize(data));*/
           
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
            

           string str = MessageProcess.replyMessage(from_no.ToString(), msg_type.ToString(), msg_body.ToString(), msg_id.ToString(),contact.ToString());
          
            var usr = _context.user.AsNoTracking().Where(p=> p.phone_number == from_no.ToString()).FirstOrDefault();
  
            if (usr is null)
            {
                usr = new Users()
                {
                    phone_number = from_no.ToString(),
                    UserName = contact.ToString() ,
                    status_id = 0,
                };
                _context.Add(usr);
                _context.SaveChanges();
            }
            Messages msg = new Messages()
            {
                full_message = str,
                original_message = JsonSerializer.Serialize(json_Document),
                IdUser = usr.IdUser
            };
            _context.Add(msg);
            _context.SaveChanges();

            return Ok(str);
        
        } 

    }
}
