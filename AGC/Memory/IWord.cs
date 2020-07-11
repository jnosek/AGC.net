﻿using Apollo.Virtual.AGC.Math;

namespace Apollo.Virtual.AGC.Memory
{
    public interface IWord
    {
        void Write(OnesCompliment value);
        OnesCompliment Read();
    }
}
