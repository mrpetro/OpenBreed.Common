﻿using OpenBreed.Common.Sounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBreed.Common.Data
{
    public class SoundsDataProvider
    {
        #region Public Constructors

        public SoundsDataProvider(DataProvider provider)
        {
            Provider = provider;
        }

        #endregion Public Constructors

        #region Public Properties

        public DataProvider Provider { get; }

        #endregion Public Properties

        public SoundModel GetSound(string id)
        {
            var entry = Provider.UnitOfWork.GetRepository<ISoundEntry>().GetById(id);
            if (entry == null)
                throw new Exception("Sound error: " + id);

            if (entry.DataRef == null)
                return null;

            return Provider.GetData(entry.DataRef) as SoundModel;
        }

    }
}
