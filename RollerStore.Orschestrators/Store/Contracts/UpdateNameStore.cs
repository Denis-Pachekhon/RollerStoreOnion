using System.ComponentModel.DataAnnotations;

namespace RollerStore.Orschestrators.Store.Contracts
{
    public class UpdateNameStore
    {
        [Required]
        [MinLength(1, ErrorMessage = "Bad name")]
        [MaxLength(256, ErrorMessage = "Bad name")]
        public string Name { get; set; }
    }
}
