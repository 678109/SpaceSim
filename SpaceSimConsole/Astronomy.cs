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
                new Planet("Mercury"),
                new Planet("Venus"),
                new Planet("Terra"),
                new Moon("The moon")
            };

        foreach (SpaceObject obj in solarSystem)
        {
            obj.Draw();
        }
        Console.ReadLine();
    }

}