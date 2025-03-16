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
        private double ScaleFactor = 1.0;          // Zoom level
        private const double MinScaleFactor = 0.2; // Minimum zoom level
        private const double MaxScaleFactor = 5.0; // Maximum zoom level
        private const double MinPlanetSize = 3;    // Minimum size of a planet
        private bool showLabels = true;            // Show planet names
        private bool showOrbits = true;            // Show planet orbits
        private bool isZoomedIn = false;           // Zoomed in on a planet
        private bool showInfoPanel = true;         // Show the info panel
        private SimulationController simulationController; // Simulationcontroller
        private Planet selectedPlanet = null;      // currently selected planet

        //List of planets, moons and the sun
        private List<Planet> planets;
        private List<Moon> moons;
        private Star sun;

        //Variables for dragging the canvas
        private bool isDragging = false;
        private Point lastMousePosition;
        private double offsetX = 0;
        private double offsetY = 0;
        private double daysSinceStart = 0;  // Days since the simulation started


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
            // Get the planets, moons and the sun
            var (planetList, starList, moonList) = ObjectInfo.GetSolarSystem();
            planets = planetList;
            moons = moonList;
            sun = starList[0]; // The first star is the sun

            // Fill the dropdown with planets
            PlanetSelector.ItemsSource = planets;
            PlanetSelector.DisplayMemberPath = "Name"; // Show the Name property in the dropdown

            // Start the simulation
            simulationController = new SimulationController();
            simulationController.DoTick += UpdatePositions; // Abonner på DoTick-eventet
            simulationController.Start(); // Start simuleringen

            DrawSolarSystem();
        }

        private void DrawPlanet(Planet planet, double centerX, double centerY)
        {
            // Get the position of the planet after a certain number of days
            double daysSinceStart = 0;
            var (planetX, planetY) = planet.GetPosition(daysSinceStart);

            // Calculate the scaled position and size of the planet
            double scaledDistance = Math.Log10(planet.OrbitalRadius) * 60 * ScaleFactor;
            double scaledSize = (planet.ObjectRadius / sun.ObjectRadius) * 60 * ScaleFactor;
            scaledSize = Math.Max(scaledSize, MinPlanetSize * ScaleFactor);

            // Calculate the angle of the planet
            double angle = Math.Atan2(planetY, planetX);
            double scaledX = centerX + Math.Cos(angle) * scaledDistance;
            double scaledY = centerY + Math.Sin(angle) * scaledDistance;

            // Draw the planet
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

            // Draw the planet in the center
            DrawCircle(SolarSystemCanvas, centerX, centerY, 40, GetPlanetColor(planet));

            // Only draw the planet name if the info panel is shown
            if (showInfoPanel)
            {
                DrawText(SolarSystemCanvas, centerX, centerY - 50, planet.Name, Brushes.White);
            }

            foreach (var moon in planet.Moons)
            {
                var (moonX, moonY) = moon.GetPosition(daysSinceStart);

                // Calculate the scaled position and size of the moon
                double moonDistance = Math.Sqrt(moonX * moonX + moonY * moonY) * ScaleFactor;
                double moonSize = Math.Max((moon.ObjectRadius / planet.ObjectRadius) * 20, 2);
                double angle = Math.Atan2(moonY, moonX);
                double moonScreenX = centerX + Math.Cos(angle) * moonDistance;
                double moonScreenY = centerY + Math.Sin(angle) * moonDistance;

                // Draw the moon
                DrawCircle(SolarSystemCanvas, moonScreenX, moonScreenY, moonSize, Brushes.Gray);

                if (showOrbits)
                {
                    DrawOrbit(SolarSystemCanvas, centerX, centerY, moonDistance);
                }
            }

            DrawInfoPanel(); 
        }



        private void DrawSolarSystem()
        {
            // Clear the canvas
            SolarSystemCanvas.Children.Clear();
            double centerX = (this.Width / 2) + offsetX;
            double centerY = (this.Height / 2) + offsetY;

            using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
            {
                logFile.WriteLine("\n--- Drawing Solar System ---");
                logFile.WriteLine($"Current mode: {(isZoomedIn ? "Planet View" : "Solar System View")}");
            }

            // Draw the sun in the center of the canvas
            if (isZoomedIn && selectedPlanet != null) // Planet View Mode
            {
                using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                {
                    logFile.WriteLine($"Planet View Mode: Centering on {selectedPlanet.Name}");
                }

                //Only draw the selected planet
                var (planetX, planetY) = selectedPlanet.GetPosition(daysSinceStart);
                double scaledX = centerX;
                double scaledY = centerY;

                DrawCircle(SolarSystemCanvas, scaledX, scaledY, 30, GetPlanetColor(selectedPlanet));

                using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
                {
                    logFile.WriteLine($"Drawing {selectedPlanet.Name} at center ({scaledX}, {scaledY})");
                }

                // draw moons
                foreach (var moon in selectedPlanet.Moons)
                {
                    var (moonX, moonY) = moon.GetPosition(daysSinceStart);

                    // Calculate the scaled position and size of the moon
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
            else // Solar System View Mode
            {
                //draw sun
                DrawCircle(SolarSystemCanvas, centerX, centerY, 60, Brushes.Yellow);
                if (showLabels) DrawText(SolarSystemCanvas, centerX, centerY - 70, "Sun", Brushes.White);

                //draw planets
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

        private void PlanetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Zoom inn på valgt planet
            if (PlanetSelector.SelectedItem is Planet planet)
            {
                isZoomedIn = true;
                selectedPlanet = planet;
                ScaleFactor = 1.5;
                DrawMoons(planet);
            }
        }

        private void SolarSystemCanvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            double zoomAmount = e.Delta > 0 ? 1.2 : 0.8;
            ScaleFactor = Math.Clamp(ScaleFactor * zoomAmount, MinScaleFactor, MaxScaleFactor);

            if (isZoomedIn && selectedPlanet != null)
            {
                DrawMoons(selectedPlanet); // Zoomer inn på måne-visningen
            }
            else
            {
                DrawSolarSystem(); // Zoomer i hele solsystemet
            }
        }


        private void SolarSystemCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right && isZoomedIn)
            {
                isZoomedIn = false;
                selectedPlanet = null;
                ScaleFactor = 1.0;
                DrawSolarSystem();
            }
            else if (e.ChangedButton == MouseButton.Left)
            {
                isDragging = true;
                lastMousePosition = e.GetPosition(this);
            }
        }

        private void SolarSystemCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging) return;

            Point newMousePosition = e.GetPosition(this);
            offsetX += (newMousePosition.X - lastMousePosition.X);
            offsetY += (newMousePosition.Y - lastMousePosition.Y);
            lastMousePosition = newMousePosition;

            // Hvis vi er i planet-visning, dra rundt i måne-visningen
            if (isZoomedIn && selectedPlanet != null)
            {
                DrawMoons(selectedPlanet);
            }
            else
            {
                DrawSolarSystem();
            }
        }


        private void SolarSystemCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isDragging = false;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        { // Toggle labels and orbits
            if (e.Key == Key.L)
            {
                showLabels = !showLabels;
                DrawSolarSystem();
            }
            // Toggle orbits
            else if (e.Key == Key.O)
            {
                showOrbits = !showOrbits;
                DrawSolarSystem();
            }
        }
        // Get the colour of a planet
        private Brush GetPlanetColor(Planet planet)
        {
            return planet.Colour.ToLower() switch
            {
                "grey" => Brushes.Gray,
                "yellow" => Brushes.Yellow,
                "blue" => Brushes.Blue,
                "red" => Brushes.Red,
                "orange" => Brushes.Orange,
                "brown" => Brushes.SaddleBrown,
                _ => Brushes.White
            };
        }
        // Draw a circle on the canvas
        private void DrawCircle(Canvas canvas, double x, double y, double size, Brush color)
        {
            Ellipse ellipse = new Ellipse
            {
                Width = size * 2,
                Height = size * 2,
                Fill = color
            };

            Canvas.SetLeft(ellipse, x - size);
            Canvas.SetTop(ellipse, y - size);
            canvas.Children.Add(ellipse);
        }

        // Draw text on the canvas
        private void DrawText(Canvas canvas, double x, double y, string text, Brush color)
        {
            TextBlock textBlock = new TextBlock
            {
                Text = text,
                Foreground = color,
                FontSize = 16
            };

            Canvas.SetLeft(textBlock, x);
            Canvas.SetTop(textBlock, y);
            canvas.Children.Add(textBlock);
        }

        // Draw an orbit on the canvas
        private void DrawOrbit(Canvas canvas, double centerX, double centerY, double radius)
        {
            Ellipse orbit = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = Brushes.White,
                StrokeThickness = 0.5,
                Opacity = 0.5
            };

            Canvas.SetLeft(orbit, centerX - radius);
            Canvas.SetTop(orbit, centerY - radius);
            canvas.Children.Add(orbit);
        }
        // Toggle the info panel
        private void ToggleInfoButton_Click(object sender, RoutedEventArgs e)
        {
            showInfoPanel = !showInfoPanel; 
            showLabels = showInfoPanel;     
            showOrbits = showInfoPanel;     

            if (isZoomedIn && selectedPlanet != null)
            {
                DrawMoons(selectedPlanet);  
            }
            else
            {
                DrawSolarSystem();          
            }
        }

        // Draw the info panel
        private void DrawInfoPanel()
        {
            if (!showInfoPanel || selectedPlanet == null) return;



            // Create a text block with the planet info
            string infoText = $"Planet: {selectedPlanet.Name}\nMoons: ";
            infoText += selectedPlanet.Moons.Count > 0
            ? "\n" + string.Join("\n", selectedPlanet.Moons.ConvertAll(m => "• " + m.Name))
            : "None";


            TextBlock infoTextBlock = new TextBlock
            {
                Text = infoText,
                Foreground = Brushes.White,
                FontSize = 14
            };
            Canvas.SetLeft(infoTextBlock, 20);
            Canvas.SetTop(infoTextBlock, 60);
            SolarSystemCanvas.Children.Add(infoTextBlock);
        }


        // Update the positions of the planets
        private void UpdatePositions(double timeStep)
        {
            daysSinceStart += timeStep;  

            using (StreamWriter logFile = new StreamWriter("simulation_log.txt", true))
            {
                logFile.WriteLine($"UpdatePositions called at simulated day {daysSinceStart}");
            }

            // Update the position of the planets
            foreach (var planet in planets)
            {
                planet.UpdatePosition(daysSinceStart);  
            }
            // Update the position of the moons
            if (isZoomedIn && selectedPlanet != null)
            {
                foreach (var moon in selectedPlanet.Moons)
                {
                    moon.UpdatePosition(daysSinceStart);
                }
            }

            // Draw the solar system
            Dispatcher.Invoke(() =>
            {
                if (isZoomedIn && selectedPlanet != null)
                {
                    DrawMoons(selectedPlanet);  
                }
                else
                {
                    DrawSolarSystem();          
                }
            });
        }

        // Increase the speed of the simulation
        private void SpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (simulationController != null)
            {
                simulationController.SetSpeed(e.NewValue);
            }
        }
    }
}



