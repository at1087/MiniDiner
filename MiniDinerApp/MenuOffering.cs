using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniDinerApp
{
    public class MenuOffering
    {
        private readonly IDictionary<DishType, string> _offerings;
        private readonly IDictionary<DishType, int> _maxOrders;

        public MenuOffering(params object[] args_)
        {
            _offerings = new Dictionary<DishType, string>();
            _maxOrders = new Dictionary<DishType, int>();

            var len = args_.Length;
            var i = 0;

            while (i < len)
            {
                var dishType = (DishType)args_[i++];
                if (i==len) throw new ArgumentNullException();

                if (_offerings.ContainsKey(dishType)) throw new ArgumentException("Duplicate Dish Types");

                var dishName = (string)args_[i++];
                var dishNameExists = _offerings.Any(o_ => (o_.Value != null && o_.Value == dishName));

                if (dishNameExists) throw new ArgumentException("Duplicate Dish Names");

                _offerings.Add(dishType, dishName);

                var maxOrders = (int)args_[i++];
                _maxOrders.Add(dishType, maxOrders);
            }
        }

        public virtual Tuple<string, int> GetOffering(DishType dishType_)
        {
            if (!_offerings.ContainsKey(dishType_)) return new Tuple<string, int>("error", 0);

            return new Tuple<string, int>(_offerings[dishType_], _maxOrders[dishType_]); ;
        }

        public virtual Tuple<string, int> GetOffering(int dishNo_)
        {
            var dishType_ = (DishType)dishNo_;

            if (!_offerings.ContainsKey(dishType_)) throw new ArgumentException("Dish Type does not exist");

            return new Tuple<string, int>(_offerings[dishType_], _maxOrders[dishType_]); ;
        }
    }
}
