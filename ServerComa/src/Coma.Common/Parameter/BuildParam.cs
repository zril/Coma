using Coma.Common.Map.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Parameter
{
    public class BuildParam : BaseParam
    {
        #region public properties

        public TileItemType Id { get; set; }
        public Position Position { get; set; }

        #endregion

        #region constructors

        public BuildParam()
            : base("bld")
        { }

        public BuildParam(Position pos, TileItemType id)
            : this()
        {
            this.Position = pos;
            this.Id = id;
        }

        #endregion

        #region serialization

        public override void DeserializeArguments(string args)
        {
            try
            {
                var splitArgs = args.Split(';');

                Id = (TileItemType) int.Parse(splitArgs[0]);
                Position = Utils.PosFromString(splitArgs[1]);


                IsValid = true;
            } catch (Exception)
            {
                IsValid = false;
            }
                
        }

        public override string ToString()
        {
            return base.ToString() + (int)Id + ";" + Utils.PosToString(Position);
        }

        #endregion

        
    }
}
