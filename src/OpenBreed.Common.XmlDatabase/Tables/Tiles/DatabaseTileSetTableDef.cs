﻿using OpenBreed.Common.XmlDatabase.Items.Tiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Tables.Tiles
{
    public class DatabaseTileSetTableDef : DatabaseTableDef
    {
        [XmlArray("Items"),
        XmlArrayItem("TileSet", typeof(TileSetDef))]
        public readonly List<TileSetDef> Items = new List<TileSetDef>();
    }
}