using System.Windows.Controls;
using Wpf.Ui.Controls;

namespace Client.UI.Controls;

public partial class AddToBlackListContentDialog : ContentDialog
{
    public string Reason { get; private set; } = string.Empty;

    public AddToBlackListContentDialog(ContentPresenter? contentPresenter, string name)
        : base(contentPresenter)
    {
        this.Title = $"确定添加{name}至黑名单？";
        InitializeComponent();
    }


    protected override void OnButtonClick(ContentDialogButton button)
    {
        if (ReasonBox.Text.Length > 0)
        {
            Reason = ReasonBox.Text;
            base.OnButtonClick(button);
            return;
        }

        WarningText.Visibility = Visibility.Visible;
        ReasonBox.Focus();
        return;
    }
}