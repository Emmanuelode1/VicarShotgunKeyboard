using System; using System.IO; using System.Text.Json; using System.Threading.Tasks; using VicarShotgunKeyboard.Models;
namespace VicarShotgunKeyboard.Services;
public sealed class AppSettingsStore
{
 readonly AppLogger logger; readonly string path=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"VicarShotgunKeyboard","settings.json");
 public AppSettings Current{get;private set;}=new(); public AppSettingsStore(AppLogger l)=>logger=l;
 public async Task LoadAsync(){try{if(File.Exists(path)){var j=await File.ReadAllTextAsync(path);Current=JsonSerializer.Deserialize<AppSettings>(j)??new();}}catch(Exception e){logger.Error("Settings load failed",e);Current=new();}}
 public async Task SaveAsync(){try{Directory.CreateDirectory(Path.GetDirectoryName(path)!);await File.WriteAllTextAsync(path,JsonSerializer.Serialize(Current,new JsonSerializerOptions{WriteIndented=true}));}catch(Exception e){logger.Error("Settings save failed",e);}}
}