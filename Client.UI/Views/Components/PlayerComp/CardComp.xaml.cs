using System.Windows.Controls;

namespace Client.UI.Views.Components.PlayerComp;

/// <summary>
/// PlayerCard.xaml 的交互逻辑
/// </summary>
public partial class PlayerCard : UserControl
{
    // 定义依赖属性
    public static readonly DependencyProperty HeaderNameProperty = DependencyProperty.Register(
            name: "HeaderName",
            propertyType: typeof(string),
            ownerType: typeof(PlayerCard),
            new FrameworkPropertyMetadata(
                defaultValue: "默认标题", // 默认值
                flags: FrameworkPropertyMetadataOptions.BindsTwoWayByDefault // 默认双向绑定
            ));

    // CLR 属性包装器
    public string CardName
    {
        get => (string)GetValue(HeaderNameProperty);
        set => SetValue(HeaderNameProperty, value);
    }


    public PlayerCard()
    {
        InitializeComponent();
        //Loaded += (s, e) => DataContext = this;
    }
}
