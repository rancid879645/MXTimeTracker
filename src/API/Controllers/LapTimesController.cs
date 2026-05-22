using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotocrossTracker.API.Application.DTOs;
using MotocrossTracker.API.Application.Interfaces;

namespace MotocrossTracker.API.Controllers;

[ApiController]
[Route("api/laptimes")]
[Authorize]
public class LapTimesController(ILapTimeService lapTimeService) : ControllerBase
{
    private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lapTimes = await lapTimeService.GetAllByUserAsync(UserId);
        return Ok(lapTimes);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var lapTime = await lapTimeService.GetByIdAsync(id, UserId);

        if (lapTime is null)
            return NotFound();

        return Ok(lapTime);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateLapTimeRequest request)
    {
        try
        {
            var id = await lapTimeService.CreateAsync(request, UserId);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLapTimeRequest request)
    {
        try
        {
            var updated = await lapTimeService.UpdateAsync(id, request, UserId);

            if (!updated)
                return NotFound();

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await lapTimeService.DeleteAsync(id, UserId);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
