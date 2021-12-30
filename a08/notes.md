## Mono compile and disassemble

Csharp build tools `sudo apt install mono-complete`

Build to exe `mcs /o Selsort.cs`

Disassemble `monodis --output Selsort.il Selsort.exe`

## Java compile and disassemble

`javap -verbose -c Selsort > Selsort.jvmbytecode`