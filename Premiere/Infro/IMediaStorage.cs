﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Premiere.Infro
{
    interface IMediaStorage
    {
        string Storage(MemoryStream stream, string filename);
        
    }
}
