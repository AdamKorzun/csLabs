using System;
using System.Collections.Generic;
using System.Text;

namespace lab3
{
    public interface IParsable
    {
        T GetConfig<T>();
    }
}
