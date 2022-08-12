using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kael.ParkingSystem.Core.Utils
{
    public class VehicleUtil
    {
        public static bool IsOddPlate(string plate)
        {
            var arr = plate.Split("-");
            var plateNUmber = int.Parse(arr[1]);

            return plateNUmber % 2 == 1;
        }

        public static bool IsEvenPlate(string plate)
        {
            var arr = plate.Split("-");
            var plateNumber = int.Parse(arr[1]);

            return plateNumber % 2 == 0;
        }
    }
}
