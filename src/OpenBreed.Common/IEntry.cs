﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common
{
    public interface IEntry
    {
       long Id { get; }
       string Name { get; }
    }
}