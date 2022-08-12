using Kael.ParkingSystem.Core.Entities;
using Kael.ParkingSystem.Core.Repositories;
using Kael.ParkingSystem.Core.Validators;

namespace Kael.ParkingSystem.Console
{
    class Program
    {
        private static bool keepRunning = true;
        private static readonly ParkingAreaRepo repo = new();
        static void Main()
        {
            System.Console.CancelKeyPress += delegate
            {
                keepRunning = false;
            };

            while(keepRunning)
            {
                System.Console.Write("Command: ");
                var input = System.Console.ReadLine();
                var inputArgs = input.ToUpper().Split(" ");
                try
                {
                    switch(inputArgs[0])
                    {
                        case "CREATE_PARKING_LOT":
                            if (inputArgs.Length < 2)
                                throw new System.ArgumentException("Invalid command args!");

                            var numOfSlots = int.Parse(inputArgs[1]);
                            repo.Create(numOfSlots);
                            System.Console.WriteLine($"Created a parking lot with {numOfSlots}");
                            break;

                        case "PARK":
                            if (inputArgs.Length != 4)
                                throw new System.ArgumentException("Invalid command args!");

                            var parkedSlot = repo.Park(new Vehicle
                            {
                                RegistrationNumber = inputArgs[1],
                                Color = inputArgs[2],
                                Type = inputArgs[3]
                            });

                            System.Console.WriteLine($"Allocated slot number: {parkedSlot}");

                            break;

                        case "LEAVE":
                            System.Console.WriteLine($"Slot number {repo.Leave(int.Parse(inputArgs[1]))} is free!");
                            break;

                        case "STATUS":
                            System.Console.WriteLine("{0,-4}{1,-12}{2,-6}{3,-10}", "Slot", "No.", "Type", "Color");
                            foreach(var slot in repo.GetUsedParkingSlot())
                            {
                                System.Console.WriteLine(
                                    "{0,-4}{1,-12}{2,-6}{3,-10}",
                                    slot.ID,
                                    slot.Vehicle.RegistrationNumber,
                                    slot.Vehicle.Type,
                                    slot.Vehicle.Color);
                            }
                            break;

                        case "TYPE_OF_VEHICLES":
                            if (inputArgs.Length != 2)
                                throw new System.ArgumentException("Invalid command args!");

                            System.Console.WriteLine(repo.GetParkedVehicleCount(inputArgs[1]));
                            break;

                        case "REGISTRATION_NUMBERS_FOR_VEHICLES_WITH_COLOUR":
                            System.Console.WriteLine(
                                string.Join(", ", repo.GetRegistrationNumbersByColor(inputArgs[1])));
                            break;

                        case "SLOT_NUMBERS_FOR_VEHICLES_WITH_COLOUR":
                            if (inputArgs.Length != 2)
                                throw new System.ArgumentException("Invalid command args!");

                            System.Console.WriteLine(
                                string.Join(", ", repo.GetSlotsByColor(inputArgs[1])));
                            break;

                        case "SLOT_NUMBER_FOR_REGISTRATION_NUMBER":
                            if (inputArgs.Length != 2)
                                throw new System.ArgumentException("Invalid command args!");

                            var usedSlot = repo.GetSlotByRegistrationNumber(inputArgs[1]);
                            if (usedSlot > 0)
                            {
                                System.Console.WriteLine(usedSlot);
                            }
                            else
                            {
                                System.Console.WriteLine("Not Found!");
                            }
                            break;

                        case "REGISTRATION_NUMBERS_FOR_VEHICLES_WITH_ODD_PLATE":
                            System.Console.WriteLine(string.Join(", ", repo.GetOddPlates()));
                            break;

                        case "REGISTRATION_NUMBERS_FOR_VEHICLES_WITH_EVEN_PLATE":
                            System.Console.WriteLine(string.Join(", ", repo.GetEvenPlates()));
                            break;

                        case "EXIT":
                            System.Environment.Exit(0);
                            break;

                        default:
                            System.Console.WriteLine("Invalid command!");
                            break;
                    }
                }
                catch (System.Exception ex)
                {
                    System.Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
