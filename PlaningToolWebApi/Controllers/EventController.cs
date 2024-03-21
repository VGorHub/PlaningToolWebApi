using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Models;
using System.Net;
using System.Security.Principal;
using PlaningToolWebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EventController : ControllerBase

    {
        private readonly DBContext dbContext;
        ApplicationContext _context;
        IWebHostEnvironment _appEnvironment;

        public EventController(DBContext dbContext, ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            this.dbContext = dbContext;
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // POST:= api/<EventController>/CreateEvent
        [HttpPost("CreateEvent")]
        public ActionResult CreateEvent(int userId, int auditoryId, string startTime, string endTime, string name, string description, string type, string target, string date, IFormFileCollection? uploads)
        {
           
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
            dbContext.events.Add(newEvent);
            dbContext.SaveChanges();
            foreach (var uploadedFile in uploads)
            {
                // путь к папке Files
                string path = "/Files/" + uploadedFile.FileName;
                // сохраняем файл в папку Files в каталоге wwwroot
                
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    uploadedFile.CopyToAsync(fileStream);
                }
                FileModel file = new FileModel { Name = uploadedFile.FileName, Path = path };
                _context.Files.Add(file);
            }
            _context.SaveChanges();
            return Ok();
        }


    }
}
