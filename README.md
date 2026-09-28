# FrizzQOL.BeeNiceHive

https://github.com/frizzlebeard/FrizzQOL.BeeNiceHive

> Set how much honey a hive holds and how long each one takes. The time left shows in yellow. 🐝

---

## What it does

The hive still makes and gives honey the normal way. Looking at it keeps the normal text and adds one yellow line.

While it is filling, that line is the time left until it is full. When it is full, the line says Full.

Out of the box the time and the amount stay the normal Valheim values.

## Config

`BepInEx/config/com.vhmod.beenicehive.cfg`

| Setting | Default | What it does |
| --- | --- | --- |
| Minutes | 0 | Real minutes for each honey. 0 keeps the normal time. Below 0 does the same. |
| Honey | 0 | How many honey a full hive holds. 0 keeps the normal amount. Below 0 does the same. |

The yellow time is the honey still needed times `Minutes`, minus time already spent on the next one. Raising `Honey` makes a full hive take longer. Lowering `Minutes` shortens every honey, including one already started.

## Multiplayer

Install this on the dedicated server and on every client. Use the same `Minutes` and `Honey` on each of them.

## Install

Install with r2modman or the Thunderstore Mod Manager.

To install by hand, copy `FrizzQOL.BeeNiceHive.dll` into `BepInEx/plugins`.

## Requirements

- Valheim
- [BepInExPack for Valheim](https://thunderstore.io/c/valheim/p/denikson/BepInExPack_Valheim/)

## Support

☕ If you enjoy my work, please buy me a coffee.

Cash App: `$FrizzleFry4`

## License

[MIT](LICENSE). You can use, copy, change, and share this mod. Keep the copyright notice with any copy.

## Building

1. Copy `Environment.props.example` to `Environment.props`.
2. Set your Valheim and BepInEx folders in that file.
3. From this folder, run:

```
dotnet build BeeNiceHive.sln -c Release
```

The plugin file is `FrizzQOL.BeeNiceHive.dll`, under the project `bin\Release\net48` folder.

`Environment.props` stays on your machine. It is listed in `.gitignore`.
