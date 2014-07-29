using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    public interface IWord
    {
        bool Is16Bit { get; }
        ushort Address { get; }

        ushort Read();
        void Write(IWord word);
    }
}
