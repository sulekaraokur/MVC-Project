using APP.Domain;
using APP.Models;
using CORE.Models;
using CORE.Services;
using Microsoft.EntityFrameworkCore;

namespace APP.Services
{
    public class BlogService : DbService<Blog>, IService<BlogRequest, BlogResponse>
    {
        public BlogService(DbContext db) : base(db)
        {
        }

        protected override IQueryable<Blog> DbQuery()
        {
            return base.DbQuery()
                .Include(b => b.User)
                .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
                .OrderByDescending(b => b.PublishDate)
                .ThenBy(b => b.Title);
        }

        public IQueryable<BlogResponse> Query()
        {
            return DbQuery().Select(b => new BlogResponse
            {
                Id = b.Id,
                Title = b.Title,
                Content = b.Content,
                Rating = b.Rating,
                PublishDate = b.PublishDate,
                IsArchived = b.IsArchived,
                UserName = b.UserName,
                UserId = b.UserId,
                UserR = b.User.UserName,
                TagNamesR = string.Join("<br>", b.BlogTags
                    .OrderBy(bt => bt.Tag.Name)
                    .Select(bt => bt.Tag.Name)),
                TagsR = b.BlogTags
                    .OrderBy(bt => bt.Tag.Name)
                    .Select(bt => new TagResponse
                    {
                        Id = bt.Tag.Id,
                        Name = bt.Tag.Name
                    }).ToList(),
                RatingR = b.Rating.HasValue ? b.Rating.Value.ToString("N1") : string.Empty,
                PublishDateR = b.PublishDate.ToString("MM/dd/yyyy"),
                IsArchivedR = b.IsArchived ? "Yes" : "No"
            });
        }

        public BlogRequest Edit(int id)
        {
            var blogEntity = DbSingle(id);
            if (blogEntity is null)
                return null;
            return new BlogRequest
            {
                Id = blogEntity.Id,
                Title = blogEntity.Title,
                Content = blogEntity.Content,
                Rating = blogEntity.Rating,
                PublishDate = blogEntity.PublishDate,
                IsArchived = blogEntity.IsArchived,
                UserName = blogEntity.UserName,
                UserId = blogEntity.UserId,
                TagIds = blogEntity.TagIds
            };
        }

        public CommandResponse Add(BlogRequest request)
        {
            if (DbQuery().Any(b => b.Title == request.Title.Trim()))
                return Error("Blog with the same title exists!");
            var blogEntity = new Blog
            {
                Title = request.Title.Trim(),
                Content = request.Content.Trim(),
                Rating = request.Rating,
                PublishDate = request.PublishDate ?? DateTime.Now,
                IsArchived = request.IsArchived,
                UserName = request.UserName?.Trim(),
                UserId = request.UserId
            };
            if (request.TagIds is not null)
                blogEntity.TagIds = request.TagIds;
            DbAdd(blogEntity);
            return Success(blogEntity.Id, "Blog created successfully.");
        }

        public CommandResponse Update(BlogRequest request)
        {
            if (DbQuery().Any(b => b.Id != request.Id && b.Title == request.Title.Trim()))
                return Error("Blog with the same title exists!");
            var blogEntity = DbSingle(request.Id);
            if (blogEntity is null)
                return Error("Blog not found!");
            // Old BlogTags are deleted before the new TagIds are assigned.
            if (request.TagIds is not null)
                DbRemove(blogEntity.BlogTags);
            blogEntity.Title = request.Title.Trim();
            blogEntity.Content = request.Content.Trim();
            blogEntity.Rating = request.Rating;
            if (request.PublishDate.HasValue)
                blogEntity.PublishDate = request.PublishDate.Value;
            blogEntity.IsArchived = request.IsArchived;
            blogEntity.UserName = request.UserName?.Trim();
            blogEntity.UserId = request.UserId;
            if (request.TagIds is not null)
                blogEntity.TagIds = request.TagIds;
            DbUpdate(blogEntity);
            return Success(blogEntity.Id, "Blog updated successfully.");
        }

        public CommandResponse Remove(int id)
        {
            var blogEntity = DbSingle(id);
            if (blogEntity is null)
                return Error("Blog not found!");
            DbRemove(blogEntity.BlogTags);
            DbRemove(blogEntity);
            return Success(blogEntity.Id, "Blog deleted successfully.");
        }
    }
}
