﻿using System;
using WeatherStationProject.Dashboard.AirParametersService.Data;
using WeatherStationProject.Dashboard.AmbientTemperatureService.Data;
using WeatherStationProject.Dashboard.GroundTemperatureService.Data;
using WeatherStationProject.Dashboard.RainfallService.Data;
using WeatherStationProject.Dashboard.WindMeasurementsService.Data;

namespace WeatherStationProject.Dashboard.DatabaseFeeder
{
    internal static class Program
    {
        private const int MinutesBetweenMeasurements = 5;
        private const int StoreInformationEachNumber = 10000;

        private static readonly string[] WindDirections =
        {
            "N", "N-NE", "N-E", "E-NE", "E", "E-SE", "S-E", "S-SE", "S", "S-SW", "S-W", "W-SW", "W", "W-NW", "N-W",
            "N-NW"
        };

        private static readonly Random Random = new();

        private static void Main()
        {
            Console.WriteLine("Starting test data population!");

            InsertTestData();

            Console.WriteLine("Done!");
        }

        private static void InsertTestData()
        {
            var airParametersDbContext = new AirParametersDbContext();
            var ambientTemperatureDbContext = new AmbientTemperatureDbContext();
            var groundTemperatureDbContext = new GroundTemperatureDbContext();
            var rainfallDbContext = new RainfallDbContext();
            var windMeasurementsDbContext = new WindMeasurementsDbContext();

            var initialDatetime = new DateTime(2018, 1, 1, 0, 0, 0, DateTimeKind.Local);
            var finalDatetime = initialDatetime.AddYears(2);

            var i = 0;

            do
            {
                InsertAirParametersData(airParametersDbContext, initialDatetime);
                InsertAmbientTemperatureData(ambientTemperatureDbContext, initialDatetime);
                InsertGroundTemperatureData(groundTemperatureDbContext, initialDatetime);
                InsertRainfallData(rainfallDbContext, initialDatetime);
                InsertWindMeasurementsData(windMeasurementsDbContext, initialDatetime);

                i++;
                if (i == StoreInformationEachNumber)
                {
                    Console.WriteLine("Saving changes");
                    Console.WriteLine();

                    i = 0;
                    airParametersDbContext.SaveChanges();
                    ambientTemperatureDbContext.SaveChanges();
                    groundTemperatureDbContext.SaveChanges();
                    rainfallDbContext.SaveChanges();
                    windMeasurementsDbContext.SaveChanges();
                }

                initialDatetime = initialDatetime.AddMinutes(MinutesBetweenMeasurements);
                Console.WriteLine();
            } while (initialDatetime <= finalDatetime);

            airParametersDbContext.SaveChanges();
            ambientTemperatureDbContext.SaveChanges();
            groundTemperatureDbContext.SaveChanges();
            rainfallDbContext.SaveChanges();
            windMeasurementsDbContext.SaveChanges();
        }

        private static void InsertAirParametersData(AirParametersDbContext ctx, DateTime date)
        {
            var entity = new AirParameters
            {
                Humidity = GetRandomDecimal(5.0, 95.5),
                Pressure = GetRandomDecimal(900.0, 1050.0),
                DateTime = date
            };

            ctx.Add(entity);

            Console.WriteLine(
                $"AirParameters: Humidity {entity.Humidity} Pressure {entity.Pressure} Datetime {entity.DateTime}");
        }

        private static void InsertAmbientTemperatureData(AmbientTemperatureDbContext ctx, DateTime date)
        {
            var entity = new AmbientTemperature
            {
                Temperature = GetRandomDecimal(-10.0, 40.5),
                DateTime = date
            };

            ctx.Add(entity);

            Console.WriteLine($"AmbientTemperature: Temperature {entity.Temperature} Datetime {entity.DateTime}");
        }

        private static void InsertGroundTemperatureData(GroundTemperatureDbContext ctx, DateTime date)
        {
            var entity = new GroundTemperature
            {
                Temperature = GetRandomDecimal(-10.0, 40.5),
                DateTime = date
            };

            ctx.Add(entity);

            Console.WriteLine($"GroundTemperature: Temperature {entity.Temperature} Datetime {entity.DateTime}");
        }

        private static void InsertRainfallData(RainfallDbContext ctx, DateTime date)
        {
            var entity = new Rainfall
            {
                Amount = GetRandomDecimal(0.5, 100.0),
                DateTime = date
            };

            ctx.Add(entity);

            Console.WriteLine($"Rainfall: Amount {entity.Amount} Datetime {entity.DateTime}");
        }

        private static void InsertWindMeasurementsData(WindMeasurementsDbContext ctx, DateTime date)
        {
            var entity = new WindMeasurements
            {
                Speed = GetRandomDecimal(0, 100.0),
                Direction = WindDirections[Random.Next(0, WindDirections.Length)],
                DateTime = date
            };

            ctx.Add(entity);

            Console.WriteLine(
                $"WindMeasurements: Speed {entity.Speed} Direction {entity.Direction} Datetime {entity.DateTime}");
        }

        private static decimal GetRandomDecimal(double minValue, double maxValue)
        {
            var randNumber = Random.NextDouble() * (maxValue - minValue) + minValue;
            return Convert.ToDecimal(randNumber.ToString("f2"));
        }
    }
}