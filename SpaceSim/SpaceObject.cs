﻿using System;

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
            if (OrbitalPeriod <= 0)
                return (0, 0);
            
            double angle = 2 * Math.PI * daysSinceStart / OrbitalPeriod;
            double x = Math.Cos(angle) * OrbitalRadius;
            double y = Math.Sin(angle) * OrbitalRadius;
            return (x, y);
        }
    }

    public class Star : SpaceObject
    {
        public Star(String name) : base(name) { }
        public override void Draw() {
            Console.Write("Star : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(String name) : base(name) { }
        public override void Draw() {
            Console.Write("Planet : ");
            base.Draw();
        }
    }

    public class Moon : SpaceObject {
        public Moon(String name) : base(name) { }
        public override void Draw() {
            Console.Write("Moon  : ");
            base.Draw();
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
