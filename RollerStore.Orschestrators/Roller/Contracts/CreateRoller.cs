using System;
using System.ComponentModel.DataAnnotations;

namespace RollerStore.Orschestrators.Roller.Contracts
{
    public class CreateRoller
    {
        [Required]
        [MinLength(1, ErrorMessage = "Bad name")]
        [MaxLength(256, ErrorMessage = "Bad name")]
        public string Name { get; set; }

        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "Wrong price")]
        public double Price { get; set; }
    }
}
