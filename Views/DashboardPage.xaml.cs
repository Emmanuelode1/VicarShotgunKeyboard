using Microsoft.UI.Xaml;using Microsoft.UI.Xaml.Controls;using Microsoft.UI.Xaml.Controls.Primitives;using System.Linq;using VicarShotgunKeyboard.Models;
namespace VicarShotgunKeyboard.Views;
public sealed partial class DashboardPage:Page
{
 public DashboardPage(){InitializeComponent();BuildKeyboard();Loaded+=(_,_)Refresh();}
 void Refresh(){var s=App.Settings.Current;ModeSwitch.IsOn=s.Enabled;ModeText.Text=s.Enabled?"ON":"OFF";StatusText.Text=s.Enabled?"ACTIVE":"PAUSED";VolumeSlider.Value=s.Volume*100;VolumeLabel.Text=$"{s.Volume:P0}";RandomBox.IsChecked=s.RandomizeSounds;OverlapBox.IsChecked=s.AllowOverlappingSounds;MaxVoices.Value=s.MaxSimultaneousSounds;PackCombo.ItemsSource=App.Sounds.Packs.ToList();PackCombo.SelectedItem=App.Sounds.GetPack(s.SelectedSoundPack);RefreshSounds();}
 void RefreshSounds(){if(PackCombo.SelectedItem is not SoundPack p)return;SoundCombo.ItemsSource=p.Sounds.ToList();SoundCombo.SelectedItem=p.Sounds.FirstOrDefault(x=>x.Name==App.Settings.Current.SelectedSound)??p.Sounds.FirstOrDefault();}
 public void NotifyKey(uint vk){foreach(var b in KeyboardPreview.Children.OfType<Button>())if(b.Tag is uint k&&k==vk){b.Opacity=.45;_ = Reset(b);break;}}
 async System.Threading.Tasks.Task Reset(Button b){await System.Threading.Tasks.Task.Delay(80);b.Opacity=1;}
 void BuildKeyboard(){foreach(var x in new[]{("Q",0x51u),("W",0x57u),("E",0x45u),("R",0x52u),("T",0x54u),("Y",0x59u),("U",0x55u),("I",0x49u),("O",0x4Fu),("P",0x50u),("A",0x41u),("S",0x53u),("D",0x44u),("F",0x46u),("G",0x47u),("H",0x48u),("J",0x4Au),("K",0x4Bu),("L",0x4Cu),("Z",0x5Au),("X",0x58u),("C",0x43u),("V",0x56u),("B",0x42u),("N",0x4Eu),("M",0x4Du),("SPACE",0x20u)}){KeyboardPreview.Children.Add(new Button{Content=x.Item1,Tag=x.Item2,Margin=new Thickness(3),Padding=new Thickness(x.Item1=="SPACE"?35:12,8),Background=new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.DarkSlateGray),Foreground=new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White)});}}
 void ModeSwitch_Toggled(object s,RoutedEventArgs e){if(!IsLoaded)return;App.Settings.Current.Enabled=ModeSwitch.IsOn;App.ApplySettings();_=App.SaveSettingsAsync();Refresh();}
 void Volume_ValueChanged(object s,RangeBaseValueChangedEventArgs e){if(!IsLoaded)return;App.Settings.Current.Volume=e.NewValue/100;App.Audio.SetVolume(App.Settings.Current.Volume);VolumeLabel.Text=$"{e.NewValue:0}%";_=App.SaveSettingsAsync();}
 void Pack_Changed(object s,SelectionChangedEventArgs e){if(PackCombo.SelectedItem is SoundPack p){App.Settings.Current.SelectedSoundPack=p.Name;RefreshSounds();_=App.SaveSettingsAsync();}}
 void Sound_Changed(object s,SelectionChangedEventArgs e){if(SoundCombo.SelectedItem is SoundItem i){App.Settings.Current.SelectedSound=i.Name;App.Audio.SetSelectedSound(i.Name);_=App.SaveSettingsAsync();}}
 void Test_Click(object s,RoutedEventArgs e)=>App.Audio.PlayTest();
 void Random_Changed(object s,RoutedEventArgs e){if(!IsLoaded)return;App.Settings.Current.RandomizeSounds=RandomBox.IsChecked==true;App.Audio.SetRandomize(App.Settings.Current.RandomizeSounds);_=App.SaveSettingsAsync();}
 void Overlap_Changed(object s,RoutedEventArgs e){if(!IsLoaded)return;App.Settings.Current.AllowOverlappingSounds=OverlapBox.IsChecked==true;App.Audio.SetOverlap(App.Settings.Current.AllowOverlappingSounds);_=App.SaveSettingsAsync();}
 void Max_Changed(object s,RangeBaseValueChangedEventArgs e){if(!IsLoaded)return;App.Settings.Current.MaxSimultaneousSounds=(int)e.NewValue;App.Audio.SetMaxVoices((int)e.NewValue);_=App.SaveSettingsAsync();}
}