using System.Collections.Generic;
using System;

namespace Engine.DI
{
    internal static class SingletonPovider<T>
    {
        internal static T value = default(T);
    }

    internal static class ObjectsPovider<TType>
    {
        internal static readonly List<TType> Values = new List<TType>();
    }

    public static class DIContainer
    {
        /// <summary>
        /// Get the last value Registered as a singleton of type TType.
        /// </summary>
        public static TType GetAsSingle<TType>()
        {
            return SingletonPovider<TType>.value;
        }

        /// <summary>
        /// Here we register just one value, Always override the value to the last VALUE added.
        /// </summary>
        /// <typeparam name="T"> The Data Type that we will saved. </typeparam>
        public static void RegisterAsSingle<T>(T value)
        {
            SingletonPovider<T>.value = value ?? throw new NullReferenceException("eventObject has a null value!...");
        }

        /// <summary>
        /// Reset the value to the default value.
        /// </summary>
        public static void ResetSingleton<T>()
        {
            SingletonPovider<T>.value = default(T);
        }

        /// <summary>
        /// Bind all values of type TType and return it as an array of TType.
        /// </summary>
        /// <typeparam name="T"> The Data Type that we will searching. </typeparam>
        public static TType[] Bind<TType>()
        {
            return ObjectsPovider<TType>.Values.ToArray();
        }

        /// <summary>
        /// Register the value in the list with the other values of the same type (TType). if the value already exist, It will not added.
        /// </summary>
        public static void Register<TType>(TType value)
        {
            if (ObjectsPovider<TType>.Values.Contains(value)) return;

            ObjectsPovider<TType>.Values.Add(value);
        }

        /// <summary>
        /// Register range of values in the list with the other values with the same type (TType).
        /// </summary>
        public static void RegisterRange<TType>(IEnumerable<TType> collection)
        {
            if (collection == null) throw new ArgumentNullException();

            ObjectsPovider<TType>.Values.AddRange(collection);
        }

        /// <summary>
        /// Clear all values of type (TType).
        /// </summary>
        public static void Clear<TType>()
        {
            ObjectsPovider<TType>.Values.Clear();
        }

        /// <returns> Return the first value not null. If there is no value with non null exist we return the default value. </returns>
        public static TSource AsSingleton<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null) throw new ArgumentNullException();

            IList<TSource> list = source as IList<TSource>;
            if (list != null)
            {
                if (list.Count > 0)
                    foreach (TSource value in list)
                    {
                        if (value != null) return value;
                    }
            }
            else
            {
                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext() && e.Current != null) return e.Current;
                }
            }

            return default(TSource);
        }
    }
}
