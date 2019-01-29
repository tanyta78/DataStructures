namespace MassEffectGalaxyMap
{
    public class Rectangle
    {
        public Rectangle(double x1, double x2, double y1, double y2)
        {
            this.X1 = x1;
            this.X2 = x2;
            this.Y1 = y1;
            this.Y2 = y2;
        }

        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }

        public bool Intersects(Rectangle other)
        {
            return this.X1 <= other.X2 && this.X2 >= other.X1 && this.Y1 <= other.Y2 && this.Y2 >= other.Y1;
        }
    }
}
