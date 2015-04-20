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
            MealTimeOfferings = new Dictionary<MenuType, MenuOffering>();

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
            if (!MealTimeOfferings.ContainsKey(type_))
                MealTimeOfferings.Add(type_, mealOffering_);
        }

        public MenuOffering Get(MenuType type_)
        {
            if (!MealTimeOfferings.ContainsKey(type_))
                throw new ArgumentException("Meal type not found");

            return MealTimeOfferings[type_];
        }

        public MenuOffering GetMenuOffering(string menuType_)
        {
            var menu = char.ToUpper(menuType_[0]) + menuType_.Substring(1).ToLower();

            MenuType myMenu;
            var valid = Enum.TryParse(menu, out myMenu);

            if (!MealTimeOfferings.ContainsKey(myMenu)) throw new Exception("Menu Type doesn't exists");

            return MealTimeOfferings[myMenu];
        }
    }
}
