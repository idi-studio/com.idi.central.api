namespace IDI.Central.Common.Enums
{
    public enum OrderStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid = 1,
        /// <summary>
        /// 待确认
        /// </summary>
        Pending = 2,
        /// <summary>
        /// 已确认
        /// </summary>
        Confirmed = 4,
        /// <summary>
        /// 已付款
        /// </summary>
        Paid = 8,
        /// <summary>
        /// 已发货
        /// </summary>
        Shipped = 16,
        /// <summary>
        /// 已收货
        /// </summary>
        Received = 32,
        /// <summary>
        /// 交易成功
        /// </summary>
        Traded = 64
    }
}
