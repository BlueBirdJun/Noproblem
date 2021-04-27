using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Repository.NopModels
{
    public class CommonBoards :BaseModel
    {
        [MaxLength(200)]
        public string Title { get; set; }
        public string Contents { get; set; }

        [MaxLength(50)]
        public string Tag { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string UserId { get; set; }
        [MaxLength(2)]
        public char DelYN { get; set; }
    }
}
