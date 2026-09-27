# Vïçãr Shotgun Keyboard

Native Windows desktop keyboard sound-effects application built with C#, .NET 8, WinUI 3, Windows App SDK and XAML.

## Privacy
Your keystrokes are never recorded, stored, or transmitted. Keyboard input is used only to trigger sound effects locally.

## Features
- Global keyboard sound triggering
- Shotgun sound-pack architecture
- Random variations
- Volume and mute controls
- Bounded overlapping playback
- Configurable trigger categories
- Live keyboard preview
- Persistent per-user settings
- Dark WinUI interface
- No administrator requirement

## Development
Open VicarShotgunKeyboard.csproj in Visual Studio 2022+ on Windows with the Windows App SDK prerequisites installed.

Build with dotnet restore and dotnet build -c Release -p:Platform=x64.

GitHub Actions builds the project on a Windows runner.

## Release
Version 1.0.0. Production MSIX packages must be digitally signed before broad distribution.

See SECURITY.md and SUPPORT.md for privacy and support information.

License: MIT.
