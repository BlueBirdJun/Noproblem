using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Repository.NopModels
{
    public class Member : BaseModel
    {

        [Required]
        [MaxLength(50)]        
        public string UserId { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string BirthDay { get; set; }
        [MaxLength(100)]
        public string Address1 { get; set; }
        [MaxLength(100)]
        public string Address2 { get; set; }
        
        public string Memo { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
