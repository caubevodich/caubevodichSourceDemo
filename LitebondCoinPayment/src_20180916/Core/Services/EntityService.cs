using Core.Data;
using Core.Domain.Entities;

namespace Core.Services
{
    public class EntityService<T> : EfRepository<T>, IEntityService<T> where T : BaseEntity
    {
        public EntityService(IDbContext context) : base(context)
        {
        }
    }
}