using ExpenseTrackerApi.Data;
using ExpenseTrackerApi.DTOs;
using ExpenseTrackerApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<IActionResult> AddTransaction([FromBody] CreateTransactionDto dto, [FromServices] AppDbContext dbContext)
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

            dbContext.Transactions.Add(transaction);
            await dbContext.SaveChangesAsync();

            return Ok(transaction);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromServices] AppDbContext dbContext)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transactions = await dbContext.Transactions
                .Where(t => t.UserId == userId)
                .Include(t => t.Category)
                .Select(t => new TransactionResponseDto
                { 
                    TransactionId = t.TransactionId,
                    Amount = t.Amount,
                    Type = t.Type,
                    CategoryName = t.Category!.Name,
                    Date = t.Date
                })
                .ToListAsync();

            return Ok(transactions);
        }
    }
}
