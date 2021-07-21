using System;
using System.Linq;
using WebApiEntity;

namespace OzonLogsInterview.Search
{
    /// <summary>
    /// Represents a filter that used for searching in logs database.
    /// </summary>
    public class LogsSearchFilter : ISearchFilter<LogModel>
    {
        private readonly DateTime _dateFrom;
        private readonly DateTime _dateTo;

        private readonly string _inContent;
        private readonly string _inAction;

        private readonly LogLevel _level;

        public LogsSearchFilter(
            LogLevel level = default, 
            string inContent = default,
            string inAction = default, 
            DateTime dateFrom = default,
            DateTime dateTo = default)
        {
            _level = level;
            _inAction = inAction;
            _inContent = inContent;
            _dateTo = dateTo;
            _dateFrom = dateFrom;
        }

        public LogModel[] Search(IQueryable<LogModel> logs)
        {
            if (_dateFrom != default)
            {
                logs = logs.Where(l => l.Date >= _dateFrom);
            }

            if (_dateTo != default)
            {
                logs = logs.Where(l => l.Date <= _dateTo);
            }

            if (_inContent != default)
            {
                logs = logs.Where(l => l.Content.Contains(_inContent));
            }
            
            if (_inAction != default)
            {
                logs = logs.Where(l => l.Action.Contains(_inAction));
            }

            if (_level != default)
            {
                logs = logs.Where(l => l.Level == _level);
            }
            return logs.ToArray();
        }
    }
}