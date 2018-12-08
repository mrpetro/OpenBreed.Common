﻿using OpenBreed.Common.Images.Builders;
using OpenBreed.Common.Readers;
using OpenBreed.Common.Tiles.Builders;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Images.Readers.ACBM
{
    public class ACBMImageReader
    {
        #region Public Fields

        private int _bitPlanesNo;
        private int _width;
        private int _height;
        private bool _readPalette;

        #endregion Public Fields

        #region Public Constructors

        public ACBMImageReader(ImageBuilder builder, int width, int height, int bitPlanesNo, bool readPalette)
        {
            Builder = builder;
            _width = width;
            _height = height;
            _readPalette = readPalette;

            _bitPlanesNo = bitPlanesNo;
        }

        #endregion Public Constructors

        #region Internal Properties

        internal ImageBuilder Builder { get; private set; }

        #endregion Internal Properties

        #region Public Methods

        public Image Read(Stream stream)
        {
            Builder.SetSize(_width, _height);
            Builder.SetPixelFormat(PixelFormat.Format8bppIndexed);

            var binReader = new BigEndianBinaryReader(stream);
            binReader.BaseStream.Position = 0;
            int bytes = _width * _height / 8;

            byte[] imageData = new byte[_width * _height];

            for (int i = 0; i < _bitPlanesNo; i++)
            {
                byte[] rawData = binReader.ReadBytes(bytes);
                rawData = rawData.Reverse().ToArray();

                if (rawData.Length == 0)
                    return null;

                var bitArray = new BitArray(rawData);

                for (int k = 0; k < _width * _height; k++)
                    imageData[k] += (byte)((bitArray[k] ? 1 : 0) << i);
            }

            imageData = imageData.Reverse().ToArray();

            if (_readPalette)
            {
                var colorsNo = 0;
                Color[] colors;

                if (_bitPlanesNo < 6)
                {
                    colorsNo = (int)Math.Pow(2, _bitPlanesNo);
                    colors = new Color[colorsNo];

                    for (int i = 0; i < colorsNo; i++)
                        colors[i] = From16Bit(binReader.ReadUInt16());
                }
                else if (_bitPlanesNo == 6)
                {
                    colorsNo = 64; // Extra Half Byte (EHB)
                    colors = new Color[colorsNo];

                    for (int i = 0; i < 32; i++)
                        colors[i] = From16Bit(binReader.ReadUInt16());

                    for (int i = 32; i < 64; i++)
                        colors[i] = Color.FromArgb(255, colors[i - 32].R / 2,  colors[i - 32].G / 2,  colors[i - 32].B / 2);
                }
                else if (_bitPlanesNo == 8)
                {
                    colorsNo = 256;
                    colors = new Color[colorsNo];

                    for (int i = 0; i < 256; i++)
                        colors[i] = From32Bit(binReader.ReadUInt32());
                }
                else
                    throw new NotImplementedException($"Not supported bit planes no: {_bitPlanesNo}");

                Builder.SetPalette(colors);
            }

            //Start building new tile
            Builder.SetData(imageData);
            return Builder.Build();
        }

        public static Color From32Bit(UInt32 value)
        {
            byte g = (byte)(((value & 0x000000FF) ));
            byte b = (byte)(((value & 0x0000FFFF) >> 8));
            byte r = (byte)(((value & 0x00FFFFFF) >> 16));
            return Color.FromArgb(255, r, g, b);
        }

        public static Color From16Bit(UInt16 value)
        {
            byte r = (byte)(((value & 0x0FFF) >> 8) * 17);
            byte g = (byte)(((value & 0x00FF) >> 4) * 17);
            byte b = (byte)(((value & 0x000F)) * 17);
            return Color.FromArgb(255, r, g, b);
        }


        #endregion Public Methods

    }
}
