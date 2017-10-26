namespace IDI.Central.Common.Enums
{
    public enum SaleStatus
    {
        /// <summary>
        /// 新建
        /// </summary>
        Created = 2,
        /// <summary>
        /// 待确认
        /// </summary>
        Pending = 4,
        /// <summary>
        /// 订单确认
        /// </summary>
        Confirmed = 8,
        /// <summary>
        /// 付款
        /// </summary>
        Paid = 16,
        /// <summary>
        /// 发货
        /// </summary>
        Shipped = 32,
        /// <summary>
        /// 收货
        /// </summary>
        Received = 64,
        /// <summary>
        /// 交易成功
        /// </summary>
        Traded = 128,
        /// <summary>
        /// 取消
        /// </summary>
        Cancelled = 1024
    }
}
