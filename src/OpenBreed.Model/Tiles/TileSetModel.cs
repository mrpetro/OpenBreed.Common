﻿using OpenBreed.Model.Palettes;
using System.Collections.Generic;
using System.Drawing;

namespace OpenBreed.Model.Tiles
{
    public enum TilePixelFormat
    {
        Indexed8bpp
    }

    public class TileSetModel
    {
        #region Internal Constructors

        internal TileSetModel(TileSetBuilder builder)
        {
            Tiles = builder.Tiles;
            TileSize = builder.TileSize;
            TilesNoX = builder.TilesNoX;
            TilesNoY = builder.TilesNoY;
            PixelFormat = builder.PixelFormat;
            Bitmap = builder.Bitmap;
        }

        #endregion Internal Constructors

        #region Public Properties

        public Bitmap Bitmap { get; }

        public TilePixelFormat PixelFormat { get; private set; }

        /// <summary>
        ///  Gets or sets an object that provides additional data context.
        /// </summary>
        public object Tag { get; set; }

        public List<TileModel> Tiles { get; }

        public int TileSize { get; }

        public int TilesNoX { get; }

        public int TilesNoY { get; }

        #endregion Public Properties

        #region Public Methods

        public Point GetIndexCoords(Point point)
        {
            return new Point(point.X / TileSize, point.Y / TileSize);
        }

        public Point GetSnapCoords(Point point)
        {
            int x = point.X / TileSize;
            int y = point.Y / TileSize;

            return new Point(x * TileSize, y * TileSize);
        }

        public List<int> GetTileIdList(Rectangle rectangle)
        {
            int left = rectangle.Left;
            int right = rectangle.Right;
            int top = rectangle.Top;
            int bottom = rectangle.Bottom;

            if (left < 0)
                left = 0;

            if (right > Bitmap.Width)
                right = Bitmap.Width;

            if (top < 0)
                top = 0;

            if (bottom > Bitmap.Height)
                bottom = Bitmap.Height;

            rectangle = new Rectangle(left, top, right - left, bottom - top);

            List<int> tileIdList = new List<int>();
            int xFrom = rectangle.Left / TileSize;
            int xTo = rectangle.Right / TileSize;
            int yFrom = rectangle.Top / TileSize;
            int yTo = rectangle.Bottom / TileSize;

            for (int xIndex = xFrom; xIndex < xTo; xIndex++)
            {
                for (int yIndex = yFrom; yIndex < yTo; yIndex++)
                {
                    int gfxId = xIndex + TilesNoX * yIndex;
                    tileIdList.Add(gfxId);
                }
            }

            return tileIdList;
        }

        #endregion Public Methods
    }
}