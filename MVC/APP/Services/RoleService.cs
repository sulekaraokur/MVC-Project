using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class RoleService : DbService<Role>, IService<RoleRequest, RoleResponse>
    {
        public RoleService(DbContext db) : base(db)
        {
        }

        public IQueryable<RoleResponse> Query()
        {
            return DbQuery().Select(roleEntity => new RoleResponse
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            });
        }

        public RoleRequest Edit(int id)
        {
            var roleEntity = DbSingle(id);
            if (roleEntity is null)
                return null;
            return new RoleRequest
            {
                Id = roleEntity.Id,
                Name = roleEntity.Name
            };
        }

        public CommandResponse Add(RoleRequest request)
        {
            if (DbQuery().Any(roleEntity => roleEntity.Name == request.Name.Trim()))
                return Error($"\"{request.Name}\" role exists!");
            var roleEntity = new Role
            {
                Name = request.Name.Trim()
            };
            DbAdd(roleEntity);
            return Success(roleEntity.Id, $"\"{roleEntity.Name}\" role created successfully.");
        }

        public CommandResponse Update(RoleRequest request)
        {
            if (DbQuery().Any(roleEntity => roleEntity.Id != request.Id && roleEntity.Name == request.Name.Trim()))
                return Error($"\"{request.Name}\" role exists!");
            var roleEntity = DbSingle(request.Id);
            if (roleEntity is null)
                return Error("Role not found!");
            roleEntity.Name = request.Name.Trim();
            DbUpdate(roleEntity);
            return Success(roleEntity.Id, $"\"{roleEntity.Name}\" role updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var roleEntity = DbSingle(id);
            if (roleEntity is null)
                return Error("Role not found!");
            // The Roles-UserRoles delete rule is Cascade, so relational UserRoles are deleted too.
            DbRemove(roleEntity);
            return Success(roleEntity.Id, $"\"{roleEntity.Name}\" role deleted successfully.");
        }
    }
}
