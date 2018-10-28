using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Coma.Common.Message
{
    public class BankMessage : BaseMessage
    {
        #region public properties

        public int Cells { get; set; }
        public int Nutrients { get; set; }
        public int Thoughts { get; set; }
        public int Ideas { get; set; }

        #endregion

        #region constructors

        public BankMessage(int cells, int nutrients, int thoughts, int ideas)
            : this()
        {
            Cells = cells;
            Nutrients = nutrients;
            Thoughts = thoughts;
            Ideas = ideas;
        }

        public BankMessage()
            : base("bnk")
        { }

        #endregion

        #region serialization

        public override void DeserializeArguments(string args)
        {
            try
            {
                var splitArgs = args.Split(';');

                Cells = int.Parse(splitArgs[0]);
                Nutrients = int.Parse(splitArgs[1]);
                Thoughts = int.Parse(splitArgs[2]);
                Ideas = int.Parse(splitArgs[3]);

                IsValid = true;
            }
            catch (Exception)
            {
                IsValid = false;
            }
        }

        public override string ToString()
        {
            return base.ToString() + String.Format("{0};{1};{2};{3}", Cells, Nutrients, Thoughts, Ideas);
        }

        #endregion
    }
}
