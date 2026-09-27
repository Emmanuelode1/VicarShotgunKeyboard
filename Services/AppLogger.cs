using System; using System.IO; using System.Text;
namespace VicarShotgunKeyboard.Services;
public sealed class AppLogger:IDisposable
{
 readonly object gate=new(); readonly string path=Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),"VicarShotgunKeyboard","app.log");
 public AppLogger(){Directory.CreateDirectory(Path.GetDirectoryName(path)!);} public void Info(string m)=>Write("INFO",m); public void Warn(string m)=>Write("WARN",m);
 public void Error(string m,Exception? e=null)=>Write("ERROR",e is null?m:m+" | "+e.GetType().Name+": "+e.Message);
 void Write(string l,string m){lock(gate)File.AppendAllText(path,$"{DateTimeOffset.Now:O} [{l}] {m}{Environment.NewLine}",Encoding.UTF8);} public void Dispose(){}
}