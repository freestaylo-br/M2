namespace M2.Models;

public class Staff
{
    public int IdStaff { get; set; }

    public int RoleId { get; set; }

    public string Surname { get; set; } = "";

    public string Name { get; set; } = "";

    public string Patronymic { get; set; } = "";

    public string Login { get; set; } = "";

    public string Password { get; set; } = "";
}