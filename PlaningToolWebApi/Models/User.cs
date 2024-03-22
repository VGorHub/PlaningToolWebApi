using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PlaningToolWebApi.Models
{
    [Table("users")]
    public class User
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string login { get; set; }
        public string role{ get; set;}
        public string password { get; set; }

    }
}
    