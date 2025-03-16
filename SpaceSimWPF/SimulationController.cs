using System.Timers;

public class SimulationController
{
    // Timer for the simulation
    private System.Timers.Timer simulationTimer;
    // Time step for the simulation (how long each tick is in seconds)
    private double timeStep = 1.0;
    // Speed multiplier for the simulation (1.0 is normal speed)
    private double speedMultiplier = 1.0;

    // Event for when a tick occurs
    public event Action<double> DoTick;

    // Constructor
    public SimulationController()
    {
        // Create a timer with a 1 second interval
        simulationTimer = new System.Timers.Timer(1000);
        // Set the event handler for the timer
        simulationTimer.Elapsed += OnTick;
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
        this.speedMultiplier = speedMultiplier;
    }

    public void IncreaseSpeed()
    {
        speedMultiplier *= 2; // Double the speed
    }

    public void DecreaseSpeed()
    {
        speedMultiplier /= 2; // Halve the speed
    }

    // Event handler for the timer tick
    private void OnTick(object sender, ElapsedEventArgs e)
    {
        DoTick?.Invoke(timeStep * speedMultiplier); 
    } 
}

