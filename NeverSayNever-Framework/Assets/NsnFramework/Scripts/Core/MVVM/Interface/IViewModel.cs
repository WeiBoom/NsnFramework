using System;
using System.Linq.Expressions;

namespace Nsn.MVVM
{
    public interface IViewModel : System.IDisposable
    {
        void NotifyPropertyChanged(string propertyName);
        void NotifyPropertyChanged<T>(Expression<Func<T>> memberExpr);
    }
}