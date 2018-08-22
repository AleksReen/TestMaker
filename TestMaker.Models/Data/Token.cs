using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestMaker.Models.Data
{
    public class Token
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Value { get; set; }

        public int Type { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
    }  
}
