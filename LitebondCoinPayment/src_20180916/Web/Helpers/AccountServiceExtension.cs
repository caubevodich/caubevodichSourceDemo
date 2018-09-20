using Core.Data;
using Core.Domain.Entities;
using WebUI.Models;
using System.Linq;
namespace WebUI.Helpers
{
    public class AccountServiceExtension
    {
        private readonly ApplicationDbContext _context;

        public AccountServiceExtension()
        {
            _context = new ApplicationDbContext();
        }

        public void Update(ApplicationUser account)
        {
            _context.Entry(account).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }

        public ApplicationUser GetByEmail(string email)
        {
            return _context.Set<ApplicationUser>().FirstOrDefault(x => x.Email == email);
        }
    }
}