using System.Linq.Expressions;
using MediatR;

namespace PLUG.System.Common.Application;

public abstract record ApplicationQueryBase<TResult> : IRequest<TResult>
{
}
