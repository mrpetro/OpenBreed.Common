﻿using EPF;
using OpenBreed.Common.Assets;
using OpenBreed.Common.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Data
{
    public delegate string ExpandVariablesDelegate(string text);

    public class AssetsDataProvider
    {

        #region Private Fields

        private readonly Dictionary<string, AssetBase> _openedAssets = new Dictionary<string, AssetBase>();
        private Dictionary<string, EPFArchive> _openedArchives = new Dictionary<string, EPFArchive>();

        #endregion Private Fields

        #region Public Constructors

        public AssetsDataProvider(DataProvider dataProvider)
        {
            DataProvider = dataProvider;
        }

        #endregion Public Constructors

        #region Public Properties

        public static ExpandVariablesDelegate ExpandVariables { get; set; }

        public DataProvider DataProvider { get; }

        #endregion Public Properties

        #region Public Methods

        public AssetBase GetAsset(string name)
        {
            AssetBase asset = null;
            if (_openedAssets.TryGetValue(name, out asset))
                return asset;

            var entry = DataProvider.UnitOfWork.GetRepository<IAssetEntry>().GetById(name);
            if (entry == null)
                throw new Exception($"Asset error: {name}");

            asset = CreateAsset(entry);

            return asset;
        }

        #endregion Public Methods

        #region Internal Methods

        internal void CloseAll()
        {
            Save();

            foreach (var openedArchive in _openedArchives)
                openedArchive.Value.Dispose();

            _openedArchives.Clear();
        }

        internal EPFArchive GetArchive(string archivePath)
        {
            string normalizedPath = IOHelper.GetNormalizedPath(archivePath);

            EPFArchive archive = null;
            if (!_openedArchives.TryGetValue(normalizedPath, out archive))
            {
                File.Copy(normalizedPath, normalizedPath + ".bkp", true);
                archive = EPFArchive.ToUpdate(File.Open(normalizedPath, FileMode.Open), true);
                _openedArchives.Add(normalizedPath, archive);
            }

            return archive;
        }

        internal void LockSource(AssetBase source)
        {
            _openedAssets.Add(source.Id, source);
        }

        internal void ReleaseSource(AssetBase source)
        {
            _openedAssets.Remove(source.Id);
        }

        internal void Save()
        {
            foreach (var openedArchive in _openedArchives)
            {
                //if (openedArchive.Value.IsModified)
                    openedArchive.Value.Save();
            }
        }

        #endregion Internal Methods

        #region Private Methods

        private AssetBase CreateAsset(IAssetEntry asset)
        {
            if (asset is IFileAssetEntry)
                return CreateFileAsset((IFileAssetEntry)asset);
            else if (asset is IEPFArchiveAssetEntry)
                return CreateEPFArchiveAsset((IEPFArchiveAssetEntry)asset);
            else
                throw new NotImplementedException("Unknown sourceDef");
        }

        private AssetBase CreateEPFArchiveAsset(IEPFArchiveAssetEntry asset)
        {
            var formatType = DataProvider.FormatMan.GetFormatType(asset.Format.Name);
            if (formatType == null)
                throw new Exception($"Unknown format {asset.Format.Name}");

            return new EPFArchiveFileAsset(this, asset.Id, formatType, asset.Format.Parameters, asset.ArchivePath, asset.EntryName);
        }

        private AssetBase CreateFileAsset(IFileAssetEntry asset)
        {
            var formatType = DataProvider.FormatMan.GetFormatType(asset.Format.Name);
            if (formatType == null)
                throw new Exception($"Unknown format {asset.Format.Name}");

            return new FileAsset(this, asset.Id, formatType, asset.Format.Parameters, asset.FilePath);
        }

        #endregion Private Methods
    }
}
