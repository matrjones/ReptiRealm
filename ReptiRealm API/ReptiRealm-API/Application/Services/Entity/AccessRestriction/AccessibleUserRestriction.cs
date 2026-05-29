using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System.Linq;
using ReptiRealm_API.Domain.Entities;
using ReptiRealm_API.Domain.Entities.Common;
using ReptiRealm_API.Infrastructure.Data;
using ReptiRealm_API.Application.Interfaces.Entity.AccessRestrictions;

namespace ReptiRealm_API.Application.Services.Entity.AccessRestriction
{
    public class AccessibleUserRestriction(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor) : IAccessRestrictionMethod<User>
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public IQueryable<User> Filter(IQueryable<User> source)
        {
            return FilterByUser(source);
        }

        private IQueryable<User> FilterByUser(IQueryable<User> source)
        {
            var userName = _httpContextAccessor.HttpContext!.User.Identity!.Name!;
            var user = _context.Set<User>().SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new UnauthorizedAccessException();
            }
            return _context.Set<User>().Where(r => r.Id == user.Id);
        }
    }
}
