using System.ComponentModel.DataAnnotations;

namespace GymProject.Domain.Models.Payments;

public class PaymentSession
{
    [Key]
    public Guid Id { get; set; }
    public string SessionUrl { get; set; } = String.Empty;
    public Guid UserId { get; set; }
    public Guid ProductId { get; set; }
    public bool Approved { get; set; } = false;
}