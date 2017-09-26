using System;

namespace IDI.Core.Common.Enums
{
    [Flags]
    public enum PermissionType
    {
        /// <summary>
        /// 查询
        /// </summary>
        Query = 2,
        /// <summary>
        /// 读取
        /// </summary>
        Read = 4,
        /// <summary>
        /// 新增
        /// </summary>
        Add = 8,
        /// <summary>
        /// 修改
        /// </summary>
        Modify = 16,
        /// <summary>
        /// 删除
        /// </summary>
        Remove = 32,
        /// <summary>
        /// 上传
        /// </summary>
        Upload = 64,
        /// <summary>
        /// 审核
        /// </summary>
        Verify = 128,
        /// <summary>
        /// 支付
        /// </summary>
        Paid = 256
    }
}
