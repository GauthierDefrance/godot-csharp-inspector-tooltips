# C# Inspector Tooltips for Godot

An editor plugin that displays C# XML `<summary>` comments as tooltips for exported properties in the Godot Inspector.

## Features

- Uses standard C# XML documentation comments.
- Supports exported fields and properties.
- Preserves line breaks and blank lines.
- Reloads documentation automatically after a C# build.
- Runs only in the editor and does not affect game performance.

## Requirements

- Godot 4.7 with .NET support.
- .NET 8.

Only Godot 4.7 has been tested so far.

## Installation

1. Copy `addons/csharp_inspector_tooltips` into your Godot project.
2. Enable XML documentation generation in your `.csproj`:

```xml
<PropertyGroup>
  <GenerateDocumentationFile>true</GenerateDocumentationFile>
  <NoWarn>$(NoWarn);1591</NoWarn>
</PropertyGroup>
```

`1591` is suppressed because enabling XML documentation otherwise warns about every public member without documentation.

3. Build the C# project.
4. Open **Project > Project Settings > Plugins**.
5. Enable **C# Inspector Tooltips**.

## Usage

Add a standard XML `<summary>` directly above an exported field or property:

```csharp
/// <summary>
/// Location loaded immediately when the game starts in the editor.
/// Skips the main menu and is intended for quick testing.
///
/// Leave empty to use the normal main menu flow.
/// </summary>
[Export]
public Resource? DebugStartLocation { get; set; }
```

Build the C# project, select the object in the Scene dock, and hover over `Debug Start Location` in the Inspector.

After later documentation changes, build again. The plugin detects the updated XML file and refreshes the current Inspector automatically.

## Writing Useful Tooltips

A useful description should explain:

1. What the field controls.
2. When or where it is used.
3. What happens when it is empty or keeps its default value.
4. Any important setup rule or limitation.

Prefer two to four short lines. Avoid repeating the field name or its type.

## How It Works

The C# compiler writes `<summary>` comments to the generated XML documentation file. The plugin reads that file, matches documented members to C# scripts and Inspector properties, then assigns the text to the existing Inspector controls.

The XML file is checked once per second inside the editor. The plugin code is wrapped in `#if TOOLS`, so it is excluded from exported games.

## Current Limitations

- Only Godot 4.7 .NET has been tested.
- A C# build is required after changing documentation.
- The C# class name should match its script filename.
- Documentation is currently read from the editor Debug build output.
- `<summary>` is supported; other XML elements such as `<remarks>` are not yet processed separately.

## Troubleshooting

### The plugin is not listed

Make sure this file exists:

```text
addons/csharp_inspector_tooltips/plugin.cfg
```

Then build the C# project and restart the editor if necessary.

### A tooltip is missing

- Confirm `<GenerateDocumentationFile>true</GenerateDocumentationFile>` is present in the `.csproj`.
- Build the C# project.
- Confirm the comment uses `<summary>` and is directly above an `[Export]` field or property.
- Confirm the script filename matches the C# class name.

## License

MIT License. See [LICENSE](LICENSE).

## Contributing

Bug reports and focused pull requests are welcome. Please include the Godot version, operating system, and a minimal reproduction when reporting an issue.
