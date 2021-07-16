using System.ComponentModel.DataAnnotations;

namespace RollerStore.Orschestrators.Store.Contracts
{
    public class CreateStore
    {
        [Required]
        [MinLength(1, ErrorMessage = "Bad name")]
        [MaxLength(256, ErrorMessage = "Bad name")]
        public string Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Bad address")]
        [MaxLength(256, ErrorMessage = "Bad address")]
        public string Address { get; set; }
    }
}
