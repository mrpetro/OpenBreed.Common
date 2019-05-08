﻿using OpenBreed.Common.Formats;
using OpenBreed.Common.Logging;
using System;
using System.Collections.Generic;

namespace OpenBreed.Common.Data
{
    public class DataProvider
    {
        #region Private Fields

        private Dictionary<string, object> _models = new Dictionary<string, object>();

        #endregion Private Fields

        #region Public Constructors

        public DataProvider(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;

            TileSets = new TileSetsDataProvider(this);
            SpriteSets = new SpriteSetsDataProvider(this);
            ActionSets = new ActionSetsDataProvider(this);
            Maps = new MapsDataProvider(this);
            Assets = new AssetsDataProvider(this);
            Sounds = new SoundsDataProvider(this);
            Images = new ImagesDataProvider(this);
            Palettes = new PalettesDataProvider(this);
            Texts = new TextsDataProvider(this);

            Initialize();
        }

        #endregion Public Constructors

        #region Public Properties

        public ActionSetsDataProvider ActionSets { get; }
        public AssetsDataProvider Assets { get; }
        public DataFormatMan FormatMan { get; } = new DataFormatMan();
        public ImagesDataProvider Images { get; }
        public MapsDataProvider Maps { get; }
        public PalettesDataProvider Palettes { get; }
        public TextsDataProvider Texts { get; }
        public SoundsDataProvider Sounds { get; }
        public SpriteSetsDataProvider SpriteSets { get; }
        public TileSetsDataProvider TileSets { get; }
        public IUnitOfWork UnitOfWork { get; }

        #endregion Public Properties

        #region Public Methods

        public object GetData(string id)
        {
            object data;

            if (_models.TryGetValue(id, out data))
                return data;

            var asset = Assets.GetAsset(id);
            data = asset.Load();
            _models.Add(id, data);

            return data;
        }

        public void Save()
        {
            SaveEx();

            Assets.Save();

            UnitOfWork.Save();
        }

        #endregion Public Methods

        #region Private Methods

        private void Initialize()
        {
            FormatMan.RegisterFormat("ABSE_MAP", new ABSEMAPFormat());
            FormatMan.RegisterFormat("ABHC_MAP", new ABHCMAPFormat());
            FormatMan.RegisterFormat("ABTA_MAP", new ABTAMAPFormat());
            FormatMan.RegisterFormat("ABTABLK", new ABTABLKFormat());
            FormatMan.RegisterFormat("ABTASPR", new ABTASPRFormat());
            FormatMan.RegisterFormat("ACBM_TILE_SET", new ACBMTileSetFormat());
            FormatMan.RegisterFormat("ACBM_IMAGE", new ACBMImageFormat());
            FormatMan.RegisterFormat("IFF_IMAGE", new IFFImageFormat());
            FormatMan.RegisterFormat("BINARY", new BinaryFormat());
            FormatMan.RegisterFormat("PCM_SOUND", new PCMSoundFormat());
        }
        private void SaveEx()
        {
            foreach (var item in _models)
            {
                var entryId = item.Key;
                var data = item.Value;

                var asset = Assets.GetAsset(entryId);

                try
                {
                    asset.Save(data);
                }
                catch (Exception ex)
                {
                    LogMan.Instance.Error($"Problems saving asset {asset.Id}, Reason: {ex.Message}");
                }
            }
        }

        #endregion Private Methods
    }
}