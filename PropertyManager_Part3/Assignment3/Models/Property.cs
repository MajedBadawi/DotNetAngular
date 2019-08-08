﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment3.Models {
    public class Property {
        [Key]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Address { get; set; }

        [Required]
        [Column(TypeName = "integer")]
        public int NumberOfRooms { get; set; }

        [Required]
        [Column(TypeName = "float")]

        public float Price { get; set; }

        [Column(TypeName = "char")]
        public char Purchased { get; set; }

        public User currentBuyer { get; set; }

        public Property() {
            this.Purchased = '0';
        }
    }
}
