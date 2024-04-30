using Microsoft.AspNetCore.Mvc;
using Umbraco.Cms.Web.Common.Controllers;
using Umbraco13Test.Repositories;

namespace Umbraco13Test.Controllers;

public class BlogCommentsController : UmbracoApiController
{
	private readonly IBlogCommentRepository _blogCommentRepository;

	public BlogCommentsController(IBlogCommentRepository blogCommentRepository)
	{
		_blogCommentRepository = blogCommentRepository;
	}

	[HttpGet]
	public async Task<IActionResult> All()
	{
		var comments = await _blogCommentRepository.GetAllAsync();
		return Ok(comments);
	}

	[HttpGet]
	public async Task<IActionResult> GetComments(Guid umbracoNodeKey)
	{
		var comments = await _blogCommentRepository.GetByUmbracoNodeKeyAsync(umbracoNodeKey);
		return Ok(comments);
	}

	[HttpGet]
	public async Task<IActionResult> GetById([FromQuery] int id)
	{
		var comments = await _blogCommentRepository.GetByIdAsync(id);
		return Ok(comments);
	}

	[HttpPost]
	public async Task<IActionResult> InsertComment()
	{
		var comment = await _blogCommentRepository.CreateAsync();
		return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
	}
}

