using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlaningToolWebApi.Models 
{ 


    [Table("auditories")]
    public class Auditorie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string name { get; set; }
        //public AuditorieType Type { get; set; }
        public string type { get; set; } 

    }
}
