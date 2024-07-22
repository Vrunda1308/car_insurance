using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Car_Insurance.Models
{
    public class Insuree
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public int CarYear { get; set; }

        [Required]
        [StringLength(50)]
        public string CarMake { get; set; }

        [Required]
        [StringLength(50)]
        public string CarModel { get; set; }

        [Required]
        public bool DUI { get; set; }

        [Required]
        public int SpeedingTickets { get; set; }

        [Required]
        public bool CoverageType { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Quote { get; set; }
        public bool HasDUI { get; internal set; }
        public bool IsFullCoverage { get; internal set; }
    }
}