.assembly extern mscorlib
{
  .ver 4:0:0:0
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 ) // .z\V.4..
}
.assembly 'Selsort'
{
  .custom instance void class [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::'.ctor'() =  (
		01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
		63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01       ) // ceptionThrows.

  .hash algorithm 0x00008004
  .ver  0:0:0:0
}
.module Selsort.exe // GUID = {525EDA63-320F-45FA-A5DA-8F4B5AEF2C64}


  .class private auto ansi beforefieldinit Selsort
  	extends [mscorlib]System.Object
  {
    .field  public static initonly  class [mscorlib]System.Random rnd

    // method line 1
    .method public hidebysig specialname rtspecialname 
           instance default void '.ctor' ()  cil managed 
    {
        // Method begins at RVA 0x2050
	// Code size 7 (0x7)
	.maxstack 8
	IL_0000:  ldarg.0 
	IL_0001:  call instance void object::'.ctor'()
	IL_0006:  ret 
    } // end of method Selsort::.ctor

    // method line 2
    .method public static hidebysig 
           default void Main (string[] args)  cil managed 
    {
        // Method begins at RVA 0x2058
	.entrypoint
	// Code size 99 (0x63)
	.maxstack 4
	.locals init (
		int32	V_0,
		int32[]	V_1,
		int32	V_2,
		int32	V_3)
	IL_0000:  ldc.i4.s 0x0a
	IL_0002:  stloc.0 
	IL_0003:  ldloc.0 
	IL_0004:  newarr [mscorlib]System.Int32
	IL_0009:  stloc.1 
	IL_000a:  ldc.i4.0 
	IL_000b:  stloc.2 
	IL_000c:  br IL_0027

	IL_0011:  ldloc.1 
	IL_0012:  ldloc.2 
	IL_0013:  ldsfld class [mscorlib]System.Random Selsort::rnd
	IL_0018:  ldc.i4 1000000
	IL_001d:  callvirt instance int32 class [mscorlib]System.Random::Next(int32)
	IL_0022:  stelem.i4 
	IL_0023:  ldloc.2 
	IL_0024:  ldc.i4.1 
	IL_0025:  add 
	IL_0026:  stloc.2 
	IL_0027:  ldloc.2 
	IL_0028:  ldloc.0 
	IL_0029:  blt IL_0011

	IL_002e:  ldloc.1 
	IL_002f:  call void class Selsort::SelectionSort(int32[])
	IL_0034:  ldc.i4.0 
	IL_0035:  stloc.3 
	IL_0036:  br IL_0056

	IL_003b:  ldloc.1 
	IL_003c:  ldloc.3 
	IL_003d:  ldelem.i4 
	IL_003e:  box [mscorlib]System.Int32
	IL_0043:  ldstr " "
	IL_0048:  call string string::Concat(object, object)
	IL_004d:  call void class [mscorlib]System.Console::Write(string)
	IL_0052:  ldloc.3 
	IL_0053:  ldc.i4.1 
	IL_0054:  add 
	IL_0055:  stloc.3 
	IL_0056:  ldloc.3 
	IL_0057:  ldloc.0 
	IL_0058:  blt IL_003b

	IL_005d:  call void class [mscorlib]System.Console::WriteLine()
	IL_0062:  ret 
    } // end of method Selsort::Main

    // method line 3
    .method public static hidebysig 
           default void SelectionSort (int32[] arr)  cil managed 
    {
        // Method begins at RVA 0x20c8
	// Code size 72 (0x48)
	.maxstack 4
	.locals init (
		int32	V_0,
		int32	V_1,
		int32	V_2,
		int32	V_3)
	IL_0000:  ldc.i4.0              |
	IL_0001:  stloc.0               + var0 = 0
	IL_0002:  br IL_003e            + branch 3e

	IL_0007:  ldloc.0           42->|
	IL_0008:  stloc.1               + var1 = var0
	IL_0009:  ldloc.0               |
	IL_000a:  ldc.i4.1              |
	IL_000b:  add                   |
	IL_000c:  stloc.2               + var2 = var0 + 1
	IL_000d:  br IL_0023            + branch to 0023

	IL_0012:  ldarg.0           27->|
	IL_0013:  ldloc.2               |
	IL_0014:  ldelem.i4             |
	IL_0015:  ldarg.0               |
	IL_0016:  ldloc.1               |
	IL_0017:  ldelem.i4             |
	IL_0018:  bge IL_001f           + arg0[2] >= arg0[1] ? branch 1f

	IL_001d:  ldloc.2               |
	IL_001e:  stloc.1               + var1 = var2
	IL_001f:  ldloc.2               |
	IL_0020:  ldc.i4.1              |
	IL_0021:  add                   |
	IL_0022:  stloc.2               + var2 += 1
	IL_0023:  ldloc.2               |
	IL_0024:  ldarg.0               |
	IL_0025:  ldlen                 |
	IL_0026:  conv.i4               |
	IL_0027:  blt IL_0012           + var2 < (int32)arg0.len ? branch 12

	IL_002c:  ldarg.0               |
	IL_002d:  ldloc.0               |
	IL_002e:  ldelem.i4             |
	IL_002f:  stloc.3               + var3 = arg0[0]
	IL_0030:  ldarg.0               |
	IL_0031:  ldloc.0               |
	IL_0032:  ldarg.0               |
	IL_0033:  ldloc.1               |
	IL_0034:  ldelem.i4             |
	IL_0035:  stelem.i4             + arg0[0] = arg0[1]
	IL_0036:  ldarg.0               |
	IL_0037:  ldloc.1               |
	IL_0038:  ldloc.3               |
	IL_0039:  stelem.i4             + arg0[var1] = (int32)var3
	IL_003a:  ldloc.0               |
	IL_003b:  ldc.i4.1              |
	IL_003c:  add                   | 
	IL_003d:  stloc.0               + var0 += 1
	IL_003e:  ldloc.0           02->|
	IL_003f:  ldarg.0               |
	IL_0040:  ldlen                 |
	IL_0041:  conv.i4               |
	IL_0042:  blt IL_0007           + var0 < (int32)arg0.len ? branch 07

	IL_0047:  ret 
    } // end of method Selsort::SelectionSort

    // method line 4
    .method private static hidebysig specialname rtspecialname 
           default void '.cctor' ()  cil managed 
    {
        // Method begins at RVA 0x211c
	// Code size 11 (0xb)
	.maxstack 8
	IL_0000:  newobj instance void class [mscorlib]System.Random::'.ctor'()
	IL_0005:  stsfld class [mscorlib]System.Random Selsort::rnd
	IL_000a:  ret 
    } // end of method Selsort::.cctor

  } // end of class Selsort

