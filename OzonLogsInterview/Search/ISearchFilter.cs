using System.Linq;

namespace OzonLogsInterview.Search
{
    public interface ISearchFilter<T>
    {
        public T[] Search(IQueryable<T> logs);
    }
}