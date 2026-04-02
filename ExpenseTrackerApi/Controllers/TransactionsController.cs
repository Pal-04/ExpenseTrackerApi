using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExpenseTrackerApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return Ok(new
            {
                Message = "Authorized user",
                UserId = userId
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto dto, [FromServices] AppDbContext _dbContext)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transaction = new Transaction
            {
                Amount = dto.Amount,
                Type = dto.Type,
                CategoryId = dto.CategoryId,
                UserId = userId,
                Date = DateTime.Now
            };

            _dbContext.Transactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            return Ok(transaction);
        }
    }
}
