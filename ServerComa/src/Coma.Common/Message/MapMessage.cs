using Coma.Common.Map;

namespace Coma.Common.Message
{
    public class MapMessage : BaseMessage
    {

        #region public properties
        public Tile[,] TileMap;

        #endregion

        #region

        public MapMessage(Tile[,] map) : this()
        {
            this.TileMap = map;
        }

        public MapMessage() : base("map") { }

        #endregion
        
        #region serialization
        public override void DeserializeArguments(string args)
        {
            string[] messageTab = args.Split('#');

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

            this.TileMap = map;
        }

        public override string ToString()
        {
            string message = string.Format("{0},{1}", TileMap.GetLength(0), TileMap.GetLength(1));

            for (int y = 0; y < TileMap.GetLength(1); y++)
            {
                for (int x = 0; x < TileMap.GetLength(0); x++)
                {
                    message += "#" + TileMap[x, y].ToMessage();
                }
            }

            return message;
        }

        #endregion
    }
}