﻿using OpenBreed.Database.Xml.Items.Animations;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace OpenBreed.Database.Xml.Tables
{
    public class XmlDbAnimationTableDef : XmlDbTableDef
    {
        public const string NAME = "Animations";

        [XmlIgnore]
        public override string Name => NAME;


        [XmlArray("Items"),
        XmlArrayItem("Animation", typeof(XmlAnimationEntry))]
        public readonly List<XmlAnimationEntry> Items = new List<XmlAnimationEntry>();
    }
}