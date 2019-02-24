﻿using OpenBreed.Common.XmlDatabase.Items.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Tables.Actions
{
    public class DatabaseActionSetTableDef : DatabaseTableDef
    {
        [XmlArray("Items"),
        XmlArrayItem("ActionSet", typeof(XmlActionSetEntry))]
        public readonly List<XmlActionSetEntry> Items = new List<XmlActionSetEntry>();
    }
}