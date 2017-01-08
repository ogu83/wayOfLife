
namespace wayOfLifeEngine
{
    public class Element
    {
        public Element(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Id { get; set; }
    }
}
