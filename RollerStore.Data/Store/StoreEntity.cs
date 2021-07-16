using RollerStore.Data.Roller;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollerStore.Data.Store
{
    [Table("tbl_store")]
    public class StoreEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("isDeleted")]
        public bool IsDeleted { get; set; }

        public ICollection<RollerEntity> Rollers { get; set; }
    }
}
