using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment2.Models {
    public class Buyer {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Password { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public double Credit { get; set; }

        [Required]
        [Column(TypeName = "integer")]
        public int NumberOfOwnedProperties { get; set; }

        public List<Property> OwnedProperties { get; set; }
    }
}
