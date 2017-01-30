using System;
using WebFormsMvp;
using WebFormsMvp.Binder;

namespace OnTheRoad.App_Start.Factories
{
    public class CustomWebFormsMvpPresenterFactory :IPresenterFactory
    {
        private readonly ICustomPresenterFactory presenterFactory;

        public CustomWebFormsMvpPresenterFactory(ICustomPresenterFactory presenterFactory)
        {
            if (presenterFactory == null)
            {
                throw new ArgumentNullException(nameof(presenterFactory));
            }

            this.presenterFactory = presenterFactory;
        }

        public IPresenter Create(Type presenterType, Type viewType, IView viewInstance)
        {
            if (presenterType == null)
            {
                throw new ArgumentNullException(nameof(presenterType));
            }

            var presenterInstance = this.presenterFactory.GetPresenter(presenterType, viewType, viewInstance);

            return presenterInstance;
        }

        /// <summary>
        /// Ignore this, 
        /// Lifetime managerment delegated to Ninject.
        /// </summary>
        /// <param name="presenter"></param>
        public void Release(IPresenter presenter)
        {
            //var disposablePresenter = presenter as IDisposable;
            //if (disposablePresenter != null)
            //{
            //    disposablePresenter.Dispose();
            //}
        }
    }
}