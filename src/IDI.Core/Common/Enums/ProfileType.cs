using System.ComponentModel;

namespace IDI.Core.Common.Enums
{
    public enum ProfileType
    {
        [Description("型号")]
        Model,
        [Description("规格")]
        Spec,
        [Description("容量")]
        Capacity,
        [Description("重量")]
        Weight,
        [Description("长")]
        Length,
        [Description("高")]
        Height,
        [Description("宽")]
        Width,
        [Description("尺寸")]
        Size,
        [Description("颜色")]
        Color,
        [Description("年份")]
        Year,
        [Description("描述")]
        Description
    }
}
