using Logic.Core;

namespace Logic.AppServices
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Result<TResult> Handle(TQuery query);
    }
}
