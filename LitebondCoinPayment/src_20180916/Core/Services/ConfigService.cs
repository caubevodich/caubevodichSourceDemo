using Core.Data;
using Core.Domain.Entities;

namespace Core.Services
{
    public interface IConfigService : IEntityService<Config>
    {
    }

    public class ConfigService : EntityService<Config>, IConfigService
    {
        public ConfigService(IDbContext context) : base(context)
        {
        }
    }
}