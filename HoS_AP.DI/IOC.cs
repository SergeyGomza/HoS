using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DryIoc;
using HoS_AP.BLL.ServiceInterfaces;
using HoS_AP.BLL.Services;
using HoS_AP.BLL.Validation;
using HoS_AP.DAL.Dao;
using HoS_AP.DAL.DaoInterfaces;
using HoS_AP.Misc;

namespace HoS_AP.DI
{
    public sealed class InversionOfControlContainer : IInversionOfControlContainer
    {
        private IContainer container;
        private bool disposed;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling", Justification = "Depencency Injection Configuration")]
        public InversionOfControlContainer()
        {
            // We use DryIOC because it provides fantastic performance under MIT License
            // https://github.com/danielpalme/IocPerformance
            container = new Container(rules => rules.WithoutThrowOnRegisteringDisposableTransient());
            RegisterServices();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1506:AvoidExcessiveClassCoupling")]
        private void RegisterServices()
        {
            Register(new TypeDependencyDescription<IAccountDao, AccountDao>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<ICharacterDao, CharacterDao>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<IValidationMessageProvider, ValidationMessageProvider>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<IValidationService, ValidationService>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<IEncryptionService, EncryptionService>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<IAccountService, AccountService>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<ICharacterPresentationService, CharacterPresentationService>(), Reuse.Singleton);
            Register(new TypeDependencyDescription<ICharacterOperationService, CharacterOperationService>(), Reuse.Singleton);
        }

        private InversionOfControlContainer(IContainer container)
        {
            this.container = container;
        }

        public void RegisterControllers(IEnumerable<Type> controllerTypes)
        {
            if (controllerTypes == null)
            {
                return;
            }

            foreach (var t in controllerTypes)
            {
                container.Register(t, Reuse.Transient);
            }
        }

        private void Register(DependencyDescription dependencyDescription, IReuse reuse)
        {
            if (container.IsRegistered(dependencyDescription.ServiceType))
            {
                return;
            }

            if (dependencyDescription.ImplementationType != null)
            {
                container.Register(dependencyDescription.ServiceType, dependencyDescription.ImplementationType, reuse);
            }
        }

        internal void Replace(Container overriddenContainer)
        {
            container = overriddenContainer;
            RegisterServices();
        }

        public object Resolve(Type serviceType)
        {
            if (container.IsRegistered(serviceType))
            {
                return container.Resolve(serviceType);
            }

            return null;
        }

        IEnumerable<object> IInversionOfControlContainer.ResolveAll(Type serviceType)
        {
            var result = new Collection<object>();
            if (container.IsRegistered(serviceType))
            {
                result.Add(container.Resolve(serviceType));
            }

            return result;
        }

        public bool IsRegistered(Type serviceType)
        {
            return container.IsRegistered(serviceType);
        }

        IInversionOfControlContainer IInversionOfControlContainer.CreateScope()
        {
            return new InversionOfControlContainer(container.OpenScope());
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing && container != null)
            {
                container.Dispose();
            }

            disposed = true;
        }
    }
}
