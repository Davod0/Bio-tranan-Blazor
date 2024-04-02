namespace Cinema.WebApi;

using Microsoft.AspNetCore.Mvc;
using Cinema.Core;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("movie")]
public class MovieController : ControllerBase
{
    private readonly MovieService _service;
    public MovieController(MovieService movieService)
    {
        _service = movieService;
    }

    [HttpPost("")]
    [Authorize]
    public async Task<IActionResult> PostMovieAsync(Movie m)
    {
        if (await _service.AddMovieAsync(m) != null)
        {
            try
            {
                return Created("/movie", $"{m}");
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        return BadRequest();
    }

    [HttpGet("")]
    public async Task<IActionResult> GetAllMoviesAsync()
    {
        if (await _service.GetAllMoviesAsync() != null)
        {
            try
            {
                return Ok(await _service.GetAllMoviesAsync());
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovieAsync(int id)
    {
        if (await _service.DeleteMovieAsync(id) != null)
        {
            return Ok();
        }
        return BadRequest();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        Movie m = await _service.GetMovieByIdAsync(id);
        if (m != null)
        {
            return Ok(m);
        }
        return BadRequest();
    }
}
