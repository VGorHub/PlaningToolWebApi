using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Models;
using System.Net;
using System.Security.Principal;
using PlaningToolWebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Text.RegularExpressions;

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EventController : ControllerBase

    {
        private readonly DBContext dbContext;
        
        IWebHostEnvironment _appEnvironment;

        public EventController(DBContext dbContext, IWebHostEnvironment appEnvironment)
        {
            this.dbContext = dbContext;
            
            _appEnvironment = appEnvironment;
        }

        // POST:= api/<EventController>/CreateEvent
        [HttpPost("CreateEvent")]
        public async Task<ActionResult> CreateEvent(int userId, int auditoryId, string startTime, string endTime, string name, string description, string type, string target, string date, IFormFileCollection? uploads)
        {

            // Валидация startTime и endTime
            Regex timeRegex = new Regex(@"^(1[0-6]|0[1-9]):[0-5][0-9]$");
            if (!timeRegex.IsMatch(startTime) || !timeRegex.IsMatch(endTime))
            {
                return BadRequest("Неправильный формат времени. Время должно быть от 10:00 до 17:00 в формате HH:mm.");
            }

            // Валидация date
            Regex dateRegex = new Regex(@"^(0[1-9]|[12][0-9]|3[01])\.(0[1-9]|1[0-2])\.\d{4}$");
            if (!dateRegex.IsMatch(date))
            {
                return BadRequest("Неправильный формат даты. Дата должна быть в формате DD.MM.YYYY.");
            }

            DateTime startDateTime = DateTime.Parse(startTime);
            DateTime endDateTime = DateTime.Parse(endTime);

            
            var eventsInAuditoryAndDate = dbContext.events.Where(p => p.auditoryId == auditoryId && p.date == date).ToList();

            
            foreach (var existingEvent in eventsInAuditoryAndDate)
            {
                DateTime existingStartDateTime = DateTime.Parse(existingEvent.start_time);
                DateTime existingEndDateTime = DateTime.Parse(existingEvent.end_time);

                if ((startDateTime >= existingStartDateTime && startDateTime < existingEndDateTime) ||
                    (endDateTime > existingStartDateTime && endDateTime <= existingEndDateTime) ||
                    (startDateTime <= existingStartDateTime && endDateTime >= existingEndDateTime))
                {
                    return BadRequest("The audience is unavailable at the specified time.");
                }
            }

            Event newEvent = new Event();
            newEvent.userId = userId;
            newEvent.auditoryId = auditoryId;
            newEvent.start_time = startTime;
            newEvent.end_time = endTime;
            newEvent.name = name;
            newEvent.description = description;
            newEvent.type = type;
            newEvent.target = target;
            newEvent.date = date;

            string cleanedStartTime = Regex.Replace(startTime ?? "", "[^0-9a-zA-Z]+", "-");
            string cleanedDate = Regex.Replace(date ?? "", "[^0-9a-zA-Z]+", "_");

            foreach (var uploadedFile in uploads)
            {
                // путь к папке Files
                string folderPath = Path.Combine(_appEnvironment.ContentRootPath, "Files", $"{name}_{cleanedStartTime}_{cleanedDate}");//_appEnvironment.webRootPath

                // Создаем директорию, если она не существует
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // путь к файлу
                string filePath = Path.Combine(folderPath, uploadedFile.FileName);

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }

                // Получаем текущий массив строк files из события
                string[] currentFiles = newEvent.files ?? new string[0]; // Если files пустой, создаем пустой массив строк

                // Создаем новый массив строк, включающий все текущие файлы и новый файл
                string[] updatedFiles = currentFiles.Concat(new string[] { filePath }).ToArray();

                // Обновляем свойство files у события
                newEvent.files = updatedFiles;
            }

            dbContext.events.Add(newEvent);
            dbContext.SaveChanges();
            return Ok(_appEnvironment);
        }


    }
}
