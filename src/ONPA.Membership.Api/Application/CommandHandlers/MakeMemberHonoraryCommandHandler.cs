﻿using ONPA.Common.Application;
using ONPA.Common.Domain;
using ONPA.Common.Exceptions;
using ONPA.Membership.Api.Application.Commands;
using ONPA.Membership.Domain;

namespace ONPA.Membership.Api.Application.CommandHandlers;

public sealed class MakeMemberHonoraryCommandHandler : ApplicationCommandHandlerBase<MakeMemberHonoraryCommand>
{
    private readonly IAggregateRepository<Member> _aggregateRepository;

    public MakeMemberHonoraryCommandHandler(IAggregateRepository<Member> aggregateRepository)
    {
        _aggregateRepository = aggregateRepository;
    }
    
    public override async Task<CommandResult> Handle(MakeMemberHonoraryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var aggregate = await this._aggregateRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (aggregate is null)
            {
                throw new AggregateNotFoundException();
            }
            aggregate.MakeHonoraryMember();
            aggregate = await this._aggregateRepository.UpdateAsync(aggregate, cancellationToken);
            return aggregate.AggregateId;
        }
        catch (DomainException exception)
        {
            return exception;
        }
    }
}