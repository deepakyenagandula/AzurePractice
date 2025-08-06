using System;
using System.Collections;

namespace ConsoleApp1.MultiThreading;

public static class ExtensionMethods
{
    public static bool HasData(this object? obj)
    {
        if (obj == null)
        {
            return false;
        }

        if (obj is IEnumerable<object> enumarableObj)
        {
            return enumarableObj.Count() > 0;
        }

         if (obj is ICollection<object> collection)
        {
            return collection.Count() > 0;
        }
        return false;
    }
}
