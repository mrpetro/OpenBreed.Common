﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Texts
{
    public interface ITextFromMapEntry : ITextEntry
    {
        string BlockName { get; set; }
    }
}