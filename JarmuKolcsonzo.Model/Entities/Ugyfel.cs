using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JarmuKolcsonzo.Model.Entities
{
    [Table("ugyfelek")]
    public partial class Ugyfel
    {
        public Ugyfel()
        {
            rendelesek = new HashSet<Rendeles>();
        }

        [Key]
        [Column(TypeName = "int(11)")]
        public int id { get; set; }
        [StringLength(20)]
        public string vezeteknev { get; set; } = null!;
        [StringLength(20)]
        public string keresztnev { get; set; } = null!;
        [StringLength(50)]
        public string varos { get; set; } = null!;
        [Column(TypeName = "int(4)")]
        public int iranyitoszam { get; set; }
        [StringLength(250)]
        public string cim { get; set; } = null!;
        [StringLength(12)]
        public string telefonszam { get; set; } = null!;
        [StringLength(100)]
        public string email { get; set; } = null!;
        // [Precision(4, 2)]
        public decimal pont { get; set; }

        [JsonIgnore]
        [InverseProperty("ugyfel")]
        public virtual ICollection<Rendeles> rendelesek { get; set; }
    }
}
