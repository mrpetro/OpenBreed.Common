﻿using OpenBreed.Database.Interface.Items;
using OpenBreed.Database.Interface.Items.Scripts;
using OpenBreed.Database.Xml.Items.Scripts;
using OpenBreed.Database.Xml.Tables;
using System;
using System.Collections.Generic;

namespace OpenBreed.Database.Xml.Repositories
{
    public class XmlScriptsRepository : XmlRepositoryBase<IScriptEntry>
    {
        #region Private Fields

        private readonly XmlDbScriptTableDef _table;

        #endregion Private Fields

        #region Public Constructors

        public XmlScriptsRepository(XmlDatabaseMan context) : base(context)
        {
            _table = context.GetTable<XmlDbScriptTableDef>();
        }

        #endregion Public Constructors

        #region Public Properties

        public override IEnumerable<IEntry> Entries { get { return _table.Items; } }

        public override IEnumerable<Type> EntryTypes
        {
            get
            {
                yield return typeof(XmlScriptEmbeddedEntry);
                yield return typeof(XmlScriptFromFileEntry);
            }
        }

        public override string Name { get { return "Scripts"; } }

        public override int Count => _table.Items.Count;

        #endregion Public Properties

        #region Protected Methods

        protected override IScriptEntry GetEntryWithIndex(int index)
        {
            return _table.Items[index];
        }

        protected override int GetIndexOf(IScriptEntry entry)
        {
            return _table.Items.IndexOf((XmlScriptEntry)entry);
        }

        protected override void ReplaceEntryWithIndex(int index, IScriptEntry newEntry)
        {
            _table.Items[index] = (XmlScriptEntry)newEntry;
        }

        public override void Add(IScriptEntry newEntry)
        {
            _table.Items.Add((XmlScriptEntry)newEntry);
        }

        #endregion Protected Methods
    }
}