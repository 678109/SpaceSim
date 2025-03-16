using System;
using System.Timers;

namespace SpaceSimWPF // 🔹 Sørg for at namespace matcher prosjektet ditt!
{
    public class SimulationController
    {
        private System.Timers.Timer simulationTimer;
        private double timeStep = 1.0; // Tidsskritt for hver oppdatering (1 dag som standard)

        // 🎯 Event som planetene/månene skal lytte til
        public event Action<double> DoTick;

        public SimulationController()
        {
            simulationTimer = new System.Timers.Timer(1000); // Oppdatering hvert 1000 ms
            simulationTimer.Elapsed += OnTick;
            simulationTimer.AutoReset = true;
        }

        public void Start()
        {
            simulationTimer.Start();
        }

        public void Stop()
        {
            simulationTimer.Stop();
        }

        public void SetSpeed(double speedMultiplier)
        {
            timeStep = speedMultiplier; // Endre tidsskritt basert på slider-verdi
        }

        private void OnTick(object sender, ElapsedEventArgs e)
        {
            DoTick?.Invoke(timeStep); // 🔹 Trigge eventet for alle abonnenter
        }
    }
}
