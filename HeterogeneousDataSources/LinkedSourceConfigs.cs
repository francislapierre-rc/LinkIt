using System;
using System.Collections.Generic;
using System.Linq;

namespace HeterogeneousDataSources
{
    public static class LinkedSourceConfigs
    {
        private static readonly Dictionary<Type, ILinkedSourceConfig> LinkedSourceConfigByType = new Dictionary<Type, ILinkedSourceConfig>();

        public static IGenericLinkedSourceConfig<TLinkedSource> GetConfigFor<TLinkedSource>()
        {
            return (IGenericLinkedSourceConfig<TLinkedSource>)GetConfigFor(typeof(TLinkedSource));
        }

        public static ILinkedSourceConfig GetConfigFor(Type linkedSourceType) {
            //Lazy init to minimize required configuration by the client.
            //stle: dangerous for multithreading
            if (!LinkedSourceConfigByType.ContainsKey(linkedSourceType)) {
                LinkedSourceConfigByType.Add(linkedSourceType, CreateLinkedSourceConfig(linkedSourceType));
            }

            return LinkedSourceConfigByType[linkedSourceType];
        }

        private static ILinkedSourceConfig CreateLinkedSourceConfig(Type linkedSourceType)
        {
            Type[] typeArgs ={
                linkedSourceType,
                GetLinkedSourceModelType(linkedSourceType)
            };

            Type ctorGenericType = typeof(LinkedSourceConfig<,>);
            Type ctorSpecificType = ctorGenericType.MakeGenericType(typeArgs);

            var ctor = ctorSpecificType.GetConstructors().Single();
            var uncasted = ctor.Invoke(new object[0]);
            return (ILinkedSourceConfig)uncasted;
        }

        private static Type GetLinkedSourceModelType(Type linkedSourceType)
        {
            var iLinkedSourceTypes = linkedSourceType.GetInterfaces()
                .Where(interfaceType =>
                    interfaceType.IsGenericType &&
                    interfaceType.GetGenericTypeDefinition() == typeof(ILinkedSource<>))
                .ToList();

            EnsureILinkedSourceIsImplementedOnceAndOnlyOnce(linkedSourceType, iLinkedSourceTypes);

            var iLinkedSourceType = iLinkedSourceTypes.Single();
            return iLinkedSourceType.GenericTypeArguments.Single();
        }

        private static void EnsureILinkedSourceIsImplementedOnceAndOnlyOnce(Type linkedSourceType, List<Type> iLinkedSourceTypes)
        {
            if (!iLinkedSourceTypes.Any())
            {
                throw new ArgumentException(
                    string.Format(
                        "{0} must implement ILinkedSource<>.",
                        linkedSourceType
                    ),
                    "TLinkedSource"
                );
            }

            if (iLinkedSourceTypes.Count > 1)
            {
                throw new ArgumentException(
                    string.Format(
                        "{0} must implement ILinkedSource<> only once.",
                        linkedSourceType
                    ),
                    "TLinkedSource"
                );
            }
        }
    }
}