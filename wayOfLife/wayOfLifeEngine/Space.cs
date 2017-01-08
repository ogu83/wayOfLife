using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Space
    {
        private Random rand;
        public int Width { get; private set; }
        public int Height { get; private set; }
        public List<Element> Elements { get; private set; }
        public int[,] Position { get; private set; }
        public int CycleNo = 0;

        public Space(int width, int height, int elementCount)
        {
            rand = new Random();
            Elements = new List<Element>();
            Width = width;
            Height = height;
            Position = new int[Width, Height];
            for (int i = 0; i < elementCount; i++)
            {
                var elem_X = rand.Next(width);
                var elem_Y = rand.Next(height);
                var elem = new Element(elem_X, elem_Y);
                elem.Id = i;
                Elements.Add(elem);
                Position[elem_X, elem_Y] = elem.Id;
            }
        }

        public void Cycle()
        {
            CycleNo++;
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    if (isFilledCell(w, h))
                    {
                        if (isCovered3Sides(w, h)) //DEAD
                        {
                            Position[w, h] = 0;
                            Elements.Remove(Elements.Where(x => x.X == w && x.Y == h).FirstOrDefault());
                        }
                        else //BORN
                        {
                            if (CycleNo % 2 == 0) return;

                            var bornside = rand.Next(4);
                            switch (bornside)
                            {
                                case 0:
                                    if (!isLeftFilled(w, h))
                                    {
                                        var elem_X = w - 1;
                                        var elem_Y = h;
                                        var elem = new Element(elem_X, elem_Y);
                                        elem.Id = Elements.Max(x => x.Id) + 1;
                                        Elements.Add(elem);
                                        Position[elem_X, elem_Y] = elem.Id;
                                    }
                                    break;
                                case 1:
                                    if (!isRightFilled(w, h))
                                    {
                                        var elem_X = w + 1;
                                        var elem_Y = h;
                                        var elem = new Element(elem_X, elem_Y);
                                        elem.Id = Elements.Max(x => x.Id) + 1;
                                        Elements.Add(elem);
                                        Position[elem_X, elem_Y] = elem.Id;
                                    }
                                    break;
                                case 2:
                                    if (!isTopFilled(w, h))
                                    {
                                        var elem_X = w;
                                        var elem_Y = h - 1;
                                        var elem = new Element(elem_X, elem_Y);
                                        elem.Id = Elements.Max(x => x.Id) + 1;
                                        Elements.Add(elem);
                                        Position[elem_X, elem_Y] = elem.Id;
                                    }
                                    break;
                                case 3:
                                    if (!isBottomFilled(w, h))
                                    {
                                        var elem_X = w;
                                        var elem_Y = h + 1;
                                        var elem = new Element(elem_X, elem_Y);
                                        elem.Id = Elements.Max(x => x.Id) + 1;
                                        Elements.Add(elem);
                                        Position[elem_X, elem_Y] = elem.Id;
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
        }

        private bool isFilledCell(int x, int y)
        {
            return (Position[x, y] > 0);
        }
        private bool isCovered3Sides(int x, int y)
        {
            var sideCount = isLeftFilled(x, y) ? 1 : 0;
            sideCount += isRightFilled(x, y) ? 1 : 0;
            sideCount += isTopFilled(x, y) ? 1 : 0;
            sideCount += isBottomFilled(x, y) ? 1 : 0;
            return sideCount > 2;
        }
        private bool isLeftFilled(int x, int y)
        {
            if (x == 0) return true;
            return Position[x - 1, y] > 0;
        }
        private bool isRightFilled(int x, int y)
        {
            if (x == Width - 1) return true;
            return Position[x + 1, y] > 0;
        }
        private bool isTopFilled(int x, int y)
        {
            if (y == 0) return true;
            return Position[x, y - 1] > 0;
        }
        private bool isBottomFilled(int x, int y)
        {
            if (y == Height - 1) return true;
            return Position[x, y + 1] > 0;
        }

        private string enter { get { return "\r\n"; } }

        public override string ToString()
        {
            var result = string.Format("Cycle:{0},Population:{1},Available:{2},Rate:{3}{4}",
                CycleNo, Elements.Count, Width * Height, (double)Elements.Count / (double)(Width * Height), enter);
            for (int h = 0; h < Height; h++)
            {
                for (int w = 0; w < Width; w++)
                {
                    result += isFilledCell(w, h) ? "X" : "_";
                }
                result += enter;
            }
            result += string.Format("Cycle:{0},Population:{1},Available:{2},Rate:{3}{4}",
                CycleNo, Elements.Count, Width * Height, (double)Elements.Count / (double)(Width * Height), enter);
            return result;
        }
    }
}