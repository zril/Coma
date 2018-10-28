using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Coma.Common.Message
{
    public class CameraMessage : BaseMessage
    {
        #region public properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region constructors

        public CameraMessage(int x, int y)
            : this()
        {
            X = x;
            Y = y;
        }

        public CameraMessage()
            : base("cam")
        { }

        #endregion

        #region serialization

        public override void DeserializeArguments(string args)
        {
            try
            {
                var splitArgs = args.Split(';');
                
                X = int.Parse(splitArgs[0]);
                Y = int.Parse(splitArgs[1]);

                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format("{0};{1}", X, Y);
        }

        #endregion
    }
}
