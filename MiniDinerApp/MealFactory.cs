using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniDinerApp
{
    public class MealFactory<T>
    {
        private MealFactory() { }

        static readonly Dictionary<MenuType, Func<T>> Dict
             = new Dictionary<MenuType, Func<T>>();

        public static T Create(MenuType id_)
        {
            Func<T> constructor = null;
            if (Dict.TryGetValue(id_, out constructor))
                return constructor();

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(MenuType id_, Func<T> ctor_)
        {
            if (!Dict.ContainsKey(id_))
                Dict.Add(id_, ctor_);
        }
    }
}
