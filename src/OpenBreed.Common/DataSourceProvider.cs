﻿using EPF;
using OpenBreed.Common.Sources;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common
{
    public delegate string ExpandVariablesDelegate(string text);

    public class DataSourceProvider
    {
        #region Private Fields

        private readonly Dictionary<string, SourceBase> _openedSources = new Dictionary<string, SourceBase>();
        private Dictionary<string, EPFArchive> _openedArchives = new Dictionary<string, EPFArchive>();

        #endregion Private Fields

        #region Public Constructors

        public DataSourceProvider(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        #endregion Public Constructors

        #region Public Properties

        public static ExpandVariablesDelegate ExpandVariables { get; set; }
        public IUnitOfWork UnitOfWork { get; }

        #endregion Public Properties

        #region Public Methods

        public SourceBase GetAsset(string name)
        {
            SourceBase source = null;
            if (_openedSources.TryGetValue(name, out source))
                return source;

            var entry = UnitOfWork.GetRepository<ISourceEntry>().GetByName(name);
            if (entry == null)
                throw new Exception($"Source error: {name}" );

            source = CreateAsset(entry);

            return source;
        }

        #endregion Public Methods

        #region Internal Methods

        internal void CloseAll()
        {
            foreach (var openedArchive in _openedArchives)
                openedArchive.Value.Dispose();

            _openedArchives.Clear();
        }

        internal EPFArchive GetArchive(string archivePath)
        {
            string normalizedPath = OpenBreed.Common.Tools.GetNormalizedPath(archivePath);

            EPFArchive archive = null;
            if (!_openedArchives.TryGetValue(normalizedPath, out archive))
            {
                File.Copy(normalizedPath, normalizedPath + ".bkp", true);
                archive = EPFArchive.ToExtract(File.Open(normalizedPath, FileMode.Open), true);
                _openedArchives.Add(normalizedPath, archive);
            }

            return archive;
        }

        internal void LockSource(SourceBase source)
        {
            _openedSources.Add(source.Name, source);
        }

        internal void ReleaseSource(SourceBase source)
        {
            _openedSources.Remove(source.Name);
        }

        #endregion Internal Methods

        #region Private Methods

        private SourceBase CreateDirectoryFileSource(IDirectoryFileSourceEntry source)
        {
            return new DirectoryFileSource(this, source.DirectoryPath, source.Name);
        }

        private SourceBase CreateEPFArchiveSource(IEPFArchiveSourceEntry source)
        {
            return new EPFArchiveFileSource(this, source.ArchivePath, source.Name);
        }

        private SourceBase CreateAsset(ISourceEntry source)
        {
            if (source is IDirectoryFileSourceEntry)
                return CreateDirectoryFileSource((IDirectoryFileSourceEntry)source);
            else if (source is IEPFArchiveSourceEntry)
                return CreateEPFArchiveSource((IEPFArchiveSourceEntry)source);
            else
                throw new NotImplementedException("Unknown sourceDef");
        }

        #endregion Private Methods
    }
}
