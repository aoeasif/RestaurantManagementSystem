using System.Collections.Generic;

namespace Res
{
    internal class IBindingList<T>
    {
        private List<Order> orderList;

        public IBindingList(List<Order> orderList)
        {
            this.orderList = orderList;
        }
    }
}