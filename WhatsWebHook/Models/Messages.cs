using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WhatsWebHook.Models
{
    public class Messages
    {
        [Key]
        public int Idmessage { get; set; }

        public int IdUser { get; set; }
        public string? full_message { get; set; }
        public string? original_message { get; set; }

        [ForeignKey("IdUser")]
        Users user { get; set; }

    }
}
