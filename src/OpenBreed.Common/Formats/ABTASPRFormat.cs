﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenBreed.Common.DataSources;
using OpenBreed.Database.Interface.Items.Assets;
using OpenBreed.Model.Sprites;
using OpenBreed.Reader.Legacy.Sprites.SPR;

namespace OpenBreed.Common.Formats
{
    public class ABTASPRFormat : IDataFormatType
    {
        public ABTASPRFormat()
        {
        }

        public object Load(DataSourceBase ds, List<FormatParameter> parameters)
        {
            //Remember to set source stream to begining
            ds.Stream.Seek(0, SeekOrigin.Begin);

            var spriteSetBuilder = SpriteSetBuilder.NewSpriteSet();
            //spriteSetBuilder.SetSource(source);
            var sprReader = new SPRReader(spriteSetBuilder);
            return sprReader.Read(ds.Stream);
        }

        public void Save(DataSourceBase ds, object model, List<FormatParameter> parameters)
        {
            throw new NotImplementedException("ABTASPR Write");
        }
    }
}
