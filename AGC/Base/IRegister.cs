﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Base
{
    interface IRegister
    {
        MemoryAddress Memory { set; }
    }
}
