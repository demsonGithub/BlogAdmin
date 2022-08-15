using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Demkin.Blog.Utils.Help
{
    public class ListHelper
    {
        public static List<T> FormatListToTree<T>(List<T> sourceList, object pId, object pName)
        {
            List<T> targetList = new List<T>();
            var dType = typeof(T);

            foreach (var item in sourceList)
            {
                PropertyInfo propertyInfo = dType.GetProperty(pName.ToString());
                var result = propertyInfo.GetValue(item);
            }

            return targetList;
        }
    }
}