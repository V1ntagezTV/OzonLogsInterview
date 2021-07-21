using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonLogsInterview.Search;
using WebApiEntity;
using JsonSerializer = System.Text.Json.JsonSerializer;
using LogLevel = WebApiEntity.LogLevel;

namespace OzonLogsInterview.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogsController : ControllerBase
    {
        private readonly LogContext _context;

        public LogsController(LogContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Add new log to database.<br/>
        /// (Datetime format ex: 2012-12-31T22:00:00.000Z)
        /// </summary>
        [HttpGet("Add")][ActionName("Add")]
        public async Task<int> AddLog(string content, string action, DateTime date, LogLevel level)
        {
            var log = new LogModel()
            {
                Content = content,
                Action = action,
                Date = date,
                Level = level
            };
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
            return log.Id;
        }
        
        /// <summary>
        /// Return logs count that was deleted from database.
        /// </summary>
        [HttpGet("Remove")][ActionName("Remove")]
        public async Task<int> Remove(int logId)
        {
            var all = _context.Logs.ToList();
            var logs = _context.Logs.Where(i => i.Id == logId).ToList();
            _context.Set<LogModel>().RemoveRange(logs);
            await _context.SaveChangesAsync();
            return logs.Count;
        }
        
        /// <summary>
        /// Return statistics about current database state.
        /// </summary>
        [HttpGet("GetStats")][ActionName("GetStats")]
        public string GetStats()
        {
            var logsInData = _context.Logs.ToArray().Length;
            var logsCountGroupByLevel = _context.Logs
                .GroupBy(l => l.Level)
                .Select(l => new { l.Key, Count = l.Count() })
                .ToDictionary(x => x.Key, x=> x.Count);;
            var logsCountGroupByAction = _context.Logs
                .GroupBy(l => l.Action)
                .Select(l => new { l.Key, Count = l.Count() })
                .ToDictionary(x => x.Key, x=> x.Count);
            
            return JsonSerializer.Serialize(new
            {
                Count=logsInData,
                GroupByLevel=logsCountGroupByLevel,
                GroupByAction=logsCountGroupByAction
            });
        }
        
        [HttpGet("SearchByDate")]
        public LogModel[] SearchByDate(DateTime from, DateTime to)
        {
            var filter = new LogsSearchFilter(dateFrom: from, dateTo: to);
            return filter.Search(_context.Logs);
        }
        
        [HttpGet("SearchInContent")]
        public LogModel[] SearchInContent(string inContent)
        {
            var filter = new LogsSearchFilter(inContent: inContent);
            return filter.Search(_context.Logs);
        }
        
        [HttpGet("SearchInAction")]
        public LogModel[] SearchInAction(string inAction)
        {
            var filter = new LogsSearchFilter(inAction: inAction);
            return filter.Search(_context.Logs);
        }
        
        [HttpGet("SearchByLevel")]
        public LogModel[] SearchByLogLevel(LogLevel level)
        {
            var filter = new LogsSearchFilter(level);
            return filter.Search(_context.Logs);
        }

        [HttpGet("SearchByFilter")]
        public LogModel[] SearchByFilter(
            DateTime dateFrom=default, DateTime dateTo=default,
            string inContent=default, string inAction=default,
            LogLevel logLevel=default)
        {
            var filter = new LogsSearchFilter(logLevel, inContent, inAction, dateFrom, dateTo);
            return filter.Search(_context.Logs);
        }
    }
}
