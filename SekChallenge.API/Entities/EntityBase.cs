namespace SekChallenge.API.Entities
{
    public abstract class EntityBase : IEntity
    {
        public EntityBase() { }

        public Guid Id { get; set; }
    }
}
