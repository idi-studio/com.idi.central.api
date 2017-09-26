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
        /// 新增
        /// </summary>
        Add = 4,
        /// <summary>
        /// 修改
        /// </summary>
        Modify = 8,
        /// <summary>
        /// 删除
        /// </summary>
        Remove = 16,
        /// <summary>
        /// 上传
        /// </summary>
        Upload = 32
    }
}
