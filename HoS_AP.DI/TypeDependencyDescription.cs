namespace HoS_AP.DI
{
    internal class TypeDependencyDescription<TService, TImplementation> : DependencyDescription
        where TImplementation : TService
    {
        public TypeDependencyDescription() : base(typeof(TService), typeof(TImplementation))
        {
        }
    }
}