using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M2.Models;

public class Order
{
    public int OrderId { get; set; }

    public string Article { get; set; } = "";

    public DateTime OrderDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public int StatusId { get; set; }

    public string StatusName { get; set; } = "";

    public int LocationId { get; set; }

    public string PickupLocation { get; set; } = "";
}