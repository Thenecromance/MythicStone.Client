using Wpf.Ui.Controls;

namespace Client.UI.ControlsLookup;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class GalleryPageAttribute(string description, SymbolRegular icon) : Attribute
{
    public string Description { get; } = description;

    public SymbolRegular Icon { get; } = icon;
}