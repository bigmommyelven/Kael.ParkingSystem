using Kael.ParkingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kael.ParkingSystem.Core.Repositories
{
    public interface IParkingAreaRepo
    {
        void Create(int slot);
        int Park(Vehicle vehicle);
        int GetParkedVehicleCount(string type);
        int[] GetSlotsByColor(string color);
        int GetSlotByRegistrationNumber(string registrationNumbers);
        int[] GetAvailableSlots();
        IEnumerable<ParkingSlot> GetUnusedParkingSlot();
        IEnumerable<ParkingSlot> GetUsedParkingSlot();

    }
}
