﻿using OpenBreed.Common.Formats;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Sprites
{
    public interface ISpriteSetEntry : IEntry
    {
        string AssetRef { get; }
        IFormatEntry Format { get; }
    }
}