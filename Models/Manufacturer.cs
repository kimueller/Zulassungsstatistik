using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_FileReader.Models
{
    public class Manufacturer
    {
        [Key]
        public string ManufacturerID { get; set; }

        [Required]
        public string Title { get; set; }

        public virtual ICollection<Registration> Registration { get; set; }

    }
}
