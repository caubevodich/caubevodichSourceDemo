namespace Core.Domain.Entities
{
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual new T Id { get; set; }
    }
}