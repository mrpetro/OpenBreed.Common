﻿using OpenBreed.Common.Formats;
using OpenBreed.Common.Palettes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Items.Palettes
{
    public class PaletteDef : DatabaseItemDef, IPaletteEntry
    {

        #region Public Properties

        public string AssetRef { get; set; }

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

        #endregion Public Properties
    }
}
