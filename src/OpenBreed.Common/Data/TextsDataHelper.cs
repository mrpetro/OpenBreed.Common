﻿using OpenBreed.Model.Maps;
using OpenBreed.Model.Maps.Blocks;
using OpenBreed.Model.Texts;
using OpenBreed.Database.Interface.Items.Texts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Data
{
    internal class TextsDataHelper
    {
        public static TextModel Create(MapTextBlock textBlock)
        {
            var builder = TextBuilder.NewTextModel();
            builder.SetName(textBlock.Name);
            builder.SetText(textBlock.Value);
            return builder.Build();
        }

        public static TextModel FromMapModel(IDataProvider dataProvider, ITextFromMapEntry textData)
        {
            var mapModel = dataProvider.GetData<MapModel>(textData.DataRef);

            if (mapModel == null)
                return null;

            var textBlock = mapModel.Blocks.OfType<MapTextBlock>().FirstOrDefault(item => item.Name == textData.BlockName);

            if (textBlock == null)
                return null;

            return Create(textBlock);
        }

        public static TextModel FromBinary(IDataProvider dataProvider, ITextEmbeddedEntry textEntry)
        {
            var builder = TextBuilder.NewTextModel();
            builder.SetText(textEntry.Text);
            return builder.Build();
        }
    }
}
