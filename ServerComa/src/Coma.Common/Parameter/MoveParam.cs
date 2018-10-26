using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Coma.Common.Parameter
{
    public class MoveParam : BaseParam
    {
        #region public properties

        public Direction Direction { get; set; }
        public bool Active { get; set; }

        #endregion

        #region constructors

        public MoveParam()
            : base("mov")
        { }

        public MoveParam(Direction dir, bool active)
            : this()
        {
            this.Direction = dir;
            this.Active = active;
        }

        #endregion

        #region serialization

        public override void DeserializeArguments(string args)
        {
            try
            {
                var splitArgs = args.Split(';');

                Direction = DirFromString(splitArgs[0]);
                Active = "1".Equals(splitArgs[1]);

                IsValid = Direction != Direction.None;
            } catch (Exception)
            {
                IsValid = false;
            }
                
        }

        public override string ToString()
        {
            return base.ToString() + DirToString(Direction) + ";" + (Active?"1":"0");
        }

        #endregion

        private static string DirToString(Direction dir)
        {
            switch (dir)
            {
                case Direction.Down:
                    return "D";
                case Direction.Up:
                    return "U";
                case Direction.Right:
                    return "R";
                case Direction.Left:
                    return "L";
                default:
                    return "N";
            }
        }

        private static Direction DirFromString(string dir)
        {
            switch (dir)
            {
                case "D":
                    return Direction.Down;
                case "U":
                    return Direction.Up;
                case "R":
                    return Direction.Right;
                case "L":
                    return Direction.Left;
                default:
                    return Direction.None;
            }
        }
    }
}
