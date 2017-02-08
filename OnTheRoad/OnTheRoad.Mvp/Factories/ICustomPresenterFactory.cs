using System;
using WebFormsMvp;

namespace OnTheRoad.Mvp.Factories
{
    public interface ICustomPresenterFactory
    {
        IPresenter GetPresenter(Type presenterType, Type viewType, IView viewInstance);
    }
}
