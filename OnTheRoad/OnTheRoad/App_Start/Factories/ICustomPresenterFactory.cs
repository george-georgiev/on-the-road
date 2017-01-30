using System;
using WebFormsMvp;

namespace OnTheRoad.App_Start.Factories
{
    public interface ICustomPresenterFactory
    {
        IPresenter GetPresenter(Type presenterType, Type viewType, IView viewInstance);
    }
}
