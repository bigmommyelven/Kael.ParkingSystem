using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kael.ParkingSystem.Core.Entities
{
    public class ParkingSlot
    {
        public int ID { get; set; }
        public Vehicle Vehicle { get; set; }
    }
}
