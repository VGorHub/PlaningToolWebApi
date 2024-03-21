using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Models;
using System.Net;
using System.Security.Principal;
using PlaningToolWebApi.Context;

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class EventController : ControllerBase

    {
        private readonly DBContext dbContext;

        public EventController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // POST:= api/<EventController>/CreateEvent
        [HttpPost("getAuditories")]
        public ActionResult CreateEvent(int userId,int auditoryId,string startTime,string endTime,string name,string description,string type,string target)
        {
            Event newEvent = new Event();
            newEvent.userId = userId;
            newEvent.auditoryId = auditoryId;
            newEvent.start_time = startTime;
            newEvent.end_time = endTime;
            newEvent.name = name;
            newEvent.description = description;
            newEvent.type = type;
            newEvent.target = target;
            dbContext.events.Add(newEvent);
            dbContext.SaveChanges();
            return Ok();
        }

    }
}
