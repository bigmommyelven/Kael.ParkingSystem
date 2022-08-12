using Kael.ParkingSystem.Core.Entities;
using Kael.ParkingSystem.Core.Utils;
using Kael.ParkingSystem.Core.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kael.ParkingSystem.Core.Repositories
{
    public class ParkingAreaRepo : IParkingAreaRepo
    {
        public readonly ParkingArea ParkingArea;
        public ParkingAreaRepo()
        {
            ParkingArea = new ParkingArea();
        }

        public void Create(int slot)
        {
            ParkingArea.ParkingSlots = new List<ParkingSlot>(slot);
            for (int i = 0; i < slot; i++)
            {
                ParkingArea.ParkingSlots.Add(new ParkingSlot
                {
                    ID = i + 1
                });
            }
        }

        public int[] GetAvailableSlots()
        {
            if (ParkingArea.ParkingSlots == null)
                throw new Exception("Need to create parking area first!");

            if (ParkingArea.ParkingSlots.Count == 0)
                throw new Exception("Need to create parking slot first!");

            return GetUnusedParkingSlot()
                .Select(ps => ps.ID)
                .ToArray();
        }

        public IEnumerable<ParkingSlot> GetUnusedParkingSlot()
        {
            return ParkingArea.ParkingSlots
                .Where(ps => ps.Vehicle == null);
        }

        public IEnumerable<ParkingSlot> GetUsedParkingSlot()
        {
            return ParkingArea.ParkingSlots
                .Where(ps => ps.Vehicle != null);
        }

        public int GetParkedVehicleCount(string type)
        {
            return GetUsedParkingSlot()
                .Where(ps => ps.Vehicle.Type == type)
                .Count();
        }

        public int[] GetSlotsByColor(string color)
        {
            return GetUsedParkingSlot()
                .Where(ps => ps.Vehicle.Color == color)
                .Select(ps => ps.ID)
                .ToArray();
        }

        public string[] GetRegistrationNumbersByColor(string color)
        {
            return GetUsedParkingSlot()
                .Where(ps => ps.Vehicle.Color == color)
                .Select(ps => ps.Vehicle.RegistrationNumber)
                .ToArray();
        }

        public int GetSlotByRegistrationNumber(string registrationNumbers)
        {
            VehicleValidators.ValidatePlate(registrationNumbers);
            return GetUsedParkingSlot()
                .Where(ps => ps.Vehicle.RegistrationNumber == registrationNumbers)
                .Select(ps => ps.ID)
                .FirstOrDefault();
        }

        public int Park(Vehicle vehicle)
        {
            VehicleValidators.ValidateVehicle(vehicle);
            var availableSlot = GetUnusedParkingSlot().FirstOrDefault();

            if (availableSlot == null)
                throw new Exception("Sorry, parking lot is full!");

            availableSlot.Vehicle = vehicle;
            return availableSlot.ID;
        }

        public int Leave(int slotNumber)
        {
            var usedSlots = GetUsedParkingSlot()
                .Where(ps => ps.ID == slotNumber)
                .FirstOrDefault();

            if (usedSlots == null)
                throw new ArgumentException($"There is no slot with number = {slotNumber}");

            usedSlots.Vehicle = null;
            return usedSlots.ID;
        }

        public string[] GetOddPlates()
        {
            return GetUsedParkingSlot()
                .Where(ps => VehicleUtil.IsOddPlate(ps.Vehicle.RegistrationNumber))
                .Select(ps => ps.Vehicle.RegistrationNumber)
                .ToArray();
        }

        public string[] GetEvenPlates()
        {
            return GetUsedParkingSlot()
                .Where(ps => VehicleUtil.IsEvenPlate(ps.Vehicle.RegistrationNumber))
                .Select(ps => ps.Vehicle.RegistrationNumber)
                .ToArray();
        }
    }
}
