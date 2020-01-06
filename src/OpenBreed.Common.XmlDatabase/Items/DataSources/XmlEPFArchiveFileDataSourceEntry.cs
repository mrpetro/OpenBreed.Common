﻿using OpenBreed.Common.DataSources;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Items.DataSources
{
    [Serializable]
    [Description("EPF Archive Item"), Category("Appearance")]
    public class XmlEPFArchiveFileDataSourceEntry : XmlDataSourceEntry, IEPFArchiveDataSourceEntry
    {
        #region Public Properties

        [XmlAttribute]
        public string EntryName { get; set; }

        [XmlAttribute]
        public string ArchivePath { get; set; }

        #endregion Public Properties

        #region Public Methods

        public override IEntry Copy()
        {
            return new XmlEPFArchiveFileDataSourceEntry()
            {
                Id = this.Id,
                EntryName = this.EntryName,
                ArchivePath = this.ArchivePath
            };
        }

        #endregion Public Methods
    }
}