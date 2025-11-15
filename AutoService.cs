using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarSolution
{
    public class AutoService
    {
        private readonly List<Car> _cars = new();
        private readonly ILoggerService _logger;
        private readonly INotificationService _notification;
        private readonly ICarPricingService _pricing;

        public AutoService(ILoggerService logger, INotificationService notification, ICarPricingService pricing)
        {
            _logger = logger;
            _notification = notification;
            _pricing = pricing;
        }

        public IReadOnlyList<Car> Cars => _cars.AsReadOnly();

        public void AddCar(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));

            _cars.Add(car);
            _logger.Log($"Car added: {car.Make} {car.Model}");
        }

        public void PerformMaintenance(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));

            car.Refuel(10);
            car.Stop();
            _notification.NotifyOwner(car.Make, $"Maintenance completed for {car.Make} {car.Model}");
            _logger.Log($"Maintenance done for {car.Make}");
        }

        public double CalculateFleetValue()
        {
            double total = 0;
            foreach (var car in _cars)
                total += _pricing.GetEstimatedValue(car);

            _logger.Log($"Calculated total fleet value: {total}");
            return total;
        }

        public Car? FindMostValuableCar()
        {
            if (!_cars.Any()) return null;

            return _cars
                .OrderByDescending(c => _pricing.GetEstimatedValue(c))
                .FirstOrDefault();
        }

        public void RemoveCar(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));
            _cars.Remove(car);
        }

        public void RefuelAll(double liters)
        {
            foreach (var car in _cars)
                car.Refuel(liters);
        }

        public void StartAll()
        {
            foreach (var car in _cars)
            {
                try { car.Start(); }
                catch { /* Игнорируем ошибки старта */ }
            }
        }

        public void StopAll()
        {
            foreach (var car in _cars)
                car.Stop();
        }

        public double GetAverageMileage() =>
            _cars.Count == 0 ? 0 : _cars.Average(c => c.Mileage);

        public IEnumerable<Car> FindByMake(string make) =>
            _cars.Where(c => c.Make.Equals(make, StringComparison.OrdinalIgnoreCase));

        public Car? FindNewestCar() =>
            _cars.OrderByDescending(c => c.Year).FirstOrDefault();
    }
        public interface ILoggerService
        {
            void Log(string message);
        }

        public interface INotificationService
        {
            void NotifyOwner(string carMake, string message);
        }


        public interface ICarPricingService
        {
            double GetEstimatedValue(Car car);
        }
    }



