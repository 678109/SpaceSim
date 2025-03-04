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
                new Star("Sun"),
                new Star("Betelgeuse"),
                new Star("Proxima Centauri"),

                new Planet("Mercury"),
                new Planet("Venus"),
                new Planet("Terra"),

                new Moon("The moon"),
                new Moon("Ganymede"),
                new Moon("Titan"),

                new Asteroid("Ceres"),
                new Asteroid("Vesta"),
                new Asteroid("Eros"),

                new Comet("Halley"),
                new Comet("Hale-Bopp"),
                new Comet("Hyakutake"),

                new AsteroidBelt("Main Asteroid Belt"),
                new AsteroidBelt("Kuiper Belt"),
                new AsteroidBelt("Trojans"),

                new DwarfPlanet("Pluto"),
                new DwarfPlanet("Eris"),
                new DwarfPlanet("Haumea"),

                new BlackHole("Sagittarius A*"),
                new BlackHole("Cygnus X-1"),
                new BlackHole("Messier 87"),

                new Galaxy("Milky Way"),
                new Galaxy("Andromeda"),
                new Galaxy("Triangulum Galaxy"),
            };

        foreach (SpaceObject obj in solarSystem)
        {
            obj.Draw();
        }
        Console.ReadLine();
    }

}