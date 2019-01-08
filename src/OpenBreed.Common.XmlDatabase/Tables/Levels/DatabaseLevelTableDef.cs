﻿using OpenBreed.Common.XmlDatabase.Items.Levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Tables.Levels
{
    public class DatabaseLevelTableDef : DatabaseTableDef
    {
        [XmlArray("Items"),
        XmlArrayItem("Level", typeof(LevelDef))]
        public readonly List<LevelDef> Items = new List<LevelDef>();
    }
}