using Microsoft.EntityFrameworkCore;
using PaymentService.Application.Contracts;
using PaymentService.Domain.AggregateModels;
using PaymentService.Infrastructure;

namespace PaymentService.Infrastructure.Repositories;

/// <summary>
/// Implements the IPaymentRepository interface,
/// providing a concrete repository for managing PaymentTransactions using Entity Framework Core.
/// </summary>
public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    /// <summary>
    /// Initializes a new instance of the PaymentRepository class.
    /// </summary>
    /// <param name="context">The database context used for data access.</param>
    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public void AddPayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Add(payment);
    }

    public async Task<PaymentTransaction> GetPaymentByIdAsync(Guid id)
    {
        return await _context.PaymentTransactions.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void RemovePayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Remove(payment);
    }

    public async Task<List<PaymentTransaction>> GetAllPaymentTransactions()
    {
        return await _context.PaymentTransactions.ToListAsync();
    }

    public void UpdatePayment(PaymentTransaction payment)
    {
        _context.PaymentTransactions.Update(payment);
    }
}