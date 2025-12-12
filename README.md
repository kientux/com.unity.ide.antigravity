# Antigravity Editor Package for Unity

This package provides Unity Editor integration with [Antigravity](https://antigravity.google/), forked from original vscode package and rewrite using Cursor :D

## Features

- **Auto-discovery**: Automatically detects Antigravity installations on your system
- **Project generation**: Generates `.csproj` and `.sln` files for IntelliSense and code navigation
- **Open scripts**: Double-click scripts in Unity to open them in Antigravity at the correct line
- **Debugging support**: Configures launch settings for Unity debugging via the vstuc extension
- **SDK-style projects**: Modern project format with better compatibility

## Requirements

- Unity 2021.3 or later
- Antigravity installed at `/Applications/Antigravity.app` (macOS) or available via `agy` command (Linux)
- [Unity for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=visualstudiotoolsforunity.vstuc) extension (vstuc) for debugging support

## Installation

1. Open the Package Manager in Unity (**Window** > **Package Manager**)
2. Click the **+** button and select **Add package from git URL...**
3. Enter the package repository URL

## Usage

1. Open Unity and go to **Edit** > **Preferences** > **External Tools**
2. Select **Antigravity** from the **External Script Editor** dropdown
3. Configure which packages should generate `.csproj` files
4. Click **Regenerate project files** if needed

## Configuration

The package creates a `.vscode` folder in your Unity project with:

- `settings.json` - Editor settings and file exclusions
- `launch.json` - Debug configuration for attaching to Unity
- `extensions.json` - Recommended extensions (vstuc)

To prevent the package from modifying these files, create a `.vscode/.vstupatchdisable` file.

## Supported Platforms

- **macOS**: Uses the `open` command to launch Antigravity.app
- **Linux**: Uses the `agy` CLI command

## License

See [LICENSE.md](LICENSE.md) for license information.

