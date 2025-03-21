using System.Windows.Media.Imaging;

namespace Client.UI.Helpers.Specialization;

public class SpecializationToImageConverter : IValueConverter
{
    private static readonly string AppBase = "pack://application:,,,/Assets/Specializations/";

    private Dictionary<int, string> IdxToImage = new()
    {
        { 263, "talentbg-shaman-enhancement.jpg" },
        { 270, "talentbg-monk-mistweaver.jpg" },
        { 66, "talentbg-paladin-protection.jpg" },
        { 255, "talentbg-hunter-survival.jpg" },
        { 256, "talentbg-priest-discipline.jpg" },
        { 268, "talentbg-monk-brewmaster.jpg" },
        { 72, "talentbg-warrior-fury.jpg" },
        { 252, "talentbg-death-knight-unholy.jpg" },
        { 259, "talentbg-rogue-assassination.jpg" },
        { 264, "talentbg-druid-restoration.jpg" },
        { 64, "talentbg-mage-frost.jpg" },
        { 62, "talentbg-mage-arcane.jpg" },
        { 65, "talentbg-priest-holy.jpg" },
        { 70, "talentbg-paladin-retribution.jpg" },
        { 71, "talentbg-warrior-arms.jpg" },
        { 73, "talentbg-warrior-protection.jpg" },
        { 253, "talentbg-hunter-beast-mastery.jpg" },
        { 254, "talentbg-hunter-marksmanship.jpg" },
        { 258, "talentbg-priest-shadow.jpg" },
        { 261, "talentbg-rogue-subtlety.jpg" },
        { 103, "talentbg-druid-feral.jpg" },
        { 104, "talentbg-druid-guardian.jpg" },
        { 105, "talentbg-druid-restoration.jpg" },
        { 257, "talentbg-paladin-holy.jpg" },
        { 63, "talentbg-mage-fire.jpg" },
        { 250, "talentbg-death-knight-blood.jpg" },
        { 251, "talentbg-death-knight-frost.jpg" },
        { 260, "talentbg-rogue-outlaw.jpg" },
        { 262, "talentbg-shaman-elemental.jpg" },
        { 265, "talentbg-warlock-affliction.jpg" },
        { 266, "talentbg-warlock-demonology.jpg" },
        { 269, "talentbg-monk-windwalker.jpg" },
        { 102, "talentbg-druid-balance.jpg" },
        { 577, "talentbg-demon-hunter-havoc.jpg" },
        { 581, "talentbg-demon-hunter-vengeance.jpg" },
        { 1467, "talentbg-evoker-devastation.jpg" },
        { 1468, "talentbg-evoker-preservation.jpg" },
        { 1473, "talentbg-evoker-augmentation.jpg" }
    };

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is int idx)
        {
            if (IdxToImage.TryGetValue(idx, out string? img))
            {
                return new BitmapImage(new Uri(AppBase + img));
            }
        }

        return null;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}