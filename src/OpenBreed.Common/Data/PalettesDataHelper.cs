﻿using OpenBreed.Common.Maps;
using OpenBreed.Common.Maps.Blocks;
using OpenBreed.Common.Palettes;
using OpenBreed.Common.Palettes.Builders;
using OpenBreed.Common.Palettes.Readers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Data
{
    internal class PalettesDataHelper
    {
        public static PaletteModel Create(MapPaletteBlock paletteBlock)
        {
            var paletteBuilder = PaletteBuilder.NewPaletteModel();
            paletteBuilder.SetName(paletteBlock.Name);
            paletteBuilder.CreateColors();
            for (int i = 0; i < paletteBlock.Value.Length; i++)
            {
                var colorData = paletteBlock.Value[i];
                paletteBuilder.SetColor(i, Color.FromArgb(255, colorData.R, colorData.G, colorData.B));
            }

            return paletteBuilder.Build();
        }

        public static PaletteModel FromMapModel(DataProvider provider, IPaletteFromMapEntry entry)
        {
            var mapModel = provider.GetData(entry.DataRef) as MapModel;

            if (mapModel == null)
                return null;

            var paletteBlock = mapModel.Blocks.OfType<MapPaletteBlock>().FirstOrDefault(item => item.Name == entry.BlockName);

            if (paletteBlock == null)
                return null;

            return Create(paletteBlock);
        }

        public static PaletteModel FromBinary(DataProvider provider, IPaletteFromBinaryEntry entry)
        {
            if (entry.DataRef == null)
                return null;

            var binaryModel = provider.GetData(entry.DataRef) as BinaryModel;

            if (binaryModel == null)
                return null;

            //Remember to set source stream to begining
            binaryModel.Stream.Seek(entry.DataStart, SeekOrigin.Begin);

            var paletteBuilder = PaletteBuilder.NewPaletteModel();
            var paletteReader = new PaletteReader(paletteBuilder, ToPaletteMode(entry.Mode), entry.ColorsNo);
            return paletteReader.Read(binaryModel.Stream);
        }

        public static PaletteReader.PaletteMode ToPaletteMode(PaletteMode mode)
        {
            switch (mode)
            {
                case PaletteMode.COLOR_16BIT:
                    return PaletteReader.PaletteMode.PALETTE_16BIT;
                case PaletteMode.COLOR_32BIT:
                    return PaletteReader.PaletteMode.PALETTE_32BIT;
                default:
                    throw new InvalidOperationException(mode.ToString());
            }
        }

    }
}