﻿using OpenBreed.Common.Tools;
using OpenBreed.Model.Sprites;
using OpenBreed.Reader.Sprites;
using System;
using System.IO;

namespace OpenBreed.Reader.Legacy.Sprites.SPR
{
    public class SPRReader : ISpriteSetReader
    {
        #region Public Constructors

        public SPRReader(SpriteSetBuilder builder)
        {
            Builder = builder;
        }

        #endregion Public Constructors

        #region Internal Properties

        internal SpriteSetBuilder Builder { get; private set; }

        #endregion Internal Properties

        #region Public Methods

        public SpriteSetModel Read(Stream stream)
        {
            //We dont want to close the stream here so reader is not being used inside of 'using' statement
            //BigEndianBinaryReader binReader = new BigEndianBinaryReader(stream);
            BinaryReader binReader = new BinaryReader(stream);

            var spritesNo = binReader.ReadInt16();

            for (int i = 0; i < spritesNo; i++)
                ReadSprite(binReader, i);

            return Builder.Build();
        }

        #endregion Public Methods

        #region Private Methods

        private void ReadSprite(BinaryReader binReader, int index)
        {
            //Start building new sprite
            var spriteBuilder = SpriteBuilder.NewSprite();

            spriteBuilder.SetIndex(index);

            //Read sprite header data(width, height and sprite bitmap data offset)
            int width = binReader.ReadInt16();
            int height = binReader.ReadInt16();
            var offset = binReader.ReadUInt16();

            //Remember sprites headers data position
            long currentHeadPos = binReader.BaseStream.Position;
            //Jump with reader to sprite data
            binReader.BaseStream.Seek(offset, SeekOrigin.Begin);
            //Read the sprite bitmap data
            ReadSpriteBitmap(binReader, spriteBuilder, width, height);
            //Jump back to sprite headers data
            binReader.BaseStream.Seek(currentHeadPos, SeekOrigin.Begin);

            //Add new sprite to sprite set being build
            Builder.AddSprite(spriteBuilder.Build());
        }

        private void ReadSpriteBitmap(BinaryReader binReader, SpriteBuilder spriteBuilder, int width, int height)
        {
            spriteBuilder.SetSize(width, height);

            byte[] spriteData = new byte[width * height];

            while (true)
            {
                byte lineNo = binReader.ReadByte();

                if (lineNo == 0xFF)
                    break;

                byte lineStart = binReader.ReadByte();
                byte lineLength = binReader.ReadByte();
                var lineBytes = binReader.ReadBytes(lineLength);

                for (byte i = 0; i < lineLength; i++)
                    spriteData[(lineStart + i) * width + lineNo] = lineBytes[i];
            }

            spriteBuilder.SetData(spriteData);
        }

        #endregion Private Methods
    }
}