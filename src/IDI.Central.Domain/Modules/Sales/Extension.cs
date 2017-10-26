using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Sales.AggregateRoots;

namespace IDI.Central.Domain.Modules.Sales
{
    internal static class Extension
    {
        public static bool AllowModifyItem(this Order order)
        {
            if (order == null)
                return false;

            if (order.Status == SaleStatus.Pending)
                return true;

            return false;
        }

        public static bool HasItems(this Order order)
        {
            if (order == null)
                return false;

            return order.Items.Count > 0;
        }

        public static bool HasCustomer(this Order order)
        {
            if (order == null)
                return false;

            return order.CustomerId.HasValue && order.Customer != null;
        }
    }
}
