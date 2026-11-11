using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class StatusService : DbService<Status>, IService<StatusRequest, StatusResponse>
    {
        public StatusService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Status> DbQuery()
        {
            return base.DbQuery()
                .Include(s => s.Users)
                .OrderBy(s => s.Title);
        }

        public IQueryable<StatusResponse> Query()
        {
            return DbQuery().Select(s => new StatusResponse
            {
                Id = s.Id,
                Title = s.Title,
                UserNamesR = string.Join(", ", s.Users.Select(u => u.UserName)),
                UserCountR = s.Users.Count,
                UsersR = s.Users.Select(u => new UserResponse
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    PasswordR = new string('*', u.Password.Length),
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    FullNameR = u.FirstName + " " + u.LastName,
                    RegistrationDate = u.RegistrationDate,
                    BirthDate = u.BirthDate,
                    Gender = u.Gender,
                    IsOnline = u.IsOnline,
                    Score = u.Score,
                    StatusId = u.StatusId
                }).ToList()
            });
        }

        public StatusRequest Edit(int id)
        {
            var statusEntity = DbSingle(id);
            if (statusEntity is null)
                return null;
            return new StatusRequest
            {
                Id = statusEntity.Id,
                Title = statusEntity.Title
            };
        }

        public CommandResponse Add(StatusRequest request)
        {
            if (DbQuery().Any(s => s.Title == request.Title.Trim()))
                return Error("Status with the same title exists!");
            var statusEntity = new Status
            {
                Title = request.Title.Trim()
            };
            DbAdd(statusEntity);
            return Success(statusEntity.Id, "Status created successfully.");
        }

        public CommandResponse Update(StatusRequest request)
        {
            if (DbQuery().Any(s => s.Id != request.Id && s.Title == request.Title.Trim()))
                return Error("Status with the same title exists!");
            var statusEntity = DbSingle(request.Id);
            if (statusEntity is null)
                return Error("Status not found!");
            statusEntity.Title = request.Title.Trim();
            DbUpdate(statusEntity);
            return Success(statusEntity.Id, "Status updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var statusEntity = DbSingle(id);
            if (statusEntity is null)
                return Error("Status not found!");
            if (statusEntity.Users.Any())
                return Error("Status has relational users!");
            DbRemove(statusEntity);
            return Success(statusEntity.Id, "Status deleted successfully.");
        }
    }
}
