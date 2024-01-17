using System.Text.Json.Serialization;

namespace ThePowerSPAv2.Models.Memberships;

public class MembershipModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [JsonPropertyName("price")]
    public decimal Price { get; set; }
    [JsonPropertyName("duration")]
    public int Duration { get; set; }
}