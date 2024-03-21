namespace PlaningToolWebApi.Models
{
    public class Event
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public int AuditoryId {  get; set; }
        //Структура DateTime для Времени
        //https://metanit.com/sharp/tutorial/19.1.php
        public string StartTime {  get; set; }
        public string EndTime { get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Target {  get; set; }

    }
}
