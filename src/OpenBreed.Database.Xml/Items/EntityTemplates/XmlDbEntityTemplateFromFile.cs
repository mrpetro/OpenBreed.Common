﻿using OpenBreed.Common;
using OpenBreed.Database.Interface.Items;
using OpenBreed.Database.Interface.Items.EntityTemplates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OpenBreed.Database.Xml.Items.EntityTemplates
{
    [Serializable]
    [Description("Entity template from file"), Category("Appearance")]
    public class XmlDbEntityTemplateFromFile : XmlDbEntityTemplate, IDbEntityTemplateFromFile
    {
        public override IDbEntry Copy()
        {
            return new XmlDbEntityTemplateFromFile()
            {
                Id = this.Id,
                Description = this.Description,
                DataRef = this.DataRef,
            };
        }
    }
}
