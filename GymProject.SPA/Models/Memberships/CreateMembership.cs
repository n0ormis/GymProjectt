namespace ThePowerSPAv2.Models.Memberships;

public class CreateMembership
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Duration { get; set; }
    public string Description { get; set; }
}