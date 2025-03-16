using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using SpaceSimulator; // Importer ObjectInfo
using System.IO;


namespace SpaceSimWPF
{
    public partial class MainWindow : Window
    {
        private double ScaleFactor = 1.0;
        private const double MinScaleFactor = 0.2;
        private const double MaxScaleFactor = 5.0;
        private const double MinPlanetSize = 3;
        private bool showLabels = true;
        private bool showOrbits = true;
        private bool isZoomedIn = false;
        private bool showInfoPanel = true;
        private SimulationController simulationController;
        private Planet selectedPlanet = null;

        private List<Planet> planets;
        private List<Moon> moons;
        private Star sun;

        private bool isDragging = false;
        private Point lastMousePosition;
        private double offsetX = 0;
        private double offsetY = 0;
        private double daysSinceStart = 0;  // Holder styr på hvor lenge simuleringen har kjørt


        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.KeyDown += MainWindow_KeyDown;
            this.MouseWheel += SolarSystemCanvas_MouseWheel;
            this.MouseDown += SolarSystemCanvas_MouseDown;
            this.MouseMove += SolarSystemCanvas_MouseMove;
            this.MouseUp += SolarSystemCanvas_MouseUp;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            var (planetList, starList, moonList) = ObjectInfo.GetSolarSystem();
            planets = planetList;
            moons = moonList;
            sun = starList[0];

            // 🪐 Fyll dropdown-menyen med planetene
            PlanetSelector.ItemsSource = planets;
            PlanetSelector.DisplayMemberPath = "Name"; // Vis planetnavn i dropdown

            // 🎯 Start simulasjonskontrolleren
            simulationController = new SimulationController();
            simulationController.DoTick += UpdatePositions; // Abonner på DoTick-eventet
            simulationController.Start(); // Start simuleringen

            DrawSolarSystem();
        }


       







        private void DrawPlanet(Planet planet, double centerX, double centerY)
        {
            double daysSinceStart = 0;
            var (planetX, planetY) = planet.GetPosition(daysSinceStart);

            double scaledDistance = Math.Log10(planet.OrbitalRadius) * 60 * ScaleFactor;
            double scaledSize = (planet.ObjectRadius / sun.ObjectRadius) * 60 * ScaleFactor;
            scaledSize = Math.Max(scaledSize, MinPlanetSize * ScaleFactor);

            double angle = Math.Atan2(planetY, planetX);
            double scaledX = centerX + Math.Cos(angle) * scaledDistance;
            double scaledY = centerY + Math.Sin(angle) * scaledDistance;

            DrawCircle(SolarSystemCanvas, scaledX, scaledY, scaledSize, GetPlanetColor(planet));
            if (showLabels && showInfoPanel)
            {
                DrawText(SolarSystemCanvas, scaledX + (scaledSize + 5), scaledY, planet.Name, Brushes.White);
            }

            if (showOrbits) DrawOrbit(SolarSystemCanvas, centerX, centerY, scaledDistance);
        }


        private void DrawMoons(Planet planet)
        {
            SolarSystemCanvas.Children.Clear();
            double centerX = this.Width / 2 + offsetX;
            double centerY = this.Height / 2 + offsetY;

            // 🌍 Tegn planeten i midten
            DrawCircle(SolarSystemCanvas, centerX, centerY, 40, GetPlanetColor(planet));

            // 🎯 Kun tegn planetnavn hvis info-panelet er på
            if (showInfoPanel)
            {
                DrawText(SolarSystemCanvas, centerX, centerY - 50, planet.Name, Brushes.White);
            }

            foreach (var moon in planet.Moons)
            {
                var (moonX, moonY) = moon.GetPosition(daysSinceStart);

                double moonDistance = Math.Sqrt(moonX * moonX + moonY * moonY) * ScaleFactor;
                double moonSize = Math.Max((moon.ObjectRadius / planet.ObjectRadius) * 20, 2);
                double angle = Math.Atan2(moonY, moonX);
                double moonScreenX = centerX + Math.Cos(angle) * moonDistance;
                double moonScreenY = centerY + Math.Sin(angle) * moonDistance;

                DrawCircle(SolarSystemCanvas, moonScreenX, moonScreenY, moonSize, Brushes.Gray);

                if (showOrbits)
                {
                    DrawOrbit(SolarSystemCanvas, centerX, centerY, moonDistance);
                }
            }

            DrawInfoPanel(); // 🎯 Tegn info-panelet i planetvisning
        }



