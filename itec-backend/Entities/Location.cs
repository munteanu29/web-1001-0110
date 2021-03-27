namespace itec_backend.Entities
{
    public class LocationEntity: Entity
    {
        public LocationEntity(string name, float price, string xCoordonate, string yCoordonate, bool deleted, string id) : base(deleted, id)
        {
            Name = name;
            Price = price;
            XCoordonate = xCoordonate;
            YCoordonate = yCoordonate;
        }
        public string Name { get; set; }
        public float Price { get; set; } 
        public  string XCoordonate { get; set; }
        public  string YCoordonate { get; set; }
    }
}