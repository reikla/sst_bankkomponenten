using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace BankingApplication
{
    class ServiceLocator
    {
        private static ServiceLocator instance = null;

        private CompositionContainer _compositionContainer;
        public static ServiceLocator Instance()
        {
            return instance ?? (instance = new ServiceLocator());
        }

        private ServiceLocator()
        {
            
        }

        public T GetService<T>()
        {
            if (_compositionContainer == null)
            {
                throw new CompositionException("Container not initialized");
            }
            return _compositionContainer.GetExportedValue<T>();
        }

        public void Init(CompositionContainer container)
        {
            _compositionContainer = container;
        }
    }
}
