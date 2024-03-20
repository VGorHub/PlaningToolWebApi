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

        // GET api/Auditories/getAuditories
        [HttpGet("getAuditories")]
        public ActionResult<IEnumerable<Auditorie>> GetAuditories()
        {
            var auditories = dbContext.Auditories.ToList();

            if (auditories.Count == 0)
                return NotFound();

            return auditories;
        }
    }
}