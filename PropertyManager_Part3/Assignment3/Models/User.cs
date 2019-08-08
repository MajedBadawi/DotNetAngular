using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models {
    public class User  : IdentityUser {
        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [Required]
        [Column(TypeName = "float")]
        public float Credit { get; set; }

        [Required]
        [Column(TypeName = "integer")]
        public int NumberOfOwnedProperties { get; set; }

        public string Password { get; set; }

        public List<Property> OwnedProperties { get; set; }

        public User() {
            NumberOfOwnedProperties = 0;
        }
    }
}
