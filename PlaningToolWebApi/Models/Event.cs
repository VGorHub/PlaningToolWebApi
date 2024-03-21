using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PlaningToolWebApi.Models
{
    
    [Table("events")]
    public class Event
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int userId {  get; set; }
        public int auditoryId {  get; set; }
        //Структура DateTime для Времени
        //https://metanit.com/sharp/tutorial/19.1.php
        public string start_time {  get; set; }
        public string end_time { get; set; }
        public string name {  get; set; }
        public string description { get; set; }
        public string type { get; set; }
        public string target {  get; set; }

    }
}
