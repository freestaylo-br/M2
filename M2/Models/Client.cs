using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public partial class Client
{
    public int ClientId { get; set; }

    public string Surname { get; set; } = "";

    public string Name { get; set; } = "";

    public string Patronymic { get; set; } = "";

    public int RoleId { get; set; }
}