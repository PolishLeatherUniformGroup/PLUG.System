using System.Linq.Expressions;
using MediatR;

namespace ONPA.Common.Application;

public abstract record ApplicationQueryBase<TResult> : IRequest<TResult>
{
}