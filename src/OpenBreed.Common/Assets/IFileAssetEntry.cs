﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Assets
{
    public interface IFileAssetEntry : IAssetEntry
    {
        string FilePath { get; set; }
    }
}