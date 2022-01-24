# Using WinRT APIs in a Windows Desktop App

Listens for BLE advertizements with a simple CLI .exe written in C# source code using .net APIs. 


Program Output Example: 

```console
|BLE ADDR            |Name                |RSSI      |
|B8:BC:5B:58:A6:74   |                    |-91       |
|1C:D6:BE:07:D3:1E   | Samsung TV         |-73       |
|82:E8:2C:80:F4:78   |                    |-78       |
|44:E4:EE:5C:D7:06   |                    |-75       |
|18:08:E3:81:54:C4   |                    |-42       |
|39:59:DE:1B:C6:43   |                    |-86       |
```

Project reference: https://github.com/CarterAppleton/Win10Win32Bluetooth 


## Method

This program references `Windows.wnmd` API which contains core BLE APIs. 



## Listening for Bluetooth Advertisements

To use an API other than the provided API: 

### Setting Up
To use the WinRT APIs, add two references:

1. C:\Program Files (x86)\Windows Kits\10\UnionMetadata\Windows.winmd

2. C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETCore\v4.5\System.Runtime.WindowsRuntime.dll

**Note:** *#2 Depends on the framework version you are using!*
