﻿using System;
using System.Collections.Generic;

namespace OpenBreed.Common
{
    public class DataLoaderFactory : IDataLoaderFactory
    {
        #region Private Fields

        private readonly Dictionary<Type, Func<IDataLoader>> loaders = new Dictionary<Type, Func<IDataLoader>>();

        #endregion Private Fields

        #region Public Constructors

        public DataLoaderFactory()
        {
        }

        #endregion Public Constructors

        #region Public Methods

        public IDataLoader<TInterface> GetLoader<TInterface>()
        {
            if (loaders.TryGetValue(typeof(TInterface), out Func<IDataLoader> loaderInitializer))
                return (IDataLoader<TInterface>)loaderInitializer.Invoke();
            else
                throw new InvalidOperationException($"Loader for type '{typeof(TInterface)}' is not registered");
        }

        public void Register<TInterface>(Func<IDataLoader> dataLoaderInitializer)
        {
            loaders.Add(typeof(TInterface), dataLoaderInitializer);
        }

        #endregion Public Methods
    }
}