using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Models;
using PlaningToolWebApi.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriesController : ControllerBase
    {
        private readonly DBContext dbContext;

        public AuditoriesController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET:= api/<AuditoriesController>/getAuditories
        [HttpGet("getAuditories")]
        public ActionResult<IEnumerable<Auditorie>> GetAuditories()
        {
            var auditories = dbContext.Auditories.ToList();

            if (auditories.Count == 0)
                return NotFound();

            return auditories;
        }

        // GET: api/<AuditoriesController>/getAuditoriesEvents/5
        [HttpGet("getAuditoriesEvents/{auditoryId}")]
        public ActionResult<IEnumerable<Event>> GetAuditoriesEvents(int auditoryId)
        {
            var events = dbContext.Events.Where(p => p.AuditoryId == auditoryId).ToList();

            if (!events.Any())
                return NotFound();

            return events;
        }


        // GET api/<usersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
    }
}