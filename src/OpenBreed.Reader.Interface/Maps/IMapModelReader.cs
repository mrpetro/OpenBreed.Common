﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using OpenBreed.Model.Maps;

namespace OpenBreed.Reader.Maps
{
    public interface IMapModelReader
    {
        MapModel Read(Stream stream);
    }
}
