using System;
using System.Diagnostics.Contracts;
using System.Security.Cryptography.X509Certificates;

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
            Console.Write("Star  : ");
            base.Draw();
        }
    }

    public class Planet : SpaceObject
    {
        public Planet(String name) : base(name) { }
        public override void Draw() {
            Console.Write("Planet: ");
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
}
