using Microsoft.AspNetCore.Mvc;
using TicTacToe.Api.Models;
using TicTacToe.Api.Services;

namespace TicTacToe.Api.Controllers;
public class StartGameRequest
{
    public int size { get; set; }
}

[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private GameService _gameService;
    public GameController( GameService gameService)
    {
        _gameService = gameService;
    }

    [HttpGet("health")]
    public IActionResult Health()
    {
        return Ok("Api is alive");
    }

    [HttpGet("{id}")]
    public IActionResult GameStatus(Guid id)
    {

        var game = _gameService.GetGame(id);
        if (game == null) return BadRequest("Game not found");
        return Ok(game); 
    }

    [HttpPost("start")]
    public IActionResult StartGame([FromBody] StartGameRequest request)
    {
        if (request.size < 3) return BadRequest("Select a value of N >= 3");
        var game = _gameService.CreateGame(request.size);
        return Ok(game);
    }

    [HttpPost("{id}/move")]
    public IActionResult MakeMove(Guid id, [FromBody] Move move)
    {
        var game = _gameService.GetGame(id);

        if (game == null) return NotFound();
        if (game.IsFinished) return Conflict("Game is already finished");
        if (!string.IsNullOrEmpty(game.Board[move.Row][move.Column])) return BadRequest("Cell already occupied");

        var  updatedGame = _gameService.MakeMove(id, move);
        if (updatedGame == null) return BadRequest("Move failed");

       
        return Ok(updatedGame);
    }
}
