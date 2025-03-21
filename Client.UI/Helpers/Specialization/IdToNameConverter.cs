namespace Client.UI.Helpers.Specialization;

public class IdToNameConverter : IValueConverter
{
    private Dictionary<int, string> IdToName = new()
    {
        { 263, " 增强" },
        { 270, " 织雾" },
        { 66, " 防护" },
        { 255, " 生存" },
        { 256, " 戒律" },
        { 268, " 酒仙" },
        { 72, " 狂怒" },
        { 252, " 邪恶" },
        { 259, " 奇袭" },
        { 264, " 恢复" },
        { 64, " 冰霜" },
        { 62, " 奥术" },
        { 65, " 神圣" },
        { 70, " 惩戒" },
        { 71, " 武器" },
        { 73, " 防护" },
        { 253, " 野兽控制" },
        { 254, " 射击" },
        { 258, " 暗影" },
        { 261, " 敏锐" },
        { 103, " 野性" },
        { 104, " 守护" },
        { 105, " 恢复" },
        { 267, " 毁灭" },
        { 257, " 神圣" },
        { 63, " 火焰" },
        { 250, " 鲜血" },
        { 251, " 冰霜" },
        { 260, " 狂徒" },
        { 262, " 元素" },
        { 265, " 痛苦" },
        { 266, " 恶魔学识" },
        { 269, " 踏风" },
        { 102, " 平衡" },
        { 577, " 浩劫" },
        { 581, " 复仇" },
        { 1467, " 湮灭" },
        { 1468, " 恩护" },
        { 1473, " 增辉" },
    };

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is not int idx ? "" : IdToName.GetValueOrDefault(idx, "");
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}