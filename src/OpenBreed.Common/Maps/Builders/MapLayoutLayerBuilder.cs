﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Maps.Builders
{
    public class MapLayoutLayerBuilder<T>
    {
        #region Internal Fields

        internal T[] Cells = null;
        internal string Name;
        internal Size Size;

        #endregion Internal Fields

        #region Public Methods

        public static MapLayoutLayerBuilder<T> NewMapLayoutLayerModel()
        {
            return new MapLayoutLayerBuilder<T>();
        }

        public MapLayerModel<T> Build()
        {
            return new MapLayerModel<T>(this);
        }

        public MapLayoutLayerBuilder<T> SetCell(int index, T value)
        {
            Cells[index] = value;

            return this;
        }

        public MapLayoutLayerBuilder<T> SetName(string name)
        {
            Name = name;

            return this;
        }

        public MapLayoutLayerBuilder<T> SetSize(int sizeX, int sizeY)
        {
            Size = new Size(sizeX, sizeY);
            Cells = new T[sizeX * sizeY];

            return this;
        }

        #endregion Public Methods
    }
}