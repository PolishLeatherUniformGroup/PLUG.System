﻿using ONPA.Common.Domain;

namespace ONPA.Organizations.StateEvents;

public sealed class OrganizationCreated : StateEventBase
{
    public string Name { get; private set; }
    public string CardPrefix { get; private set; }
    public string TaxId { get; private set; }
    public string AccountNumber { get; private set; }
    public string Address { get; private set; }
    public string ContactEmail { get; private set; }
    public string? Regon { get; private set; }
    
    public OrganizationCreated(string name, string cardPrefix, string taxId, string accountNumber, string address,
        string contactEmail, string? regon = null)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.ContactEmail = contactEmail;
        this.Regon = regon;
    }
    
    private OrganizationCreated(Guid aggregateId, long aggregateVersion, string name, string cardPrefix, string taxId, string accountNumber, string address, string contactEmail, string? regon = null) : base(aggregateId,aggregateVersion)
    {
        this.Name = name;
        this.CardPrefix = cardPrefix;
        this.TaxId = taxId;
        this.AccountNumber = accountNumber;
        this.Address = address;
        this.Regon = regon;
    }

    public override IStateEvent WithAggregate(Guid aggregateId, long aggregateVersion)
    {
        return new OrganizationCreated(aggregateId, aggregateVersion, this.Name, this.CardPrefix, this.TaxId, this.AccountNumber, this.Address, this.ContactEmail,this.Regon);
    }
}