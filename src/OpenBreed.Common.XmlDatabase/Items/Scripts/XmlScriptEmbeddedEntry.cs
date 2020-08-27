﻿using OpenBreed.Common.Palettes;
using OpenBreed.Common.Texts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Items.Texts
{
    [Serializable]
    [Description("Script embedded"), Category("Appearance")]
    public class XmlScriptEmbeddedEntry : XmlScriptEntry, IScriptEmbeddedEntry
    {
        #region Public Properties

        [XmlElement("Script")]
        public string Script { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override IEntry Copy()
        {
            return new XmlScriptEmbeddedEntry()
            {
                Id = this.Id,
                Description = this.Description,
                DataRef = this.DataRef,
                Script = this.Script
            };
        }

        #endregion Public Methods
    }
}
