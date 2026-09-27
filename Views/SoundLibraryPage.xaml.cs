using Microsoft.UI.Xaml;using VicarShotgunKeyboard.Models;using System.Collections.Generic;
namespace VicarShotgunKeyboard.Views;
public sealed partial class SoundLibraryPage:Page
{
 public IEnumerable<SoundPack> Packs=>App.Sounds.Packs;
 public SoundLibraryPage(){InitializeComponent();}
 void Test_Click(object s,RoutedEventArgs e){if((s as FrameworkElement)?.Tag is SoundItem i)App.Audio.Play(i);}
}