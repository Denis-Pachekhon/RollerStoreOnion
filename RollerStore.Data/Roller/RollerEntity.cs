using RollerStore.Data.Store;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RollerStore.Data.Roller
{
    [Table("tbl_roller")]
    public class RollerEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("store_id")]
        [ForeignKey("store")]
        public int StoreId { get; set; }
        public StoreEntity Store { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("price")]
        public double Price { get; set; }

        [Column("isDeleted")]
        public bool IsDeleted { get; set; }
    }
}
