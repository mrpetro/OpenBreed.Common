﻿using OpenBreed.Database.Xml.Items.Actions;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenBreed.Database.Xml.Tables
{
    public class XmlDbActionSetTableDef : XmlDbTableDef
    {
        public const string NAME = "ActionSets";

        [XmlIgnore]
        public override string Name => NAME;

        [XmlArray("Items"),
        XmlArrayItem("ActionSet", typeof(XmlDbActionSet))]
        public readonly List<XmlDbActionSet> Items = new List<XmlDbActionSet>();

    }
}