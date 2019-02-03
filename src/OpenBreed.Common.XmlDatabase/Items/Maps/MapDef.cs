﻿using OpenBreed.Common.XmlDatabase.Items.Sources;
using OpenBreed.Common.Formats;
using OpenBreed.Common.Maps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Items.Maps
{
    [Serializable]
    public class MapDef : DatabaseItemDef, IMapEntry
    {
        #region Public Properties

        [XmlIgnore]
        public IFormatEntry Format { get; set; }

        [XmlElement("Format")]
        public FormatDef FormatDef
        {
            get
            {
                return (FormatDef)Format;
            }

            set
            {
                Format = value;
            }
        }

        [XmlArray("PaletteRefs"),
        XmlArrayItem("PaletteRef", typeof(string))]
        public List<string> PaletteRefs { get; } = new List<string>();

        public string PropertySetRef { get; set; }
        public string AssetRef { get; set; }

        [XmlArray("SpriteSetRefs"),
        XmlArrayItem("SpriteSetRef", typeof(string))]
        public List<string> SpriteSetRefs { get; } = new List<string>();

        public string TileSetRef { get; set; }

        public override IEntry Copy()
        {
            throw new NotImplementedException();
        }

        #endregion Public Properties

    }
}