namespace mccotter_net_api.Models
{
    public class Disc
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public double Weight { get; set; }
        public double Speed { get; set; }
        public double Glide { get; set; }
        public double Turn { get; set; }
        public double Fade { get; set; }
    }
}