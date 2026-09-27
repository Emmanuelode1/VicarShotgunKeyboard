using Microsoft.UI.Xaml;using Microsoft.UI.Xaml.Controls;using System.Threading.Tasks;using VicarShotgunKeyboard.Views;
namespace VicarShotgunKeyboard;
public sealed partial class MainWindow:Window
{
 public MainWindow(){InitializeComponent();RootFrame.Navigate(typeof(DashboardPage));}
 public void InitializeServices(){App.Keyboard.KeyPressed+=(_,e)=>{if(App.Settings.Current.Enabled)App.Audio.PlayForKey();DispatcherQueue.TryEnqueue(()=>{if(RootFrame.Content is DashboardPage p)p.NotifyKey(e.VirtualKey);});};}
 public void ShowWelcome(){var d=new ContentDialog{Title="Turn every keystroke into a shotgun blast.",Content="Your keystrokes are never recorded, stored, or transmitted. Keyboard input is used only to trigger sound effects locally.",PrimaryButtonText="GET STARTED",CloseButtonText="Later",XamlRoot=Content.XamlRoot};_=WelcomeAsync(d);}
 async Task WelcomeAsync(ContentDialog d){if(await d.ShowAsync()==ContentDialogResult.Primary){App.Settings.Current.FirstLaunchCompleted=true;await App.SaveSettingsAsync();}}
 void Close_Click(object s,RoutedEventArgs e)=>Close();
}