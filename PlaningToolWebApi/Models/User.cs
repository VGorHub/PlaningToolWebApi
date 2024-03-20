using PlaningToolWebApi.Models;
namespace PlaningToolWebApi.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }
        public Role Role{ get; set;}
        public int Age { get; set; }

    }
}
