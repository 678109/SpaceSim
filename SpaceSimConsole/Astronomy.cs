using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpaceSimulator;
class Astronomy
{
    static void Main()
    {
        List<SpaceObject> solarSystem = new List<SpaceObject> {
                new Star("Sun") {OrbitalRadius = 0, OrbitalPeriod = 0, ObjectRadius = 696340, RotationPeriod = 609.12, Colour = "yellow"},

                new Planet("Mercury") {OrbitalRadius = 57909, OrbitalPeriod = 87.97, ObjectRadius = 2439.7, RotationPeriod = 1407.6, Colour = "grey"},
                new Planet("Venus") {OrbitalRadius = 108208, OrbitalPeriod = 224.7, ObjectRadius = 6051.8, RotationPeriod = -5832.5, Colour = "yellow"},
                new Planet("Earth") {OrbitalRadius = 149598, OrbitalPeriod = 365.26, ObjectRadius = 6371.0, RotationPeriod = 24, Colour = "blue"},

                new Moon("The moon") {OrbitalRadius = 384.4, OrbitalPeriod = 27.32, ObjectRadius = 1737.1, RotationPeriod = 655.7, Colour = "grey"},
                new Moon("Ganymede") {OrbitalRadius = 1070.4, OrbitalPeriod = 7.15, ObjectRadius = 2634.1, RotationPeriod = 171.7, Colour = "grey"},
                new Moon("Titan") {OrbitalRadius = 1222, OrbitalPeriod = 15.95, ObjectRadius = 2575.5, RotationPeriod = 382.8, Colour = "orange"},

                new Asteroid("Ceres") {OrbitalRadius = 413700, OrbitalPeriod = 1680.5, ObjectRadius = 473, RotationPeriod = 9.07, Colour = "grey"},
                new Asteroid("Vesta") {OrbitalRadius = 353400, OrbitalPeriod = 1325.75, ObjectRadius = 262.7, RotationPeriod = 5.34, Colour = "grey"},
                new Asteroid("Eros") {OrbitalRadius = 218000, OrbitalPeriod = 642.01, ObjectRadius = 16.84, RotationPeriod = 5.27, Colour = "grey"},

                new Comet("Halley") {OrbitalRadius = 2667000, OrbitalPeriod = 27750, ObjectRadius = 5.5, RotationPeriod = 2.2, Colour = "white"},
                new Comet("Hale-Bopp") {OrbitalRadius = 2860000, OrbitalPeriod = 104000, ObjectRadius = 30.0, RotationPeriod = 11.5, Colour = "white"},
                new Comet("Hyakutake") {OrbitalRadius = 1040000, OrbitalPeriod = 70000, ObjectRadius = 2.0, RotationPeriod = 6, Colour = "white"},

                new AsteroidBelt("Main Asteroid Belt") {OrbitalRadius = 413700, OrbitalPeriod = 2000, ObjectRadius = -1, RotationPeriod = -1, Colour = "grey"},
                new AsteroidBelt("Kuiper Belt") {OrbitalRadius = 7500000, OrbitalPeriod = -1, ObjectRadius = -1, RotationPeriod = -1, Colour = "grey"},
                new AsteroidBelt("Trojans") {OrbitalRadius = 778500, OrbitalPeriod = 4332, ObjectRadius = -1, RotationPeriod = -1, Colour = "grey"},

                new DwarfPlanet("Pluto") {OrbitalRadius = 5906380, OrbitalPeriod = 90560, ObjectRadius = 1188.3, RotationPeriod = 153.3, Colour = "brown"},
                new DwarfPlanet("Eris") {OrbitalRadius = 10120000, OrbitalPeriod = 203830, ObjectRadius = 1163, RotationPeriod = 25.9, Colour = "grey"},
                new DwarfPlanet("Haumea") {OrbitalRadius = 6452000, OrbitalPeriod = 103400, ObjectRadius = 780, RotationPeriod = 3.9, Colour = "white"},

            };

        foreach (SpaceObject obj in solarSystem)
        {
            obj.Draw();
        }
        Console.ReadLine();
    }

}