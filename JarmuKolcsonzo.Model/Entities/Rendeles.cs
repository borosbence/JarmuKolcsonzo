using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JarmuKolcsonzo.Model.Entities
{
    // [Index("jarmu_id", Name = "jarmu_id")]
    // [Index("ugyfel_id", Name = "ugyfel_id")]
    [Table("rendelesek")]
    public partial class Rendeles
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [Column(TypeName = "int(11)")]
        public int ugyfel_id { get; set; }
        [Column(TypeName = "int(11)")]
        public int jarmu_id { get; set; }
        public DateTime datum { get; set; }
        [Column(TypeName = "int(3)")]
        public int napok_szama { get; set; }
        // [Precision(7, 0)]
        public decimal ar { get; set; }

        [JsonIgnore]
        [ForeignKey("jarmu_id")]
        [InverseProperty("rendelesek")]
        public virtual Jarmu? jarmu { get; set; }

        [JsonIgnore]
        [ForeignKey("ugyfel_id")]
        [InverseProperty("rendelesek")]
        public virtual Ugyfel? ugyfel { get; set; }
    }
}
