using System;

namespace SpaceSimulator
{
    public class SpaceObject
    {

        public String Name { get; protected set;}

        public SpaceObject(string name) {
            Name = name;
        }
            public virtual void Draw() {
            Console.WriteLine(Name);
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
    public class BlackHole : SpaceObject
    {
        public BlackHole(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Black Hole: ");
            base.Draw();
        }
    }
    public class Galaxy : SpaceObject
    {
        public Galaxy(String name) : base(name) { }
        public override void Draw()
        {
            Console.Write("Galaxy: ");
            base.Draw();
        }
    }
}
