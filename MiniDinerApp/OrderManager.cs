using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiniDinerApp
{
    public class OrderManager
    {
        public string DinerName { get; private set; }

        public OrderManager(string dinerName_)
        {
            DinerName = dinerName_;

            const int OneOrder = 1;
            const int InfinteOrders = int.MaxValue;

            // register types of meals per time
            MenuFactory<MenuOffering>.Register(MenuType.Morning, () => new MenuOffering(DishType.Entree, "eggs", OneOrder,
                                                                                        DishType.Side, "Toast", OneOrder,
                                                                                        DishType.Drink, "coffee", InfinteOrders));
            MenuFactory<MenuOffering>.Register(MenuType.Night, () => new MenuOffering(DishType.Entree, "steak", OneOrder,
                                                                                      DishType.Side, "potato", InfinteOrders,
                                                                                      DishType.Drink, "wine", OneOrder,
                                                                                      DishType.Dessert, "cake",  OneOrder));

            // create types of offerings
            var morningOfferings = MenuFactory<MenuOffering>.Create(MenuType.Morning);
            var nightOfferings = MenuFactory<MenuOffering>.Create(MenuType.Night);

            // add to container singleton
            MenuContainer.GetInstance().Register(MenuType.Morning, morningOfferings);
            MenuContainer.GetInstance().Register(MenuType.Night, nightOfferings);
        }

        public string Process(string input_)
        {
            if (string.IsNullOrEmpty(input_)) throw new Exception("input is empty");

            // parse
            string[] parts = input_.Split(',');
            var menuType = parts[0].Trim();
            var offering = MenuContainer.GetInstance().GetMenuOffering(menuType);

            IDictionary<int, int> orderSizeAllowed = new Dictionary<int, int>();
            IDictionary<int, int> dishOrders = new Dictionary<int, int>();
            IDictionary<int, int> dishNotValid = new Dictionary<int, int>();

            // collect dish orders
            for (int i = 1; i < parts.Length; ++i)
            {
                var dish = parts[i].Trim();
                int nDish;
                bool isNumeric = int.TryParse(dish, out nDish);

                if (!isNumeric) throw new Exception("Dish Type is not numeric");

                try
                {
                    var dishNameAndOrders = offering.GetOffering(nDish);

                    if (!orderSizeAllowed.ContainsKey(nDish)) orderSizeAllowed.Add(nDish, dishNameAndOrders.Item2);

                    if (!dishOrders.ContainsKey(nDish)) dishOrders.Add(nDish, 1);
                    else dishOrders[nDish]++;
                }
                catch (Exception)
                {
                    if (!dishNotValid.ContainsKey(nDish)) dishNotValid.Add(nDish, 1);
                    else dishNotValid[nDish]++;
                }
            }

            var sb = new StringBuilder();

            var dishOrderList = dishOrders.Keys.ToList();
            dishOrderList.Sort();

            bool errorFound = false;
            foreach (var dishOrdered in dishOrderList)
            {
                var dishNameAndOrders = offering.GetOffering(dishOrdered);
                sb.Append(dishNameAndOrders.Item1);

                if (dishOrders[dishOrdered] > dishNameAndOrders.Item2)
                {
                    sb.Append(", error");
                    errorFound = true;
                    break;
                }

                if (dishOrders[dishOrdered] > 1)
                {
                    sb.Append(string.Format("(x{0})", dishOrders[dishOrdered]));
                }

                sb.Append(", ");
            }

            if (!errorFound)
            {
                if (dishNotValid.Count > 0) sb.Append("error");
            }

            string returnOutput = sb.ToString();

            // remove ', ' if any
            var trimEnd = returnOutput.TrimEnd(' ');
            return trimEnd.TrimEnd(',');
        }

        public string GetAllMenus()
        {
            return MenuContainer.GetInstance().GetAllMenus(); ;
        }
    }
}
