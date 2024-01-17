namespace GymProject.Infastructure.Identity;

public static class RoleTypes
{
    public const string Client = nameof(Client);
    public const string Admin = nameof(Admin);

    public static List<string> GetList()
        => new() { Client, Admin };
}