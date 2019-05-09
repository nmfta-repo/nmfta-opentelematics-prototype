namespace Prototype.OpenTelematics.DataAccess
{ 
    public class SimulatedData_HwyDataPoints
    {
        public int Id { get; set; }
        public string NodeId { get; set; }
        public string HwySectionName { get; set; }
        public int Sequence { get; set; }
        public string Direction { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }    
    }
}