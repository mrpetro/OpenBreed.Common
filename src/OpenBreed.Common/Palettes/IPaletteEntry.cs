﻿using OpenBreed.Common.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Palettes
{
    public interface IPaletteEntry : IEntry
    {
        string DataRef { get; }
    }
}
