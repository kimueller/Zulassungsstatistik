using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_FileReader.Models
{
    public class Time
    {
        [Key]
        public string TimeID { get; set; }

        [Required]
        public string Month { get; set; }
        
        [Required]
        public int Year { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }
    }
}
