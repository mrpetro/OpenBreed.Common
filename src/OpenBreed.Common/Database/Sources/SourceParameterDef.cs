﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Common.Database.Sources
{
    public class SourceParameterDef
    {
        [XmlAttribute]
        public string Name { get; set; }

        [XmlAttribute]
        public string Type { get; set; }

        [XmlText]
        public string Value { get; set; }
    }
}
