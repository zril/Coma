using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Coma.Common.Message
{
    public class PlayerMessage : BaseMessage
    {
        #region public properties

        public int Id { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        #endregion

        #region constructors

        public PlayerMessage(int id, double x, double y)
            : this()
        {
            Id = id;
            X = x;
            Y = y;
        }

        public PlayerMessage()
            : base("pla")
        { }

        #endregion

        #region serialization

        public override void DeserializeArguments(string args)
        {
            try
            {
                var splitArgs = args.Split(';');

                Id = int.Parse(splitArgs[0]);
                X = double.Parse(splitArgs[1].Replace(',', '.'), CultureInfo.InvariantCulture);
                Y = double.Parse(splitArgs[2].Replace(',', '.'), CultureInfo.InvariantCulture);

                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format("{0};{1};{2}", Id, X, Y);
        }

        #endregion
    }
}
