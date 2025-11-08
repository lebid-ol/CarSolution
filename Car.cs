using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSolution
{
    public class Car
    {
        public string Make { get; }
        public string Model { get; }
        public int Year { get; }
        public double Mileage { get; private set; }
        public double FuelLevel { get; private set; } // в литрах
        public bool IsRunning { get; private set; }

        private const double MaxFuelCapacity = 50.0;

        public Car(string make, string model, int year, double mileage = 0, double fuelLevel = 0)
        {
            if (string.IsNullOrWhiteSpace(make)) throw new ArgumentException("Make cannot be empty");
            if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model cannot be empty");
            if (year < 1886 || year > DateTime.Now.Year + 1) throw new ArgumentOutOfRangeException(nameof(year), "Invalid year");

            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage >= 0 ? mileage : throw new ArgumentOutOfRangeException(nameof(mileage));
            FuelLevel = fuelLevel >= 0 ? fuelLevel : throw new ArgumentOutOfRangeException(nameof(fuelLevel));
        }

        public void Start()
        {
            if (FuelLevel <= 0)
                throw new InvalidOperationException("Cannot start car — fuel tank is empty.");
            IsRunning = true;
        }

        public void Stop() => IsRunning = false;

        public void Drive(double distance)
        {
            if (!IsRunning)
                throw new InvalidOperationException("Cannot drive — the car is not started.");

            if (distance <= 0)
                throw new ArgumentOutOfRangeException(nameof(distance), "Distance must be positive.");

            double fuelNeeded = distance * 0.1; // 10 км на 1 литр
            if (fuelNeeded > FuelLevel)
                throw new InvalidOperationException("Not enough fuel for the trip.");

            FuelLevel -= fuelNeeded;
            Mileage += distance;
        }

        public void Refuel(double liters)
        {
            if (liters <= 0)
                throw new ArgumentOutOfRangeException(nameof(liters), "Fuel amount must be positive.");

            if (FuelLevel + liters > MaxFuelCapacity)
                FuelLevel = MaxFuelCapacity;
            else
                FuelLevel += liters;
        }

        public override string ToString()
            => $"{Year} {Make} {Model} - {Mileage:N0} km, Fuel: {FuelLevel:F1}L, Running: {IsRunning}";
    }

}
