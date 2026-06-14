using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2.Models;

public class PickupLocation
{
    public int LocationId { get; set; }

    public int Index { get; set; }

    public string City { get; set; } = "";

    public string Street { get; set; } = "";

    public string Home { get; set; } = "";

    public override string ToString()
    {
        return $"{City}, {Street}, {Home}";
    }
}
