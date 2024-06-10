using Microsoft.AspNetCore.Mvc;
using Umbraco13Test.Repositories;

namespace Umbraco13Test.Controllers;

[ApiController]
[Route("api/[controller]")]
[EndpointGroupName("BlogComments")]
public class BlogCommentsController : ControllerBase
{
    private readonly IBlogCommentRepository _blogCommentRepository;

    public BlogCommentsController(IBlogCommentRepository blogCommentRepository)
    {
        _blogCommentRepository = blogCommentRepository;
    }

    [HttpGet("All")]
    public async Task<IActionResult> All()
    {
        var comments = await _blogCommentRepository.GetAllAsync();
        return Ok(comments);
    }

    [HttpGet("GetComments")]
    public async Task<IActionResult> GetComments(Guid umbracoNodeKey)
    {
        var comments = await _blogCommentRepository.GetByUmbracoNodeKeyAsync(umbracoNodeKey);
        return Ok(comments);
    }

    [HttpGet("GetById")]
    public async Task<IActionResult> GetById([FromQuery] int id)
    {
        var comments = await _blogCommentRepository.GetByIdAsync(id);
        return Ok(comments);
    }

    [HttpPost("InsertComment")]
    public async Task<IActionResult> InsertComment()
    {
        var comment = await _blogCommentRepository.CreateAsync();
        return CreatedAtAction(nameof(GetById), new { id = comment.Id }, comment);
    }
}

