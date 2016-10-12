﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TedEnergy.Web.API
{
    public static class DataObjectTypeCaster<T>
    {
        public static bool TryParse(object inDataObject, out T convertedObject)
        {
            try
            {
                if (inDataObject.GetType() == typeof(T))
                {
                    convertedObject = (T)inDataObject;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            convertedObject = default(T);
            return false;

        }

        public static T Parse(object inDataObject)
        {
            try
            {
                if (inDataObject.GetType() == typeof(T))
                {
                    return (T)inDataObject;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return default(T);
        }
    }
}
