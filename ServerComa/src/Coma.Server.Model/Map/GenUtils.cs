using Coma.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coma.Server.Model.Map
{
    public static class GenUtils
    {
        static Random random = new Random();

        public static List<Position> MazeGen(int baseSize, double mazeness, bool square, int resolution, double rmBorder, double rmNoBorder, double noise)
        {
            List<Position> passable = new List<Position>();
            List<Position> border = new List<Position>();

            passable.Add(new Position(0, 0));
            border.Add(new Position(1, 0));
            border.Add(new Position(-1, 0));
            border.Add(new Position(0, 1));
            border.Add(new Position(0, -1));

            int k = random.Next(4);
            for (int i = 0; i < baseSize; i++)
            {
                Position tile = GetRandomBorder(border, mazeness, true);
                bool isLast = square && tile.Equals(border.Last());
                border.Remove(tile);
                passable.Add(tile);

                Position newBorderTile;
                if (!isLast) k = random.Next(4);

                //int d = (random.Next(2) * 2) - 1;
                //technique pour rendre aléatoire l'ordre d'ajout des nouvelles bordures
                int d = 1;
                k = (k + d) % 4;
                if (k < 0) k += 4;
                for (int j = 0; j < 4; j++)
                {
                    switch (k)
                    {
                        case 0:
                            newBorderTile = new Position(tile.X + 1, tile.Y);
                            if (!passable.Contains(newBorderTile) && !border.Contains(newBorderTile))
                            {
                                border.Add(newBorderTile);
                            }
                            break;
                        case 1:
                            newBorderTile = new Position(tile.X, tile.Y + 1);
                            if (!passable.Contains(newBorderTile) && !border.Contains(newBorderTile))
                            {
                                border.Add(newBorderTile);
                            }
                            break;
                        case 2:
                            newBorderTile = new Position(tile.X - 1, tile.Y);
                            if (!passable.Contains(newBorderTile) && !border.Contains(newBorderTile))
                            {
                                border.Add(newBorderTile);
                            }
                            break;
                        case 3:
                            newBorderTile = new Position(tile.X, tile.Y - 1);
                            if (!passable.Contains(newBorderTile) && !border.Contains(newBorderTile))
                            {
                                border.Add(newBorderTile);
                            }
                            break;
                    }
                    k = (k + d) % 4;
                    if (k < 0) k += 4;
                }
                k = (k - d) % 4;
                if (k < 0) k += 4;
            }

            //var passableSet = new HashSet<Position>(passable);
            //var borderSet = new HashSet<Position>(border);

            //if (rmBorder > 0) RemoveBorder(passableSet, borderSet, rmBorder, random);
            //if (rmNoBorder > 0) RemoveNoBorder(passableSet, borderSet, rmNoBorder, random, false);

            //_tiles = ListToMatrix(passableSet, borderSet, resolution, random, noise);
            //return border;

            return passable;
        }

        private static Position GetRandomBorder(List<Position> border, double mazeness, bool exp)
        {
            int k;

            if (exp)
            {
                k = (int)(Math.Pow(random.NextDouble(), mazeness) * border.Count);
            }
            else
            {
                k = random.Next((int)mazeness);
                if (k != 0) k = 0;
                else k = random.Next(border.Count - 1);
                //idée : choisir ceux plus près du centre
                //et qui ont qu'un seul passable en voisin (mode étoile)
                //sauf si deux passables opposés ?
            }

            return border[border.Count - k - 1];
        }
    }
}
