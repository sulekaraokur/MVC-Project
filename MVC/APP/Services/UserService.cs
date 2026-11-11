using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class UserService : DbService<User>, IService<UserRequest, UserResponse>
    {
        public UserService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<User> DbQuery()
        {
            return base.DbQuery()
                .Include(u => u.Status)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .OrderByDescending(u => u.Score)
                .ThenByDescending(u => u.RegistrationDate)
                .ThenBy(u => u.UserName);
        }

        public IQueryable<UserResponse> Query()
        {
            return DbQuery().Select(u => new UserResponse
            {
                Id = u.Id,
                UserName = u.UserName,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                RegistrationDate = u.RegistrationDate,
                BirthDate = u.BirthDate,
                IsOnline = u.IsOnline,
                Gender = u.Gender,
                Score = u.Score,
                StatusId = u.StatusId,
                StatusTitleR = u.Status.Title,
                StatusR = new StatusResponse
                {
                    Id = u.Status.Id,
                    Title = u.Status.Title
                },
                RoleNamesR = string.Join("<br>", u.UserRoles
                    .OrderBy(ur => ur.Role.Name)
                    .Select(ur => ur.Role.Name)),
                RolesR = u.UserRoles
                    .OrderBy(ur => ur.Role.Name)
                    .Select(ur => new RoleResponse
                    {
                        Id = ur.Role.Id,
                        Name = ur.Role.Name
                    }).ToList(),
                PasswordR = new string('*', u.Password.Length),
                FullNameR = u.FirstName + " " + u.LastName,
                RegistrationDateR = u.RegistrationDate.ToString("MM/dd/yyyy HH:mm:ss"),
                BirthDateR = u.BirthDate.HasValue ? u.BirthDate.Value.ToShortDateString() : string.Empty,
                IsOnlineR = u.IsOnline ? "Yes" : "No",
                GenderR = u.Gender.ToString(),
                ScoreR = u.Score.ToString("N1")
            });
        }

        public UserRequest Edit(int id)
        {
            var userEntity = DbSingle(id);
            if (userEntity is null)
                return null;
            return new UserRequest
            {
                Id = userEntity.Id,
                UserName = userEntity.UserName,
                Password = userEntity.Password,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                BirthDate = userEntity.BirthDate,
                Gender = userEntity.Gender,
                Score = userEntity.Score,
                StatusId = userEntity.StatusId,
                RoleIds = userEntity.RoleIds
            };
        }

        public CommandResponse Add(UserRequest request)
        {
            if (DbQuery().Any(u => u.UserName == request.UserName.Trim()))
                return Error("User with the same user name exists!");
            var userEntity = new User
            {
                UserName = request.UserName.Trim(),
                Password = request.Password.Trim(),
                FirstName = request.FirstName?.Trim(),
                LastName = request.LastName?.Trim(),
                RegistrationDate = DateTime.Now,
                BirthDate = request.BirthDate,
                Gender = request.Gender,
                Score = request.Score ?? 0,
                StatusId = request.StatusId ?? 0,
                RoleIds = request.RoleIds
            };
            DbAdd(userEntity);
            return Success(userEntity.Id, "User created successfully.");
        }

        public CommandResponse Update(UserRequest request)
        {
            if (DbQuery().Any(u => u.Id != request.Id && u.UserName == request.UserName.Trim()))
                return Error("User with the same user name exists!");
            var userEntity = DbSingle(request.Id);
            if (userEntity is null)
                return Error("User not found!");
            // Old UserRoles are deleted before the new RoleIds are assigned.
            if (request.RoleIds is not null)
                DbRemove(userEntity.UserRoles);
            userEntity.UserName = request.UserName.Trim();
            userEntity.Password = request.Password.Trim();
            userEntity.FirstName = request.FirstName?.Trim();
            userEntity.LastName = request.LastName?.Trim();
            userEntity.BirthDate = request.BirthDate;
            userEntity.Gender = request.Gender;
            if (request.Score.HasValue)
                userEntity.Score = request.Score.Value;
            if (request.StatusId.HasValue)
                userEntity.StatusId = request.StatusId.Value;
            if (request.RoleIds is not null)
                userEntity.RoleIds = request.RoleIds;
            DbUpdate(userEntity);
            return Success(userEntity.Id, "User updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var userEntity = DbSingle(id);
            if (userEntity is null)
                return Error("User not found!");
            DbRemove(userEntity.UserRoles);
            DbRemove(userEntity);
            return Success(userEntity.Id, "User deleted successfully.");
        }
    }
}
