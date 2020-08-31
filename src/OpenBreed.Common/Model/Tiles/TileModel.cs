﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenBreed.Common.Model.Tiles.Builders;

namespace OpenBreed.Common.Model.Tiles
{
    public class TileModel
    {
        public TileModel(TileBuilder builder)
        {
            Index = builder.Index;
            Data = builder.Data;
        }

        public int Index { get; private set; }
        public byte[] Data { get; private set; }
    }
}