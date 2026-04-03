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

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransaction(int id, [FromBody] UpdateTransactionDto dto, [FromServices] AppDbContext dbContext)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transaction = await dbContext.Transactions.FirstOrDefaultAsync(t => t.TransactionId == id && t.UserId == userId);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            var categoryExists = await dbContext.Categories.AnyAsync(c => c.CategoryId == dto.CategoryId);

            if (!categoryExists)
            {
                return BadRequest("Invalid CategoryId");
            }

            transaction.Amount = dto.Amount;
            transaction.Type = dto.Type;
            transaction.CategoryId = dto.CategoryId;

            await dbContext.SaveChangesAsync();

            return Ok("Transaction updated successfully");
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id, [FromServices] AppDbContext dbContext)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transaction = await dbContext.Transactions
                .FirstOrDefaultAsync(t => t.TransactionId == id && t.UserId == userId);

            if (transaction == null)
            {
                return NotFound("Transaction not found");
            }

            dbContext.Transactions.Remove(transaction);
            dbContext.SaveChanges();

            return Ok("Transaction deleted successfully");
        }

        [Authorize]
        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary([FromServices] AppDbContext dbContext)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            var transactions = await dbContext.Transactions.Where(t => t.UserId == userId).ToListAsync();

            var totalIncome = transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);

            var totalExpense = transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);

            var balance = totalIncome - totalExpense;

            return Ok(new 
            {
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                Balance = balance
            });
        }
    }
}
