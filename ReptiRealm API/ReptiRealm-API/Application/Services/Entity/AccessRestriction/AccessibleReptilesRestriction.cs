using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Infrastructure.Data;
using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;

namespace ReptiRealm_API.Application.Services.Entity.AccessRestriction
{
    public class AccessibleReptilesRestriction(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IAccessRestrictionMethod<Reptile>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public IQueryable<Reptile> Filter(IQueryable<Reptile> source)
        {
            return FilterByUser(source);
        }

        private IQueryable<Reptile> FilterByUser(IQueryable<Reptile> source)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
            var user = _context.Set<User>().SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return _context.Set<Reptile>().Where(r => r.UserId == user.Id);
        }
    }
}
