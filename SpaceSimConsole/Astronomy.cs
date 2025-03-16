using System;
using System.Collections.Generic;
using System.Linq;
using SpaceSimulator;

class Astronomy
{
    static void Main()
    {   // Get the solar system objects
        var (planets, stars, moons) = ObjectInfo.GetSolarSystem();

        // Combine all objects into one list
        List<SpaceObject> solarSystem = new List<SpaceObject>();
        solarSystem.AddRange(stars);
        solarSystem.AddRange(planets);
        solarSystem.AddRange(moons);

        // Get the number of days since time 0
        Console.WriteLine("Enter the number of days since time 0:");
        if (!int.TryParse(Console.ReadLine(), out int days))
        {   // If the input is invalid, default to 0 days
            Console.WriteLine("Invalid input, defaulting to 0 days");
            days = 0; 
        }

        // Get object name
        Console.WriteLine("Enter name of object (leave empty for Sun & planets):");
        string objectName = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(objectName))
        {
            // Default to showing Sun + all planets
            Console.WriteLine("\n--- Solar System Positions ---");
            foreach (var obj in solarSystem.Where(o => o is Star || o is Planet))
            {
                ShowObjectInfo(obj, days); // Show the object
            }
        }
        else
        {   // Find the object by name
            var selectedObject = solarSystem.FirstOrDefault(o => o.Name.Equals(objectName, StringComparison.OrdinalIgnoreCase));

            if (selectedObject != null) // If the object was found
            {
                if (selectedObject is Planet selectedPlanet) // If it's a planet
                {
                    // Show the planet
                    ShowObjectInfo(selectedPlanet, days); // Show the object

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
                Console.WriteLine($"Object with name '{objectName}' not found."); // If the object wasn't found
            }
        }

        Console.ReadLine(); // Wait for user input before closing
    }

    static void ShowObjectInfo(SpaceObject obj, int days) 
    {
        obj.Draw(); // Draw the object 
        var (x, y) = obj.GetPosition(days); // Get the object's position
        Console.WriteLine($" Position after {days} days: X = {x:F2}, Y = {y:F2}\n");
    }
}
