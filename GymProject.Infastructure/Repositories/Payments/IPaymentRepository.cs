
using GymProject.Domain.Models.Payments;

namespace GymProject.Infastructure.Repositories.Payments;

public interface IPaymentRepository
{
    Task<bool> SessionExist(Guid sessionId);
    Task ApproveSession(string sessionId);
    Task CreateSession(PaymentSession session);
    Task<PaymentSession> GetSession(string sessionId);
}