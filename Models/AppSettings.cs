using System.Collections.Generic;
namespace VicarShotgunKeyboard.Models;
public sealed class AppSettings
{
 public bool FirstLaunchCompleted{get;set;}
 public bool Enabled{get;set;}=true; public double Volume{get;set;}=.75; public bool Muted{get;set;}
 public string SelectedSoundPack{get;set;}="Shotgun"; public string SelectedSound{get;set;}="";
 public bool RandomizeSounds{get;set;}=true; public bool PlayOnKeyRelease{get;set;}=false; public bool IgnoreKeyRepeat{get;set;}=true;
 public bool AllowOverlappingSounds{get;set;}=true; public int MaxSimultaneousSounds{get;set;}=8;
 public bool Letters{get;set;}=true; public bool Numbers{get;set;}=true; public bool Symbols{get;set;}=true; public bool Space{get;set;}=true;
 public bool Enter{get;set;}=true; public bool Backspace{get;set;}=true; public bool Tab{get;set;}=true; public bool ArrowKeys{get;set;}
 public bool FunctionKeys{get;set;} public bool Ctrl{get;set;} public bool Alt{get;set;} public bool Shift{get;set;} public bool WindowsKey{get;set;}
 public bool StartWithWindows{get;set;} public bool StartMinimized{get;set;} public bool MinimizeToTray{get;set;}=true; public bool ReducedMotion{get;set;}
 public string Theme{get;set;}="Dark"; public uint HotkeyModifiers{get;set;}=0x0002|0x0004; public uint HotkeyVirtualKey{get;set;}=0x53;
 public Dictionary<string,bool> TriggerFilters=>new(){["Letters"]=Letters,["Numbers"]=Numbers,["Symbols"]=Symbols,["Space"]=Space,["Enter"]=Enter,["Backspace"]=Backspace,["Tab"]=Tab,["ArrowKeys"]=ArrowKeys,["FunctionKeys"]=FunctionKeys,["Ctrl"]=Ctrl,["Alt"]=Alt,["Shift"]=Shift,["WindowsKey"]=WindowsKey};
}