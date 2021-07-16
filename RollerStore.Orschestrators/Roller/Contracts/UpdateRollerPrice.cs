using System;
using System.ComponentModel.DataAnnotations;

namespace RollerStore.Orschestrators.Roller.Contracts
{
    public class UpdateRollerPrice
    {
        [Required]
        [Range(1, Double.MaxValue, ErrorMessage = "Wrong price")]
        public double Price { get; set; }
    }
}
