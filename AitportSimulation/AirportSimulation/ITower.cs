namespace AirportSimulation
{
    public interface ITower
    {

        int Id { get; set; }
        string Name { get; set; }
        int FuelLeft { get; set; }
        int TimeToTouchDown { get; set; }

        void TouchDown();
    }
}