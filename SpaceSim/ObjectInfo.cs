using System.Collections.Generic;

namespace SpaceSimulator
{
    public static class ObjectInfo
    {
        public static (List<Planet> Planets, List<Star> Stars, List<Moon> Moons) GetSolarSystem()
        {
            var sun = new Star("Sun")
            {
                OrbitalRadius = 0,
                OrbitalPeriod = 0,
                ObjectRadius = 696340,
                RotationPeriod = 609.12,
                Colour = "yellow"
            };

            var mercury = new Planet("Mercury")
            {
                OrbitalRadius = 57909,
                OrbitalPeriod = 87.97,
                ObjectRadius = 2439.7,
                RotationPeriod = 1407.6,
                Colour = "grey"
            };

            var venus = new Planet("Venus")
            {
                OrbitalRadius = 108208,
                OrbitalPeriod = 224.7,
                ObjectRadius = 6051.8,
                RotationPeriod = -5832.5,
                Colour = "yellow"
            };

            var earth = new Planet("Earth")
            {
                OrbitalRadius = 149598,
                OrbitalPeriod = 365.26,
                ObjectRadius = 6371.0,
                RotationPeriod = 24,
                Colour = "blue"
            };

            var mars = new Planet("Mars")
            {
                OrbitalRadius = 227939,
                OrbitalPeriod = 686.98,
                ObjectRadius = 3389.5,
                RotationPeriod = 24.6,
                Colour = "red"
            };

            var jupiter = new Planet("Jupiter")
            {
                OrbitalRadius = 778330,
                OrbitalPeriod = 4332.71,
                ObjectRadius = 69911,
                RotationPeriod = 9.93,
                Colour = "orange"
            };

            var saturn = new Planet("Saturn")
            {
                OrbitalRadius = 1429400,
                OrbitalPeriod = 10759.50,
                ObjectRadius = 58232,
                RotationPeriod = 10.7,
                Colour = "yellow"
            };

            var uranus = new Planet("Uranus")
            {
                OrbitalRadius = 2870990,
                OrbitalPeriod = 30685.00,
                ObjectRadius = 25362,
                RotationPeriod = -17.2,
                Colour = "blue"
            };

            var neptune = new Planet("Neptune")
            {
                OrbitalRadius = 4504300,
                OrbitalPeriod = 60190.00,
                ObjectRadius = 24622,
                RotationPeriod = 16.1,
                Colour = "blue"
            };

            var pluto = new DwarfPlanet("Pluto")
            {
                OrbitalRadius = 5913520,
                OrbitalPeriod = 90550,
                ObjectRadius = 1188.3,
                RotationPeriod = 153.3,
                Colour = "brown"
            };

            // Måner
            var theMoon = new Moon("The Moon", earth)
            {
                OrbitalRadius = 384,
                OrbitalPeriod = 27.32,
                ObjectRadius = 1737.4,
                RotationPeriod = 655.7,
                Colour = "grey"
            };

            var phobos = new Moon("Phobos", mars)
            {
                OrbitalRadius = 9,
                OrbitalPeriod = 0.32,
                ObjectRadius = 11.3,
                RotationPeriod = 7.66,
                Colour = "grey"
            };

            var deimos = new Moon("Deimos", mars)
            {
                OrbitalRadius = 23,
                OrbitalPeriod = 1.26,
                ObjectRadius = 6.2,
                RotationPeriod = 30.3,
                Colour = "grey"
            };

            var ganymede = new Moon("Ganymede", jupiter)
            {
                OrbitalRadius = 1070,
                OrbitalPeriod = 7.15,
                ObjectRadius = 2634.1,
                RotationPeriod = 171.7,
                Colour = "grey"
            };

            var europa = new Moon("Europa", jupiter)
            {
                OrbitalRadius = 671,
                OrbitalPeriod = 3.55,
                ObjectRadius = 1560.8,
                RotationPeriod = 85.2,
                Colour = "grey"
            };

            var callisto = new Moon("Callisto", jupiter)
            {
                OrbitalRadius = 1883,
                OrbitalPeriod = 16.69,
                ObjectRadius = 2410.3,
                RotationPeriod = 400.5,
                Colour = "grey"
            };

            var io = new Moon("Io", jupiter)
            {
                OrbitalRadius = 422,
                OrbitalPeriod = 1.77,
                ObjectRadius = 1821.6,
                RotationPeriod = 42.5,
                Colour = "yellow"
            };

            var metis = new Moon("Metis", jupiter)
            {
                OrbitalRadius = 128,
                OrbitalPeriod = 0.29,
                ObjectRadius = 21,
                RotationPeriod = 0.29,
                Colour = "grey"
            };

            var adrastea = new Moon("Adrastea", jupiter)
            {
                OrbitalRadius = 129,
                OrbitalPeriod = 0.30,
                ObjectRadius = 16,
                RotationPeriod = 0.30,
                Colour = "grey"
            };

            var amalthea = new Moon("Amalthea", jupiter)
            {
                OrbitalRadius = 181,
                OrbitalPeriod = 0.50,
                ObjectRadius = 83.5,
                RotationPeriod = 0.50,
                Colour = "red-brown"
            };

            var thebe = new Moon("Thebe", jupiter)
            {
                OrbitalRadius = 222,
                OrbitalPeriod = 0.67,
                ObjectRadius = 49.3,
                RotationPeriod = 0.67,
                Colour = "grey"
            };

            var leda = new Moon("Leda", jupiter)
            {
                OrbitalRadius = 11094,
                OrbitalPeriod = 238.72,
                ObjectRadius = 10,
                RotationPeriod = 238.72,
                Colour = "grey"
            };

            var himalia = new Moon("Himalia", jupiter)
            {
                OrbitalRadius = 11480,
                OrbitalPeriod = 250.57,
                ObjectRadius = 85,
                RotationPeriod = 250.57,
                Colour = "grey"
            };

            var lysithea = new Moon("Lysithea", jupiter)
            {
                OrbitalRadius = 11720,
                OrbitalPeriod = 259.22,
                ObjectRadius = 18,
                RotationPeriod = 259.22,
                Colour = "grey"
            };

            var elara = new Moon("Elara", jupiter)
            {
                OrbitalRadius = 11737,
                OrbitalPeriod = 259.65,
                ObjectRadius = 40,
                RotationPeriod = 259.65,
                Colour = "grey"
            };

            var ananke = new Moon("Ananke", jupiter)
            {
                OrbitalRadius = 21200,
                OrbitalPeriod = -631.00,  // retrograd
                ObjectRadius = 14,
                RotationPeriod = -631.00,
                Colour = "grey"
            };

            var carme = new Moon("Carme", jupiter)
            {
                OrbitalRadius = 22600,
                OrbitalPeriod = -692.00,
                ObjectRadius = 23,
                RotationPeriod = -692.00,
                Colour = "grey"
            };

            var pasiphae = new Moon("Pasiphae", jupiter)
            {
                OrbitalRadius = 23500,
                OrbitalPeriod = -735.00,
                ObjectRadius = 30,
                RotationPeriod = -735.00,
                Colour = "grey"
            };

            var sinope = new Moon("Sinope", jupiter)
            {
                OrbitalRadius = 23700,
                OrbitalPeriod = -758.00,
                ObjectRadius = 19,
                RotationPeriod = -758.00,
                Colour = "grey"
            };


            var pan = new Moon("Pan", saturn)
            {
                OrbitalRadius = 134,
                OrbitalPeriod = 0.58,
                ObjectRadius = 14,
                RotationPeriod = 0.58,
                Colour = "grey"
            };

            var atlas = new Moon("Atlas", saturn)
            {
                OrbitalRadius = 138,
                OrbitalPeriod = 0.60,
                ObjectRadius = 15,
                RotationPeriod = 0.60,
                Colour = "grey"
            };

            var prometheus = new Moon("Prometheus", saturn)
            {
                OrbitalRadius = 139,
                OrbitalPeriod = 0.61,
                ObjectRadius = 43.1,
                RotationPeriod = 0.61,
                Colour = "grey"
            };

            var pandora = new Moon("Pandora", saturn)
            {
                OrbitalRadius = 142,
                OrbitalPeriod = 0.63,
                ObjectRadius = 40.7,
                RotationPeriod = 0.63,
                Colour = "grey"
            };

            var epimetheus = new Moon("Epimetheus", saturn)
            {
                OrbitalRadius = 151,
                OrbitalPeriod = 0.69,
                ObjectRadius = 58,
                RotationPeriod = 0.69,
                Colour = "grey"
            };

            var janus = new Moon("Janus", saturn)
            {
                OrbitalRadius = 151,
                OrbitalPeriod = 0.69,
                ObjectRadius = 89.5,
                RotationPeriod = 0.69,
                Colour = "grey"
            };

            var mimas = new Moon("Mimas", saturn)
            {
                OrbitalRadius = 186,
                OrbitalPeriod = 0.94,
                ObjectRadius = 198.2,
                RotationPeriod = 0.94,
                Colour = "grey"
            };

            var enceladus = new Moon("Enceladus", saturn)
            {
                OrbitalRadius = 238,
                OrbitalPeriod = 1.37,
                ObjectRadius = 252.1,
                RotationPeriod = 1.37,
                Colour = "white"
            };

            var tethys = new Moon("Tethys", saturn)
            {
                OrbitalRadius = 295,
                OrbitalPeriod = 1.89,
                ObjectRadius = 531.1,
                RotationPeriod = 1.89,
                Colour = "grey"
            };

            var telesto = new Moon("Telesto", saturn)
            {
                OrbitalRadius = 295,
                OrbitalPeriod = 1.89,
                ObjectRadius = 12.4,
                RotationPeriod = 1.89,
                Colour = "grey"
            };

            var calypso = new Moon("Calypso", saturn)
            {
                OrbitalRadius = 295,
                OrbitalPeriod = 1.89,
                ObjectRadius = 10.7,
                RotationPeriod = 1.89,
                Colour = "grey"
            };

            var dione = new Moon("Dione", saturn)
            {
                OrbitalRadius = 377,
                OrbitalPeriod = 2.74,
                ObjectRadius = 561.4,
                RotationPeriod = 2.74,
                Colour = "grey"
            };

            var helene = new Moon("Helene", saturn)
            {
                OrbitalRadius = 377,
                OrbitalPeriod = 2.74,
                ObjectRadius = 16,
                RotationPeriod = 2.74,
                Colour = "grey"
            };

            var rhea = new Moon("Rhea", saturn)
            {
                OrbitalRadius = 527,
                OrbitalPeriod = 4.52,
                ObjectRadius = 764.3,
                RotationPeriod = 4.52,
                Colour = "grey"
            };

            var titan = new Moon("Titan", saturn)
            {
                OrbitalRadius = 1222,
                OrbitalPeriod = 15.95,
                ObjectRadius = 2575.5,
                RotationPeriod = 15.95,
                Colour = "orange"
            };

            var hyperion = new Moon("Hyperion", saturn)
            {
                OrbitalRadius = 1481,
                OrbitalPeriod = 21.28,
                ObjectRadius = 135,
                RotationPeriod = 21.28,
                Colour = "grey"
            };

            var iapetus = new Moon("Iapetus", saturn)
            {
                OrbitalRadius = 3561,
                OrbitalPeriod = 79.33,
                ObjectRadius = 734.5,
                RotationPeriod = 79.33,
                Colour = "brown"
            };

            var phoebe = new Moon("Phoebe", saturn)
            {
                OrbitalRadius = 12952,
                OrbitalPeriod = -550.48,  // retrograd
                ObjectRadius = 106.5,
                RotationPeriod = -550.48,
                Colour = "grey"
            };


            var cordelia = new Moon("Cordelia", uranus)
            {
                OrbitalRadius = 50,
                OrbitalPeriod = 0.34,
                ObjectRadius = 20,
                RotationPeriod = 0.34,
                Colour = "grey"
            };

            var ophelia = new Moon("Ophelia", uranus)
            {
                OrbitalRadius = 54,
                OrbitalPeriod = 0.38,
                ObjectRadius = 21,
                RotationPeriod = 0.38,
                Colour = "grey"
            };

            var bianca = new Moon("Bianca", uranus)
            {
                OrbitalRadius = 59,
                OrbitalPeriod = 0.43,
                ObjectRadius = 26,
                RotationPeriod = 0.43,
                Colour = "grey"
            };

            var cressida = new Moon("Cressida", uranus)
            {
                OrbitalRadius = 62,
                OrbitalPeriod = 0.46,
                ObjectRadius = 31,
                RotationPeriod = 0.46,
                Colour = "grey"
            };

            var desdemona = new Moon("Desdemona", uranus)
            {
                OrbitalRadius = 63,
                OrbitalPeriod = 0.47,
                ObjectRadius = 29,
                RotationPeriod = 0.47,
                Colour = "grey"
            };

            var juliet = new Moon("Juliet", uranus)
            {
                OrbitalRadius = 64,
                OrbitalPeriod = 0.49,
                ObjectRadius = 42,
                RotationPeriod = 0.49,
                Colour = "grey"
            };

            var portia = new Moon("Portia", uranus)
            {
                OrbitalRadius = 66,
                OrbitalPeriod = 0.51,
                ObjectRadius = 54,
                RotationPeriod = 0.51,
                Colour = "grey"
            };

            var rosalind = new Moon("Rosalind", uranus)
            {
                OrbitalRadius = 70,
                OrbitalPeriod = 0.56,
                ObjectRadius = 36,
                RotationPeriod = 0.56,
                Colour = "grey"
            };

            var belinda = new Moon("Belinda", uranus)
            {
                OrbitalRadius = 75,
                OrbitalPeriod = 0.62,
                ObjectRadius = 45,
                RotationPeriod = 0.62,
                Colour = "grey"
            };

            var puck = new Moon("Puck", uranus)
            {
                OrbitalRadius = 86,
                OrbitalPeriod = 0.76,
                ObjectRadius = 81,
                RotationPeriod = 0.76,
                Colour = "grey"
            };

            var miranda = new Moon("Miranda", uranus)
            {
                OrbitalRadius = 130,
                OrbitalPeriod = 1.41,
                ObjectRadius = 235.8,
                RotationPeriod = 1.41,
                Colour = "grey"
            };

            var ariel = new Moon("Ariel", uranus)
            {
                OrbitalRadius = 191,
                OrbitalPeriod = 2.52,
                ObjectRadius = 578.9,
                RotationPeriod = 2.52,
                Colour = "grey"
            };

            var umbriel = new Moon("Umbriel", uranus)
            {
                OrbitalRadius = 266,
                OrbitalPeriod = 4.14,
                ObjectRadius = 584.7,
                RotationPeriod = 4.14,
                Colour = "grey"
            };

            var titania = new Moon("Titania", uranus)
            {
                OrbitalRadius = 436,
                OrbitalPeriod = 8.71,
                ObjectRadius = 788.9,
                RotationPeriod = 8.71,
                Colour = "grey"
            };

            var oberon = new Moon("Oberon", uranus)
            {
                OrbitalRadius = 583,
                OrbitalPeriod = 13.46,
                ObjectRadius = 761.4,
                RotationPeriod = 13.46,
                Colour = "grey"
            };

            var caliban = new Moon("Caliban", uranus)
            {
                OrbitalRadius = 7169,
                OrbitalPeriod = -580.00,
                ObjectRadius = 36,
                RotationPeriod = -580.00,
                Colour = "grey"
            };

            var stephano = new Moon("Stephano", uranus)
            {
                OrbitalRadius = 7948,
                OrbitalPeriod = -674.00,
                ObjectRadius = 16,
                RotationPeriod = -674.00,
                Colour = "grey"
            };

            var sycorax = new Moon("Sycorax", uranus)
            {
                OrbitalRadius = 12213,
                OrbitalPeriod = -1289.00,
                ObjectRadius = 75,
                RotationPeriod = -1289.00,
                Colour = "grey"
            };

            var prospero = new Moon("Prospero", uranus)
            {
                OrbitalRadius = 16568,
                OrbitalPeriod = -2019.00,
                ObjectRadius = 25,
                RotationPeriod = -2019.00,
                Colour = "grey"
            };

            var setebos = new Moon("Setebos", uranus)
            {
                OrbitalRadius = 17681,
                OrbitalPeriod = -2239.00,
                ObjectRadius = 24,
                RotationPeriod = -2239.00,
                Colour = "grey"
            };


            var naiad = new Moon("Naiad", neptune)
            {
                OrbitalRadius = 48,
                OrbitalPeriod = 0.29,
                ObjectRadius = 29,
                RotationPeriod = 0.29,
                Colour = "grey"
            };

            var thalassa = new Moon("Thalassa", neptune)
            {
                OrbitalRadius = 50,
                OrbitalPeriod = 0.31,
                ObjectRadius = 40,
                RotationPeriod = 0.31,
                Colour = "grey"
            };

            var despina = new Moon("Despina", neptune)
            {
                OrbitalRadius = 53,
                OrbitalPeriod = 0.33,
                ObjectRadius = 75,
                RotationPeriod = 0.33,
                Colour = "grey"
            };

            var galatea = new Moon("Galatea", neptune)
            {
                OrbitalRadius = 62,
                OrbitalPeriod = 0.43,
                ObjectRadius = 88,
                RotationPeriod = 0.43,
                Colour = "grey"
            };

            var larissa = new Moon("Larissa", neptune)
            {
                OrbitalRadius = 74,
                OrbitalPeriod = 0.55,
                ObjectRadius = 97,
                RotationPeriod = 0.55,
                Colour = "grey"
            };

            var proteus = new Moon("Proteus", neptune)
            {
                OrbitalRadius = 118,
                OrbitalPeriod = 1.12,
                ObjectRadius = 210,
                RotationPeriod = 1.12,
                Colour = "grey"
            };

            var triton = new Moon("Triton", neptune)
            {
                OrbitalRadius = 355,
                OrbitalPeriod = -5.88,  // Retrograd bane
                ObjectRadius = 1353.4,
                RotationPeriod = -5.88,
                Colour = "grey"
            };

            var nereid = new Moon("Nereid", neptune)
            {
                OrbitalRadius = 5513,
                OrbitalPeriod = 360.13,
                ObjectRadius = 170,
                RotationPeriod = 360.13,
                Colour = "grey"
            };

            var charon = new Moon("Charon", pluto)
            {
                OrbitalRadius = 20,
                OrbitalPeriod = 6.39,
                ObjectRadius = 606,
                RotationPeriod = 6.39,  
                Colour = "grey"
            };

            var nix = new Moon("Nix", pluto)
            {
                OrbitalRadius = 49,
                OrbitalPeriod = 24.86,
                ObjectRadius = 49,
                RotationPeriod = 24.86,
                Colour = "grey"
            };

            var hydra = new Moon("Hydra", pluto)
            {
                OrbitalRadius = 65,
                OrbitalPeriod = 38.21,
                ObjectRadius = 55,
                RotationPeriod = 38.21,
                Colour = "grey"
            };


            // Koble måner til planetene
            earth.Moons.Add(theMoon);
            mars.Moons.AddRange(new[] { phobos, deimos });
            jupiter.Moons.AddRange(new[] { metis, adrastea, amalthea, thebe, leda, himalia, lysithea, elara, ananke, carme, pasiphae, sinope, io, europa, ganymede, callisto  });
            saturn.Moons.AddRange(new[] { pan, atlas, prometheus, pandora, epimetheus, janus, mimas, enceladus, tethys, telesto, calypso, dione, helene, rhea, titan, hyperion, iapetus, phoebe });
            uranus.Moons.AddRange(new[] { cordelia, ophelia, bianca, cressida, desdemona, juliet, portia, rosalind, belinda, puck, miranda, ariel, umbriel, titania, oberon, caliban, stephano,  sycorax, prospero, setebos });
            neptune.Moons.AddRange(new[] { naiad, thalassa, despina, galatea, larissa, proteus, triton, nereid});
            pluto.Moons.AddRange(new[] {charon, nix, hydra});

            var planets = new List<Planet> { mercury, venus, earth, mars, jupiter, saturn, uranus, neptune, pluto };
            var stars = new List<Star> { sun };
            var moons = new List<Moon>
        {
            // Earth
            theMoon,
    
            // Mars
             phobos, deimos,

            // Jupiter
            adrastea, amalthea, ananke, callisto, carme,
            elara, europa, ganymede, himalia, io, leda,
            lysithea, metis, pasiphae, sinope, thebe,

            // Saturn
            atlas, calypso, dione, enceladus, epimetheus,
            helene, hyperion, iapetus, janus, mimas, pandora,
            pan, phoebe, prometheus, rhea, telesto, titan,

            // Uranus
            ariel, belinda, bianca, caliban, cressida,
            desdemona, juliet, miranda, oberon, ophelia,
            portia, prospero, puck, rosalind, setebos,
            stephano, sycorax, titania, umbriel,

            // Neptune
            despina, galatea, larissa, naiad, nereid, thalassa, triton,

            // Pluto
            charon, hydra, nix
        };


            return (planets, stars, moons);

        }
    }
}
