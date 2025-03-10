using System;
using System.Collections.Generic;
using System.Linq;
using SpaceSimulator;

class Astronomy
{
    static void Main()
    {
        var (planets, stars, moons) = ObjectInfo.GetSolarSystem();

        List<SpaceObject> solarSystem = new List<SpaceObject>();
        solarSystem.AddRange(stars);
        solarSystem.AddRange(planets);
        solarSystem.AddRange(moons);

        // Get the number of days since time 0
        Console.WriteLine("Enter the number of days since time 0:");
        if (!int.TryParse(Console.ReadLine(), out int days))
        {
            Console.WriteLine("Invalid input, defaulting to 0 days");
            days = 0;
        }

        // Get object name
        Console.WriteLine("Enter name of object (leave empty for Sun & planets):");
        string objectName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(objectName))
        {
            // Default to showing Sun + all planets (no moons)
            Console.WriteLine("\n--- Solar System Positions ---");
            foreach (var obj in solarSystem.Where(o => o is Star || o is Planet))
            {
                ShowObjectInfo(obj, days);
            }
        }
        else
        {
            var selectedObject = solarSystem.FirstOrDefault(o => o.Name.Equals(objectName, StringComparison.OrdinalIgnoreCase));

            if (selectedObject != null)
            {
                if (selectedObject is Planet selectedPlanet)
                {
                    // Show the planet
                    ShowObjectInfo(selectedPlanet, days);

                    // Show all moons for this planet
                    var planetMoons = moons.Where(m => m.Orbits == selectedPlanet);

                    foreach (var moon in planetMoons)
                    {
                        ShowObjectInfo(moon, days);
                    }
                }
                else
                {
                    // If it's not a planet (like Sun), just show the object itself
                    ShowObjectInfo(selectedObject, days);
                }
            }
            else
            {
                Console.WriteLine($"Object with name '{objectName}' not found.");
            }
        }

        Console.ReadLine();
    }

    static void ShowObjectInfo(SpaceObject obj, int days)
    {
        obj.Draw();
        var (x, y) = obj.GetPosition(days);
        Console.WriteLine($" Position after {days} days: X = {x:F2}, Y = {y:F2}\n");
    }
}
