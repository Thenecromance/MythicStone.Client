using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Client.UI.Views.Components.PlayerComp;

/// <summary>
/// TitleBarComp.xaml 的交互逻辑
/// </summary>
public partial class TitleBarComp : UserControl
{
    #region PlayerName

    public static readonly DependencyProperty PlayerNameProperty = DependencyProperty.Register(
        name: nameof(PlayerName),
        propertyType: typeof(string),
        ownerType: typeof(TitleBarComp),
        new FrameworkPropertyMetadata(
            defaultValue: "未知",
            flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        ));

    public string? PlayerName
    {
        get => (string?)GetValue(PlayerNameProperty);
        set => SetValue(PlayerNameProperty, value);
    }

    #endregion

    #region RealmName

    public static readonly DependencyProperty RealmNameProperty = DependencyProperty.Register(
        name: nameof(RealmName),
        propertyType: typeof(string),
        ownerType: typeof(TitleBarComp),
        new FrameworkPropertyMetadata(
            defaultValue: "未知",
            flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
        ));


    public string? RealmName
    {
        get => (string?)GetValue(PlayerNameProperty);
        set => SetValue(PlayerNameProperty, value);
    }

    #endregion

    //#region BackGroundBrush

    //public static readonly DependencyProperty TitleBackGroundProperty = DependencyProperty.Register(
    //    name: nameof(TitleBackGround),
    //    propertyType: typeof(ImageSource),
    //    ownerType: typeof(TitleBarComp),
    //    new FrameworkPropertyMetadata(
    //         // new ImageBrush(new ImageSource { "../../../Assets/main_page_bg.png" })
    //         new BitmapImage(new Uri("pack://application:,,,/Assets/main_page_bg.png"))
    //    ));

    //public ImageSource? TitleBackGround
    //{
    //    get => (ImageSource?)GetValue(TitleBackGroundProperty);
    //    set => SetValue(TitleBackGroundProperty, value);
    //}

    //#endregion


    public TitleBarComp()
    {
        InitializeComponent();
        // Loaded += (s, e) => DataContext = this;
    }
}