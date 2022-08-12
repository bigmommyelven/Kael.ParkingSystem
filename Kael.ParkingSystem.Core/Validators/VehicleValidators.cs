using Kael.ParkingSystem.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kael.ParkingSystem.Core.Validators
{
    public class VehicleValidators
    {
        public static string[] AllowedTypes =
        {
            "MOBIL",
            "MOTOR"
        };

        public static void ValidateVehicle(Vehicle vehicle)
        {
            ValidatePlate(vehicle.RegistrationNumber);
            ValidateColor(vehicle.Color);
            ValidateType(vehicle.Type);
        }

        public static void ValidateType(string type)
        {
            if (type == null)
                throw new NullReferenceException($"{nameof(Vehicle.Type)} cannot be null!");

            type = type.ToUpper();
            if (!AllowedTypes.Contains(type))
                throw new ArgumentException($"Invalid vehicle type!");
        }

        public static void ValidateColor(string color)
        {
            if (color == null)
                throw new NullReferenceException($"{nameof(Vehicle.Color)} cannot be null!");
        }

        public static void ValidatePlate(string plate)
        {
            if (plate == null)
                throw new NullReferenceException($"{nameof(Vehicle.RegistrationNumber)} cannot be null!");

            var registPattern = @"[a-zA-Z]+\-\d{1,4}\-[a-zA-Z]{1,3}";
            Match m = Regex.Match(plate, registPattern);
            if (!m.Success)
                throw new ArgumentException("Invalid registration number!");
        }
    }
}
