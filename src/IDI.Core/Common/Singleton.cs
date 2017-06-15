using System;

namespace IDI.Core.Common
{
    /// <summary>
    /// A base class for the singleton design pattern.
    /// </summary>
    /// <typeparam name="T">Class type of the singleton, must be sealed and constructor is private</typeparam>
    public abstract class Singleton<T> where T : class
    {
        #region Members

        /// <summary>
        /// Static instance. Needs to use lambda expression
        /// to construct an instance (since constructor is private).
        /// </summary>
        private static readonly Lazy<T> instance = new Lazy<T>(() => CreateInstance());

        #endregion

        #region Properties

        /// <summary>
        /// Gets the instance of this singleton.
        /// </summary>
        public static T Instance { get { return instance.Value; } }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an instance of T via reflection since T's constructor is expected to be private.
        /// </summary>
        /// <returns></returns>
        private static T CreateInstance()
        {
            return Activator.CreateInstance(typeof(T), true) as T;
        }

        #endregion
    }
}
