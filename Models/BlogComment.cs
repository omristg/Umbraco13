namespace Umbraco13Test.Models;

public class BlogComment
{
	public int Id { get; set; }

	public Guid BlogPostUmbracoKey { get; set; }

	public string Name { get; set; }

	public string Email { get; set; }

	public string Website { get; set; }

	public string Message { get; set; } = string.Empty;
}
