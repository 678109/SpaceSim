using System;

namespace SpaceSimulator
{
    public class SpaceObject
    {

        public string Name { get; protected set; }     // Name of the object 
        public double OrbitalRadius { get; set; }      // Distance from the object it orbits
        public double OrbitalPeriod { get; set; }      // Time it takes to orbit the object it orbits
        public double ObjectRadius { get; set; }       // Diameter of the object
        public double RotationPeriod { get; set; }     // Time it takes to rotate around its own axis
        public string Colour { get; set; }             // Colour of the object
        public double CurrentX { get; protected set; } // Current X position
        public double CurrentY { get; protected set; } // Current Y position

        public SpaceObject(String name) // Constructor
        {
            Name = name;
        }

        // Draw the object
        public virtual void Draw() 
        {
            Console.WriteLine($"{Name} (Colour: {Colour}, Diameter: {ObjectRadius * 2} km)"); 
        }

        // Get the position of the object after a certain number of days
        public (double X, double Y) GetPosition(double daysSinceStart)
        {
            string logFilePath = "simulation_log.txt";
            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                logFile.WriteLine($"GetPosition() called for {Name} with daysSinceStart = {daysSinceStart}");
            }

            // If the object is stationary, return (0, 0)
            if (OrbitalPeriod <= 0)
            {
                using (StreamWriter logFile = new StreamWriter(logFilePath, true))
                {
                    logFile.WriteLine($"{Name} is stationary (OrbitalPeriod = {OrbitalPeriod})");
                }
                return (0, 0);
            }

            // Calculate the angle based on the number of days since the start
            double angle = 2 * Math.PI * daysSinceStart / OrbitalPeriod;
            double x = Math.Cos(angle) * OrbitalRadius;
            double y = Math.Sin(angle) * OrbitalRadius;

            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                logFile.WriteLine($"{Name} position calculated: X = {x}, Y = {y}");
            }

            return (x, y);
        }
    }
    // Represents a star
    public class Star : SpaceObject
    {
        public Star(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Star : ");
            base.Draw();
        }
    }
    // Represents a planet
    public class Planet : SpaceObject
    {
        public List<Moon> Moons { get; set; } = new List<Moon>();

        public Planet(String name) : base(name) { }

        public override void Draw()
        {
            Console.Write("Planet : ");
            base.Draw();
        }

        //update position of planet
        public void UpdatePosition(double timeStep)
        {
            string logFilePath = "simulation_log.txt";
            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                logFile.WriteLine($"Updating {Name} position at timeStep {timeStep}...");
            }

            if (OrbitalPeriod > 0) // Sjekk at planeten faktisk beveger seg
            {
                double angle = 2 * Math.PI * (timeStep / OrbitalPeriod);
                CurrentX = Math.Cos(angle) * OrbitalRadius;
                CurrentY = Math.Sin(angle) * OrbitalRadius;

                using (StreamWriter logFile = new StreamWriter(logFilePath, true))
                {
                    logFile.WriteLine($"{Name} updated position to ({CurrentX}, {CurrentY})");
                }
            }
        }
    }

        // Represents a moon

        public class Moon : SpaceObject
    {
            public Planet Orbits { get; set; } // The planet this moon orbits

        public Moon(string name, Planet orbits) : base(name)
        {
            Orbits = orbits;
        }

            public override void Draw()
        {
            Console.WriteLine($"[Moon] {Name} (Orbits: {Orbits.Name}, Colour: {Colour}, Diameter: {ObjectRadius * 2} km)");
        }

            // Get the position of the moon after a certain number of days
            public (double X, double Y) GetPosition(double daysSinceStart)
        {
            double angle = 2 * Math.PI * (daysSinceStart / OrbitalPeriod);
            double x = OrbitalRadius * Math.Cos(angle);
            double y = OrbitalRadius * Math.Sin(angle);
            return (x, y);
        }


        // Update moon position based on time
        public void UpdatePosition(double timeStep)
        {
            string logFilePath = "simulation_log.txt";
            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                logFile.WriteLine($"Updating {Name} position at timeStep {timeStep}...");
            }

            if (OrbitalPeriod > 0) // Sjekk at planeten faktisk beveger seg
            {
                double angle = 2 * Math.PI * (timeStep / OrbitalPeriod);
                CurrentX = Math.Cos(angle) * OrbitalRadius;
                CurrentY = Math.Sin(angle) * OrbitalRadius;

                using (StreamWriter logFile = new StreamWriter(logFilePath, true))
                {
                    logFile.WriteLine($"{Name} updated position to ({CurrentX}, {CurrentY})");
                }
            }
        }


    }


    // Represents an asteroid
    public class Asteroid : SpaceObject
    {
        public Asteroid(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid: ");
            base.Draw();
        }
    }

    // Represents a comet
    public class Comet : SpaceObject
    {
        public Comet(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Comet: ");
            base.Draw();
        }
    }

    // Represents an asteroid belt
    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid Belt: ");
            base.Draw();
        }
    }
    // Represents a dwarf planet
    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Dwarf Planet: "); 
            base.Draw();
        }
    }
}

