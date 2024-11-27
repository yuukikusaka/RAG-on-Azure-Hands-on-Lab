using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Azure.Search.Documents;

using Simple.Services;

[ApiController]
[Route("[controller]")]
public class SearchController : ControllerBase
{
    private readonly AoaiService _aoaiService;
    private readonly AiSearchService _aiSearchService;

    public SearchController(AoaiService aoaiService, AiSearchService aiSearchService)
    {
        _aoaiService = aoaiService ?? throw new ArgumentNullException(nameof(aoaiService));
        _aiSearchService = aiSearchService ?? throw new ArgumentNullException(nameof(aiSearchService));
    }

    [HttpGet("fulltext")]
    public async Task<IActionResult> FullTextSearch(string query)
    {
        try
        {
            var rewritten_query = await _aoaiService.RewriteQueryAsync(query);
            var results = _aiSearchService.FullTextSearch(rewritten_query);
            var response = await _aoaiService.GenerateAnswerAsync(query, results);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}