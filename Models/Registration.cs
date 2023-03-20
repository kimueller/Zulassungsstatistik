using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSV_FileReader.Models
{
    public class Registration
    {
        [Key]
        public int RegistrationID { get; set; }

        public string ManufacturerID { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string TimeID { get; set; }
        public Time Time{ get; set; }

        [Required]
        public string RegistrationType { get; set; }

        [Required]
        public int Amount{ get; set; }
    }
}
