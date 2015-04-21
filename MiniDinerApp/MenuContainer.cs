using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniDinerApp
{
    public class MenuContainer
    {
         private static readonly object Locker = new object();
        private static volatile MenuContainer menuContainer = null;

        private static readonly IDictionary<MenuType, MenuOffering>
            MenuTimeOfferings = new Dictionary<MenuType, MenuOffering>();

        private MenuContainer()
        {
        }

        public static MenuContainer GetInstance()
        {
            if (menuContainer == null)
            {
                lock (Locker)
                {
                    if (menuContainer == null) menuContainer = new MenuContainer();
                }
            }
            return menuContainer;
        }

        public void Register(
            MenuType type_, MenuOffering mealOffering_)
        {
            if (!MenuTimeOfferings.ContainsKey(type_))
                MenuTimeOfferings.Add(type_, mealOffering_);
        }

        public MenuOffering Get(MenuType type_)
        {
            if (!MenuTimeOfferings.ContainsKey(type_))
                throw new ArgumentException("Meal type not found");

            return MenuTimeOfferings[type_];
        }

        public MenuOffering GetMenuOffering(string menuType_)
        {
            var menu = char.ToUpper(menuType_[0]) + menuType_.Substring(1).ToLower();

            MenuType myMenu;
            var valid = Enum.TryParse(menu, out myMenu);

            if (!MenuTimeOfferings.ContainsKey(myMenu)) throw new Exception("Menu Type doesn't exists");

            return MenuTimeOfferings[myMenu];
        }

        public string GetAllMenus()
        {
            var menuTimeList = MenuTimeOfferings.Keys.ToList();
            menuTimeList.Sort();

            var sb = new StringBuilder();

            foreach (var menuType in menuTimeList)
            {
                var newLine = Environment.NewLine;
                var menuTimeOfDay = menuType.ToString();
                var len = menuTimeOfDay.Length;
                var underScore = String.Concat(Enumerable.Repeat("=", len));
                sb.Append(menuTimeOfDay).Append(newLine).Append(underScore).Append(newLine);

                var offering = MenuTimeOfferings[menuType];
                var dishes = offering.GetDishes();
                foreach (var dish in dishes)
                {
                    sb.Append("\t").Append(dish).Append(newLine);
                }
            }

            return sb.ToString();
        }
    }
}
