using IDI.Central.Common.Enums;
using IDI.Central.Domain.Modules.Retailing.AggregateRoots;

namespace IDI.Central.Domain.Modules.Retailing
{
    internal static class Extension
    {
        public static bool Valid(this Product product)
        {
            if (product == null)
                return false;

            return product.Enabled && product.OnShelf;
        }

        public static bool AllowModifyItem(this Order order)
        {
            if (order == null)
                return false;

            if (order.Status == OrderStatus.Pending)
                return true;

            return false;
        }
    }
}
