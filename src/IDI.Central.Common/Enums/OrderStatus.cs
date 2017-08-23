namespace IDI.Central.Common.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 1,
        /// <summary>
        /// 待付款
        /// </summary>
        Pending = 2,
        /// <summary>
        /// 已付款
        /// </summary>
        Paid = 4,
        /// <summary>
        /// 已发货
        /// </summary>
        Shipped = 8,
        /// <summary>
        /// 已收货
        /// </summary>
        Received = 16,
        /// <summary>
        /// 交易成功
        /// </summary>
        Traded = 32
    }
}
