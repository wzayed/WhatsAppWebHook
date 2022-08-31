using System.ComponentModel.DataAnnotations;

namespace WhatsWebHook.Models
{
    public class Users
    {
        [Key]
        public int IdUser { get; set; }
        public string? UserName { get; set; }
        public string phone_number { get; set; }
        public int status_id { get; set; }
        public string? status_name { get; set; }

        ICollection<Messages> msgs { get; set; }
        ICollection<UserComments> comments { get; set; }

    }
}