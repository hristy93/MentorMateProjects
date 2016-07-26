namespace AirportSimulation
{
    public interface ITower
    {

        int Id { get; set; }
        string Name { get; set; }
        string ManufacturingNumber { get; set; }
        int Weight { get; set; }
        int FuelConsumption { get; set; }
        int FuelTankCapacity { get; set; }
        int TouchDownTime { get; set; }
        int PassengersCount { get; set; }
        int MaxPassengersCount { get; set; }
        int FuelLeft { get; set; }

        void TouchDown();
    }
}