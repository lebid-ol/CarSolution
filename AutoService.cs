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

        public IReadOnlyList<Car> Cars => _cars.AsReadOnly();

        public void AddCar(Car car)
        {
            if (car == null) throw new ArgumentNullException(nameof(car));
            _cars.Add(car);
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

}
