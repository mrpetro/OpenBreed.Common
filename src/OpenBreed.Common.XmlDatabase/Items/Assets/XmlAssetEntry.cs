﻿using OpenBreed.Common.Assets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace OpenBreed.Common.XmlDatabase.Items.Assets
{
    [Serializable]
    public abstract class XmlAssetEntry : XmlDbEntry, IAssetEntry
    {

    }
}