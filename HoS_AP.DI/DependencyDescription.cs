using System;

namespace HoS_AP.DI
{
    internal abstract class DependencyDescription
    {
        private readonly Type serviceType;
        private readonly Type implementationType;

        protected DependencyDescription(Type serviceType, Type implementationType)
        {
            this.serviceType = serviceType;
            this.implementationType = implementationType;
        }

        public Type ServiceType
        {
            get { return serviceType; }
        }

        public Type ImplementationType
        {
            get { return implementationType; }
        }
    }
}