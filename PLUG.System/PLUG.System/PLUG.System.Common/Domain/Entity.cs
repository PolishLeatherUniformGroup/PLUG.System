namespace PLUG.System.Common.Domain;

public abstract class Entity
{
    public Entity(Guid id)
    {
        this.Id = id;
    }
    
    protected Entity(){}
    
    public Guid Id
    {
        get;
        protected set;
    }
}