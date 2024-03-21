using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PlaningToolWebApi.Models;
using PlaningToolWebApi.Context;
using Microsoft.EntityFrameworkCore;

namespace PlaningToolWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly DBContext dbContext;

        public StatisticsController(DBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("auditorie/people-count")]
        public IActionResult GetPeopleCountByAuditorie()
        {
            var peopleCountByAuditorie = new Dictionary<string, int>();

            var membersList = dbContext.members.ToList();

            foreach (var member in membersList)
            {
                var eventAuditoryId = dbContext.events.Select(e => new {
                    e.id,
                    e.userId,
                    e.auditoryId,
                    e.start_time,
                    e.end_time,
                    e.name,
                    e.description,
                    e.type,
                    e.target,
                    e.date
                }).FirstOrDefault(e => e.id == member.eventid)?.auditoryId;

                if (eventAuditoryId != null)
                {
                    var auditoryName = dbContext.auditories.FirstOrDefault(a => a.id == eventAuditoryId)?.name;

                    if (auditoryName != null)
                    {
                        if (!peopleCountByAuditorie.ContainsKey(auditoryName))
                        {
                            peopleCountByAuditorie[auditoryName] = 1;
                        }
                        else
                        {
                            peopleCountByAuditorie[auditoryName]++;
                        }
                    }
                }
            }

            return Ok(peopleCountByAuditorie);
        }


        [HttpGet("auditorie/occupancy")]//возвращает процент времени, в течение которого каждая аудитория была занята мероприятиями.
        public IActionResult GetAuditorieOccupancy()
        {
            var statistics = new Dictionary<string, double>();
            var auditorieList = dbContext.auditories.ToList();

            foreach (var auditorie in auditorieList)
            {
                var eventsInAuditorie = dbContext.events.Select(e => new {
                    e.id,
                    e.userId,
                    e.auditoryId,
                    e.start_time,
                    e.end_time,
                    e.name,
                    e.description,
                    e.type,
                    e.target,
                    e.date
                }).Where(e => e.auditoryId == auditorie.id).ToList();
                double totalOccupancy = 0;
                TimeSpan totalDuration = TimeSpan.Zero;

                foreach (var evnt in eventsInAuditorie)
                {
                    if (DateTime.TryParse(evnt.start_time, out DateTime startTime) && DateTime.TryParse(evnt.end_time, out DateTime endTime))
                    {
                        totalDuration += endTime - startTime;
                    }
                }

                // Рассчитываем процент занятости аудитории
                if (totalDuration.TotalMinutes > 0)
                {
                    double occupancyPercentage = (totalDuration.TotalMinutes / (7 * 60)) * 100; // Предполагаем, что сутки = 24 часа
                    statistics[auditorie.name] = occupancyPercentage;
                }
                else
                {
                    statistics[auditorie.name] = 0; // Если аудитория не была занята, устанавливаем занятость в 0%
                }
            }

            return Ok(statistics);
        }

        [HttpGet("auditorie/events")]//возвращает количество мероприятий, проведенных в каждой аудитории.
        public IActionResult GetEventsCountByAuditorie()
        {
            var statistics = new Dictionary<string, int>();
            var auditorieList = dbContext.auditories.ToList();

            foreach (var auditorie in auditorieList)
            {
                var eventsCount = dbContext.events.Count(e => e.auditoryId == auditorie.id);
                statistics[auditorie.name] = eventsCount;
            }

            return Ok(statistics);
        }
    
    }
}
