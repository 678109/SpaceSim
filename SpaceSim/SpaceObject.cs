using System;

namespace SpaceSimulator
{
    public class SpaceObject
    {

        public string Name { get; protected set; }
        public double OrbitalRadius { get; set; }
        public double OrbitalPeriod { get; set; }
        public double ObjectRadius { get; set; }
        public double RotationPeriod { get; set; }
        public string Colour { get; set; }
        public double CurrentX { get; protected set; }
        public double CurrentY { get; protected set; }

        public SpaceObject(String name)
        {
            Name = name;
        }

        public virtual void Draw()
        {
            Console.WriteLine($"{Name} (Colour: {Colour}, Diameter: {ObjectRadius * 2} km)");
        }

        public (double X, double Y) GetPosition(double daysSinceStart)
        {
            string logFilePath = "simulation_log.txt";
            using (StreamWriter logFile = new StreamWriter(logFilePath, true))
            {
                logFile.WriteLine($"GetPosition() called for {Name} with daysSinceStart = {daysSinceStart}");
            }

            if (OrbitalPeriod <= 0)
            {
                using (StreamWriter logFile = new StreamWriter(logFilePath, true))
                {
                    logFile.WriteLine($"{Name} is stationary (OrbitalPeriod = {OrbitalPeriod})");
                }
                return (0, 0);
            }

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

        public class Star : SpaceObject
    {
        public Star(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Star : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public List<Moon> Moons { get; set; } = new List<Moon>();

        public Planet(String name) : base(name) { }

        public override void Draw()
        {
            Console.Write("Planet : ");
            base.Draw();
        }

        // 🎯 Oppdater planetens posisjon basert på tid
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


        public class Moon : SpaceObject
    {
        public Planet Orbits { get; set; }

        public Moon(string name, Planet orbits) : base(name)
        {
            Orbits = orbits;
        }

        public override void Draw()
        {
            Console.WriteLine($"[Moon] {Name} (Orbits: {Orbits.Name}, Colour: {Colour}, Diameter: {ObjectRadius * 2} km)");
        }
        /*
        public virtual (double X, double Y) GetPosition(double daysSinceStart)
        {
            // Månen sin posisjon er relativ til planeten den går rundt
            var (planetX, planetY) = Orbits.GetPosition(daysSinceStart);

            // Beregn måne sin posisjon i forhold til planeten
            double angle = 2 * Math.PI * daysSinceStart / OrbitalPeriod;
            double moonX = planetX + OrbitalRadius * Math.Cos(angle);
            double moonY = planetY + OrbitalRadius * Math.Sin(angle);

            return (moonX, moonY);
        }

        */

        public (double X, double Y) GetPosition(double daysSinceStart)
        {
            double angle = 2 * Math.PI * (daysSinceStart / OrbitalPeriod);
            double x = OrbitalRadius * Math.Cos(angle);
            double y = OrbitalRadius * Math.Sin(angle);
            return (x, y);
        }


        // 🎯 Oppdater månens posisjon basert på tid
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



    public class Asteroid : SpaceObject
    {
        public Asteroid(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid: ");
            base.Draw();
        }
    }

    public class Comet : SpaceObject
    {
        public Comet(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Comet: ");
            base.Draw();
        }
    }

    public class AsteroidBelt : SpaceObject
    {
        public AsteroidBelt(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Asteroid Belt: ");
            base.Draw();
        }
    }
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

