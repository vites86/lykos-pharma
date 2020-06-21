using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Olga.Util
{
    public static class ArrayHelper
    {
        public static T[] RemoveItem<T>(this T[] arrayToUpdate, T itemToRemove)
        {
            if (arrayToUpdate == null)
                throw new ArgumentNullException("arrayToUpdate");

            T[] newArray = new T[arrayToUpdate.Length - 1];

            int i = 0;
            int j = 0;

            while (i < arrayToUpdate.Length)
            {
                if (!arrayToUpdate[i].Equals(itemToRemove))
                {
                    newArray[j] = arrayToUpdate[i];
                    j++;
                }

                i++;
            }

            return newArray;
        }
    }
}