using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Input;
using SpaceSimulator; // Importer ObjectInfo

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
        private Planet selectedPlanet = null;

        private List<Planet> planets;
        private List<Moon> moons;
        private Star sun;

        private bool isDragging = false;
        private Point lastMousePosition;
        private double offsetX = 0;
        private double offsetY = 0;

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

            DrawSolarSystem();
        }

        private void DrawSolarSystem()
        {
            SolarSystemCanvas.Children.Clear();
            double centerX = (this.Width / 2) + offsetX;
            double centerY = (this.Height / 2) + offsetY;

            // 🌞 Tegn solen
            DrawCircle(SolarSystemCanvas, centerX, centerY, 60, Brushes.Yellow);
            if (showLabels) DrawText(SolarSystemCanvas, centerX, centerY - 70, "Sun", Brushes.White);

            // 🪐 Tegn alle planeter
            foreach (var planet in planets)
            {
                DrawPlanet(planet, centerX, centerY);
            }
            if (isZoomedIn) DrawInfoPanel(); // 🎯 Tegn info-panelet kun i planetvisning

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
            double centerX = this.Width / 2;
            double centerY = this.Height / 2;

            // 🌍 Tegn planeten i midten
            DrawCircle(SolarSystemCanvas, centerX, centerY, 40, GetPlanetColor(planet));

            // 🎯 Kun tegn planetnavn hvis info-panelet er på
            if (showInfoPanel)
            {
                DrawText(SolarSystemCanvas, centerX, centerY - 50, planet.Name, Brushes.White);
            }

            foreach (var moon in planet.Moons)
            {
                double moonDistance = Math.Log10(moon.OrbitalRadius + 1) * 30 * ScaleFactor;
                double moonSize = Math.Max((moon.ObjectRadius / planet.ObjectRadius) * 20, 2);
                double angle = Math.Atan2(moon.OrbitalRadius, moon.OrbitalRadius);
                double moonX = centerX + Math.Cos(angle) * moonDistance;
                double moonY = centerY + Math.Sin(angle) * moonDistance;

                DrawCircle(SolarSystemCanvas, moonX, moonY, moonSize, Brushes.Gray);

                if (showOrbits)
                {
                    DrawOrbit(SolarSystemCanvas, centerX, centerY, moonDistance);
                }
            }

            DrawInfoPanel(); // 🎯 Tegn info-panelet i planetvisning
        }


        private void PlanetSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
        {
            if (e.Key == Key.L)
            {
                showLabels = !showLabels;
                DrawSolarSystem();
            }
            else if (e.Key == Key.O)
            {
                showOrbits = !showOrbits;
                DrawSolarSystem();
            }
        }

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
        private void ToggleInfoButton_Click(object sender, RoutedEventArgs e)
        {
            showInfoPanel = !showInfoPanel; // 🎯 Veksler mellom å vise/skjule info-panelet
            showLabels = showInfoPanel;     // 🎯 Hvis info-panelet er på, skjuler vi individuelle labels
            showOrbits = showInfoPanel;     // 🎯 Hvis info-panelet er på, viser vi også banene

            if (isZoomedIn && selectedPlanet != null)
            {
                DrawMoons(selectedPlanet);  // 🎯 Oppdater planet-visningen
            }
            else
            {
                DrawSolarSystem();          // 🎯 Oppdater solsystem-visningen
            }
        }

        private void DrawInfoPanel()
        {
            if (!showInfoPanel || selectedPlanet == null) return;

          

            // 🔹 Tekstinnhold
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


    }
}

