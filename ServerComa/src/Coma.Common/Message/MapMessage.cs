using Coma.Common.Map;

namespace Coma.Common.Message
{
    public class MapMessage : BaseMessage
    {

        #region public properties
        Tile[,] map;

        #endregion

        #region

        public MapMessage(Tile[,] map) : this()
        {
            this.map = map;
        }

        public MapMessage() : base("map") { }

        #endregion
        
        #region serialization
        public override void DeserializeArguments(string args)
        {
            string[] messageTab = args.Split('|');

            string[] sizeTab = messageTab[0].Split(';');
            int width = int.Parse(sizeTab[0]);
            int height = int.Parse(sizeTab[1]);

            Tile[,] map = new Tile[width, height];
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    //+1 pour le décalage de 1 dans le message
                    int index = y * width + x + 1;
                    Tile tile = new Tile();
                    tile.FromMessage(messageTab[index]);
                    map[x, y] = tile;
                }
            }

            this.map = map;
        }

        public override string ToString()
        {
            string message = string.Format("{0},{1}", map.GetLength(0), map.GetLength(1));

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    message += "|" + map[x, y].ToMessage();
                }
            }

            return message;
        }

        #endregion
    }
}