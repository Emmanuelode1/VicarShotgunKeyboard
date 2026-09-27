using System; using System.Collections.Generic; using System.IO; using System.Linq; using VicarShotgunKeyboard.Models;
namespace VicarShotgunKeyboard.Services;
public sealed class SoundManager
{
 readonly AppLogger logger; readonly List<SoundPack> packs=new(); public IReadOnlyList<SoundPack>Packs=>packs; public SoundManager(AppLogger l)=>logger=l;
 public void LoadBuiltInSounds(){packs.Clear();var root=Path.Combine(AppContext.BaseDirectory,"Assets","Sounds");if(!Directory.Exists(root))return;foreach(var d in Directory.GetDirectories(root)){var p=new SoundPack{Name=Path.GetFileName(d),DirectoryPath=d};foreach(var f in Directory.EnumerateFiles(d,"*.wav"))p.Sounds.Add(new SoundItem{Name=Path.GetFileNameWithoutExtension(f),FilePath=f,IsBuiltIn=true});if(p.Sounds.Count>0)packs.Add(p);}}
 public SoundPack? GetPack(string n)=>packs.FirstOrDefault(p=>p.Name.Equals(n,StringComparison.OrdinalIgnoreCase));
 public IEnumerable<SoundItem> GetAll(string n)=>GetPack(n)?.Sounds??Enumerable.Empty<SoundItem>();
 public SoundItem? GetSound(string p,string n)=>GetPack(p)?.Sounds.FirstOrDefault(s=>s.Name.Equals(n,StringComparison.OrdinalIgnoreCase));
}