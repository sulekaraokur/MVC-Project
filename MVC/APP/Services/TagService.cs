using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class TagService : DbService<Tag>, IService<TagRequest, TagResponse>
    {
        public TagService(DbContext db) : base(db)
        {
        }

        public IQueryable<TagResponse> Query()
        {
            return DbQuery().Select(tagEntity => new TagResponse
            {
                Id = tagEntity.Id,
                Name = tagEntity.Name
            });
        }

        public TagRequest Edit(int id)
        {
            var tagEntity = DbSingle(id);
            if (tagEntity is null)
                return null;
            return new TagRequest
            {
                Id = tagEntity.Id,
                Name = tagEntity.Name
            };
        }

        public CommandResponse Add(TagRequest request)
        {
            if (DbQuery().Any(tagEntity => tagEntity.Name == request.Name.Trim()))
                return Error("\"" + request.Name + "\"" + " tag exists!");
            var tagEntity = new Tag
            {
                Name = request.Name.Trim()
            };
            DbAdd(tagEntity);
            return Success(tagEntity.Id, "\"" + tagEntity.Name + "\"" +
                " tag created successfully.");
        }

        public CommandResponse Update(TagRequest request)
        {
            if (DbQuery().Any(tagEntity => tagEntity.Id != request.Id && tagEntity.Name == request.Name.Trim()))
                return Error($"\"{request.Name}\" tag exists!");
            var tagEntity = DbSingle(request.Id);
            if (tagEntity is null)
                return Error("Tag not found!");
            tagEntity.Name = request.Name.Trim();
            DbUpdate(tagEntity);
            return Success(tagEntity.Id, $"\"{tagEntity.Name}\" tag updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var tagEntity = DbSingle(id);
            if (tagEntity is null)
                return Error("Tag not found!");
            // The Tags-BlogTags delete rule is Cascade, so relational BlogTags are deleted too.
            DbRemove(tagEntity);
            return Success(tagEntity.Id, $"\"{tagEntity.Name}\" tag deleted successfully.");
        }
    }
}
