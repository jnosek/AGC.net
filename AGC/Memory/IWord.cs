using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace Apollo.Virtual.AGC.Memory
{
    public interface IWord
    {
        ushort Read();
        void Write(ushort value);
    }
}