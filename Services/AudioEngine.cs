using System; using System.Collections.Generic; using System.IO; using System.Linq; using System.Threading.Tasks; using NAudio.Wave; using VicarShotgunKeyboard.Models;
namespace VicarShotgunKeyboard.Services;
public sealed class AudioEngine:IDisposable
{
 sealed class Clip{public byte[] Data=Array.Empty<byte>();}
 sealed class Voice:IDisposable{public WaveOutEvent Output=new(){DesiredLatency=30};public BufferedWaveProvider Buffer=null!;public DateTime Started;public void Dispose()=>Output.Dispose();}
 readonly AppLogger logger;readonly SoundManager sounds;readonly object gate=new();readonly Dictionary<string,Clip> clips=new();readonly List<Voice> voices=new();readonly Random random=new();
 double volume=.75;bool muted,randomize=true,overlap=true;int max=8;string selected="";
 public AudioEngine(AppLogger l,SoundManager s){logger=l;sounds=s;}
 public Task InitializeAsync(){foreach(var p in sounds.Packs)foreach(var s in p.Sounds)Load(s);SetMaxVoices(max);return Task.CompletedTask;}
 void Load(SoundItem s){try{using var r=new WaveFileReader(s.FilePath);using var ms=new MemoryStream();r.CopyTo(ms);clips[s.FilePath]=new Clip{Data=ms.ToArray()};}catch(Exception e){logger.Error("Sound load failed: "+s.Name,e);}}
 public void SetVolume(double v){volume=Math.Clamp(v,0,1);lock(gate)foreach(var x in voices)x.Output.Volume=muted?0:(float)volume;} public void SetMuted(bool v)=>muted=v;
 public void SetRandomize(bool v)=>randomize=v;public void SetOverlap(bool v)=>overlap=v;public void SetSelectedSound(string s)=>selected=s;
 public void SetMaxVoices(int n){max=Math.Clamp(n,1,20);lock(gate){while(voices.Count<max)Create();while(voices.Count>max){voices[^1].Dispose();voices.RemoveAt(voices.Count-1);}}}
 void Create(){var v=new Voice();v.Buffer=new BufferedWaveProvider(new WaveFormat(44100,16,2)){DiscardOnBufferOverflow=true};v.Output.Init(v.Buffer);v.Output.Volume=(float)volume;voices.Add(v);}
 public void PlayForKey(){var p=sounds.GetPack(App.Settings.Current.SelectedSoundPack);if(p is null)return;var a=p.Sounds.Where(s=>s.Enabled&&clips.ContainsKey(s.FilePath)).ToList();if(a.Count==0)return;Play(randomize?a[random.Next(a.Count)]:a.FirstOrDefault(s=>s.Name.Equals(selected,StringComparison.OrdinalIgnoreCase))??a[0]);}
 public void PlayTest(){var s=sounds.GetSound(App.Settings.Current.SelectedSoundPack,selected)??sounds.GetAll(App.Settings.Current.SelectedSoundPack).FirstOrDefault();if(s is not null)Play(s);}
 void Play(SoundItem s){if(!clips.TryGetValue(s.FilePath,out var c)){Load(s);if(!clips.TryGetValue(s.FilePath,out c))return;}lock(gate){var v=voices.FirstOrDefault(x=>x.Output.PlaybackState!=PlaybackState.Playing);if(v is null){if(!overlap)return;v=voices[0];}v.Buffer.ClearBuffer();v.Buffer.AddSamples(c.Data,0,c.Data.Length);v.Started=DateTime.UtcNow;v.Output.Volume=muted?0:(float)volume;v.Output.Play();}}
 public void Dispose(){lock(gate){foreach(var v in voices)v.Dispose();voices.Clear();}}
}