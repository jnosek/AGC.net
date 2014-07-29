using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    class Word : IWord
    {
        public Word()
        {
        }

        public Word(ushort value)
        {
            this.Value = value;
            this.Is16Bit = true;
        }

        public ushort Value { get; set; }
        public bool Is16Bit { get; set; }
        public ushort Address { get;set; }

        public ushort Read()
        {
            return Value;
        }

        public void Write(IWord word)
        {
            Value = word.Read();
        }
    }
}
