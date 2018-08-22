using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TestMaker.Models.Data
{
    public class ApplicationUser: IdentityUser
    {
        public string  DisplayName { get; set; }

        public string  Notes { get; set; }

        [Required]
        public int Type { get; set; }

        [Required]
        public int Flags { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime LastModifiedDate { get; set; }

        #region Lazy-Load Properties
        
        public virtual List<Quiz> Quizzes { get; set; }
        public virtual List<Token> Tokens { get; set; }

        #endregion
    }
}
