using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Simple.Services;

[ApiController]
[Route("[controller]")]
public class ChatController : ControllerBase
{
    private readonly AoaiService _aoaiService;

    public ChatController(AoaiService aoaiService)
    {
        _aoaiService = aoaiService ?? throw new ArgumentNullException(nameof(aoaiService));
    }

    [HttpGet]
    public async Task<IActionResult> Chat(string query)
    {
        try
        {
            var response = await _aoaiService.GetChatResponseAsync(query);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}