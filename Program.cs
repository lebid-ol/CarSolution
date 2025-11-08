using System;

namespace CarSolution
{
    public class Program
    {
        static void Main(string[] args)
        {
          
                Console.OutputEncoding = System.Text.Encoding.UTF8;
                Console.WriteLine("=== Демонстрация работы AutoService ===\n");

                // Создаём несколько машин
                var car1 = new Car("Toyota", "Camry", 2020, 15000, 10);
                var car2 = new Car("Honda", "Civic", 2018, 32000, 5);
                var car3 = new Car("Ford", "Focus", 2022, 5000, 0);

                // Создаём автосервис и добавляем машины
                var service = new AutoService();
                service.AddCar(car1);
                service.AddCar(car2);
                service.AddCar(car3);

                // Заправляем все автомобили
                Console.WriteLine("Заправляем все автомобили на 20 литров...");
                service.RefuelAll(20);

                // Пытаемся завести все машины
                Console.WriteLine("Пробуем завести все автомобили...");
                service.StartAll();

                // Пробуем проехать на первом авто
                Console.WriteLine("\nЕдем на Toyota 50 км...");
                car1.Drive(50);
                Console.WriteLine(car1);

                // Останавливаем все автомобили
                Console.WriteLine("\nОстанавливаем все автомобили...");
                service.StopAll();

                // Печатаем средний пробег
                Console.WriteLine($"\nСредний пробег по автопарку: {service.GetAverageMileage():N0} км");

                // Ищем машины по марке
                Console.WriteLine("\nМашины марки 'Honda':");
                foreach (var c in service.FindByMake("Honda"))
                    Console.WriteLine(c);

                // Ищем самую новую
                var newest = service.FindNewestCar();
                Console.WriteLine($"\nСамая новая машина: {newest}");

                Console.WriteLine("\n=== Программа завершена ===");
            }
        }
    }


