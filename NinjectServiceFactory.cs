using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;

public class NinjectServiceFactory
{
    private IKernel _kernel;

    [Inject]
    public NinjectServiceFactory(IKernel kernel)
    {
        _kernel = kernel;
    }

    public T GetService<T>()
    {
        return (T)_kernel.GetService(typeof(T));
    }
}

