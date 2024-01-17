using GymProject.Domain.Models.Payments;
using GymProject.Infastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace GymProject.Infastructure.Repositories.Payments;

internal sealed class PaymentRepository : IPaymentRepository
{
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PaymentSession> GetSession(string sessionId)
        => await _dbContext.PaymentSessions.FirstOrDefaultAsync(prp => prp.SessionUrl.Equals(sessionId));
    
    public async Task<bool> SessionExist(Guid sessionId)
    {
        var session = await _dbContext.PaymentSessions.FirstOrDefaultAsync(prp => prp.Id.Equals(sessionId));
        return session is null ? false : session.Approved;
    }

    public async Task ApproveSession(string sessionId)
    {
        var paymentSession = await _dbContext.PaymentSessions.FirstOrDefaultAsync(prp => prp.SessionUrl.Equals(sessionId)); //throw if null
        paymentSession.Approved = true;
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateSession(PaymentSession session)
    {
        await _dbContext.PaymentSessions.AddAsync(session);
        await _dbContext.SaveChangesAsync();
    }
}