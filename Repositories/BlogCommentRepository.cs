using Microsoft.EntityFrameworkCore;
using Umbraco.Cms.Persistence.EFCore.Scoping;
using Umbraco13Test.Data;
using Umbraco13Test.Models;

namespace Umbraco13Test.Repositories;


public class BlogCommentRepository : IBlogCommentRepository
{

	private readonly IEFCoreScopeProvider<MyDbContext> _efCoreScopeProvider;

	public BlogCommentRepository(IEFCoreScopeProvider<MyDbContext> efCoreScopeProvider)
	{
		_efCoreScopeProvider = efCoreScopeProvider;
	}


	public async Task<IEnumerable<BlogComment>> GetAllAsync()
	{
		using var scope = _efCoreScopeProvider.CreateScope();
		var comments = await scope.ExecuteWithContextAsync(async db =>
			await db.BlogComments.ToListAsync());
		scope.Complete();
		return comments;
	}

	public async Task<IEnumerable<BlogComment>> GetByUmbracoNodeKeyAsync(Guid umbracoNodeKey)
	{
		using var scope = _efCoreScopeProvider.CreateScope();
		var comments = await scope.ExecuteWithContextAsync(async db =>
		{
			return await db.BlogComments.Where(x => x.BlogPostUmbracoKey == umbracoNodeKey).ToListAsync();
		});

		scope.Complete();
		return comments;
	}

	public async Task<BlogComment?> GetByIdAsync(int id)
	{
		using var scope = _efCoreScopeProvider.CreateScope();
		var comment = await scope.ExecuteWithContextAsync(async db =>
			await db.FindAsync<BlogComment>(id));
		scope.Complete();

		return comment;
	}

	public async Task<BlogComment> CreateAsync()
	{
		using var scope = _efCoreScopeProvider.CreateScope();

		var comment = new BlogComment()
		{
			Email = "omristg@gmail.com",
			Name = "Omri",
			BlogPostUmbracoKey = Guid.Parse("DF284EAF-7E74-4365-BEAB-E7A06ABB57D8"),
			Message = "Test test test",
			Website = "https://google.com/"
		};

		await scope.ExecuteWithContextAsync<Task>(async db =>
		{
			db.BlogComments.Add(comment);
			await db.SaveChangesAsync();
		});

		scope.Complete();
		return comment;
	}
}

