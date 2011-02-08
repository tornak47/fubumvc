using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FubuCore.Reflection;

namespace FubuFastPack.Querying
{
    // Tested by StoryTeller
    public class QueryService : IQueryService
    {
        private readonly IEnumerable<IFilterHandler> _handlers;

        public QueryService(IEnumerable<IFilterHandler> handlers)
        {
            _handlers = handlers;
        }

        public IEnumerable<OperatorKeys> FilterOptionsFor<TEntity>(Accessor accessor)
        {
            return allHandlers().SelectMany(x => x.FilterOptionsFor<TEntity>(accessor));
        }

        public Expression<Func<T, bool>> WhereFilterFor<T>(FilterRequest<T> request)
        {
            return allHandlers().First(h => h.Handles(request)).WhereFilterFor(request);
        }

        private IEnumerable<IFilterHandler> allHandlers()
        {
            foreach (var filterHandler in _handlers)
            {
                yield return filterHandler;
            }

            foreach (var filterHandler in BasicFiltersCache.AllHandlers)
            {
                yield return filterHandler;
            }
        }
    }
}