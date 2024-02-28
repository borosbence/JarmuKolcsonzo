using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JarmuKolcsonzo.Model.Entities
{
    [Table("jarmu_tipusok")]
    public partial class JarmuTipus
    {
        public JarmuTipus()
        {
            jarmuvek = new HashSet<Jarmu>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(50)]
        public string megnevezes { get; set; } = null!;
        [Column(TypeName = "int(2)")]
        public int ferohely { get; set; }

        [JsonIgnore]
        [InverseProperty("tipus")]
        public virtual ICollection<Jarmu> jarmuvek { get; set; }
    }
}
