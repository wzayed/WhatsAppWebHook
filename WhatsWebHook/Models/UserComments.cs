using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsWebHook.Models
{
    public class UserComments
    {
        [Key]
        public int IdUserComment { get; set; }
        public int IdUser { get; set; }
        public string? user_comment { get; set; }
        public string? msg_id { get; set; }

        [ForeignKey("IdUser")]
        public Users usr { get; set; }
    }
}
