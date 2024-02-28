using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JarmuKolcsonzo.Model.Entities
{
    // [Index("rendszam", Name = "rendszam", IsUnique = true)]
    // [Index("tipus_id", Name = "tipus_id")]
    [Table("jarmuvek")]
    public partial class Jarmu
    {
        public Jarmu()
        {
            rendelesek = new HashSet<Rendeles>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(7)]
        public string rendszam { get; set; } = null!;
        [Column(TypeName = "int(11)")]
        public int tipus_id { get; set; }
        [Column(TypeName = "int(5)")]
        public int dij { get; set; }
        public bool elerheto { get; set; }
        public DateTime? szerviz_datum { get; set; }

        [ForeignKey("tipus_id")]
        [InverseProperty("jarmuvek")]
        public virtual JarmuTipus? tipus { get; set; }

        [JsonIgnore]
        [InverseProperty("jarmu")]
        public virtual ICollection<Rendeles> rendelesek { get; set; }
    }
}
