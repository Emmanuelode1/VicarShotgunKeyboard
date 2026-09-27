using Microsoft.UI.Xaml;using Microsoft.UI.Xaml.Controls;
namespace VicarShotgunKeyboard.Views;
public sealed partial class SettingsPage:Page
{
 public SettingsPage(){InitializeComponent();Loaded+=(_,_)Load();}
 void Load(){var s=App.Settings.Current;RepeatBox.IsChecked=s.IgnoreKeyRepeat;ReleaseBox.IsChecked=s.PlayOnKeyRelease;LettersBox.IsChecked=s.Letters;NumbersBox.IsChecked=s.Numbers;SymbolsBox.IsChecked=s.Symbols;SpaceBox.IsChecked=s.Space;EnterBox.IsChecked=s.Enter;TrayBox.IsChecked=s.MinimizeToTray;}
 void Changed(object sender,RoutedEventArgs e){if(!IsLoaded)return;var s=App.Settings.Current;s.IgnoreKeyRepeat=RepeatBox.IsChecked==true;s.PlayOnKeyRelease=ReleaseBox.IsChecked==true;s.Letters=LettersBox.IsChecked==true;s.Numbers=NumbersBox.IsChecked==true;s.Symbols=SymbolsBox.IsChecked==true;s.Space=SpaceBox.IsChecked==true;s.Enter=EnterBox.IsChecked==true;s.MinimizeToTray=TrayBox.IsChecked==true;App.ApplySettings();_=App.SaveSettingsAsync();}
}