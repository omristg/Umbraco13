using Umbraco13Test.Models;

namespace Umbraco13Test.Repositories;
public interface IBlogCommentRepository
{
	Task<IEnumerable<BlogComment>> GetAllAsync();
	Task<BlogComment?> GetByIdAsync(int id);
	Task<IEnumerable<BlogComment>> GetByUmbracoNodeKeyAsync(Guid umbracoNodeKey);
	Task<BlogComment> CreateAsync();
}