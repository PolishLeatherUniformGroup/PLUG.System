using ONPA.Common.Application;

namespace ONPA.Common.Queries;

public static class PageableResultExtension
{
    public static PageableResult<T> FromQueryResult<T,TQuery>(this CollectionResult<T> result, TQuery query) 
        where TQuery: ApplicationCollectionQueryBase<T>
        where T : class
    {
        return new PageableResult<T>(result.Value, result.Total, query.Page, query.Limit);
    }
}