        private void DrawSolarSystem()
        {
            SolarSystemCanvas.Children.Clear();
            double centerX = (this.Width / 2) + offsetX;
            double centerY = (this.Height / 2) + offsetY;

            using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
            {
                logFile.WriteLine("\n--- Drawing Solar System ---");
                logFile.WriteLine($"Current mode: {(isZoomedIn ? "Planet View" : "Solar System View")}");
            }

            if (isZoomedIn && selectedPlanet != null) // 🌍 Planet View Mode
            {
                using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                {
                    logFile.WriteLine($"Planet View Mode: Centering on {selectedPlanet.Name}");
                }

                // 🎯 Tegn kun den valgte planeten i midten
                var (planetX, planetY) = selectedPlanet.GetPosition(daysSinceStart);
                double scaledX = centerX;
                double scaledY = centerY;

                DrawCircle(SolarSystemCanvas, scaledX, scaledY, 30, GetPlanetColor(selectedPlanet));

                using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                {
                    logFile.WriteLine($"Drawing {selectedPlanet.Name} at center ({scaledX}, {scaledY})");
                }

                // 🌙 Tegn måner i forhold til planeten
                foreach (var moon in selectedPlanet.Moons)
                {
                    var (moonX, moonY) = moon.GetPosition(daysSinceStart);

                    double moonDistance = Math.Sqrt(moonX * moonX + moonY * moonY) * ScaleFactor;
                    double moonSize = Math.Max((moon.ObjectRadius / selectedPlanet.ObjectRadius) * 20, 2);
                    double angle = Math.Atan2(moonY, moonX);
                    double moonScreenX = centerX + Math.Cos(angle) * moonDistance;
                    double moonScreenY = centerY + Math.Sin(angle) * moonDistance;

                    DrawCircle(SolarSystemCanvas, moonScreenX, moonScreenY, moonSize, Brushes.Gray);

                    if (showOrbits)
                        DrawOrbit(SolarSystemCanvas, centerX, centerY, moonDistance);

                    using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                    {
                        logFile.WriteLine($"Drawing Moon {moon.Name} at ({moonScreenX}, {moonScreenY})");
                    }
                }
            }
            else // ☀️ Solar System View Mode
            {
                // 🌞 Tegn solen (kun i Solar System View)
                DrawCircle(SolarSystemCanvas, centerX, centerY, 60, Brushes.Yellow);
                if (showLabels) DrawText(SolarSystemCanvas, centerX, centerY - 70, "Sun", Brushes.White);

                // 🪐 Tegn alle planeter med riktig skalering
                foreach (var planet in planets)
                {
                    double scaledDistance = Math.Log10(planet.OrbitalRadius + 1) * 100 * ScaleFactor;
                    double scaledSize = (planet.ObjectRadius / sun.ObjectRadius) * 60 * ScaleFactor;
                    scaledSize = Math.Max(scaledSize, MinPlanetSize * ScaleFactor);

                    var (planetX, planetY) = planet.GetPosition(daysSinceStart);

                    double scaledX = centerX + (planetX / planet.OrbitalRadius) * scaledDistance;
                    double scaledY = centerY + (planetY / planet.OrbitalRadius) * scaledDistance;

                    DrawCircle(SolarSystemCanvas, scaledX, scaledY, scaledSize, GetPlanetColor(planet));

                    using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                    {
                        logFile.WriteLine($"Drawing {planet.Name} at ({scaledX}, {scaledY}) after update");
                    }

                    if (showLabels) DrawText(SolarSystemCanvas, scaledX + (scaledSize + 5), scaledY, planet.Name, Brushes.White);
                    if (showOrbits) DrawOrbit(SolarSystemCanvas, centerX, centerY, scaledDistance);
                }
            }
        }






