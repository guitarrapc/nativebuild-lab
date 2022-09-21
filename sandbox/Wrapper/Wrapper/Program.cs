using System.Runtime.InteropServices;

//FiboCaller.Call();
WrapperCaller.Call();

public static class FiboCaller
{
  public static void Call()
  {
    var handleLib = IntPtr.Zero;

    var lib = "";
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      lib = @"C:\git\guitarrapc\nativebuild-lab\pkg\fibo\v1.0.0\windows\x64\fibo.dll";
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
      lib = @"/mnt/c/git/guitarrapc/nativebuild-lab/pkg/fibo/v1.0.0/linux/x64/libfibo.so";
    }
    try
    {
      if (NativeLibrary.TryLoad(lib, out handleLib))
      {
        // echo_no_export
        try
        {
          var echoNoExport = Marshal.GetDelegateForFunctionPointer<SampleNative.echo_no_export>(NativeLibrary.GetExport(handleLib, "echo_no_export"));
          var r = echoNoExport.Invoke((IntPtr)20); // always return 1
          Console.WriteLine($"echo_no_export: {r}");
        }
        catch (EntryPointNotFoundException ex)
        {
          Console.WriteLine($" - {ex.Message} This is expected behaviour, entrypoint should not found.");
        }

        // get_sample_data / set_sample_data
        {
          {
            var GetSampleData = Marshal.GetDelegateForFunctionPointer<SampleNative.get_sample_data>(NativeLibrary.GetExport(handleLib, "get_sample_data"));
            var o = new SampleData();
            GetSampleData(ref o);
            Console.WriteLine($"get_sample_data: num {o.num}; value {o.value}");
          }

          var SetSampleData = Marshal.GetDelegateForFunctionPointer<SampleNative.set_sample_data>(NativeLibrary.GetExport(handleLib, "set_sample_data"));
          // reserve unmanaged memory for input
          var i = new SampleData()
          {
            value = 0.12f,
            num = 100
          };
          System.IntPtr inputPtr = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(SampleData)));
          try
          {
            // copy managed structure to unmanaged
            Marshal.StructureToPtr(i, inputPtr, false);

            // execute unmanaged C function
            var o = new SampleData();
            SetSampleData.Invoke(ref o, inputPtr);
            Console.WriteLine($"set_sample_data: num {o.num}; value {o.value}");
          }
          finally
          {
            Marshal.FreeCoTaskMem(inputPtr);
          }
        }

        // fibo
        {
          var fibo = Marshal.GetDelegateForFunctionPointer<FiboNative.fibo>(NativeLibrary.GetExport(handleLib, "fibo"));
          var result = fibo.Invoke((IntPtr)10);
          Console.WriteLine($"fibo: result {result};");
        }
      }
    }
    finally
    {
      if (handleLib != IntPtr.Zero)
      {
        NativeLibrary.Free(handleLib);
      }
    }
  }
}

public static class WrapperCaller
{
  public static void Call()
  {
    var handleLib = IntPtr.Zero;

    var lib = "";
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
    {
      throw new NotSupportedException("Run VS Debug on WSL");
    }
    else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    {
      lib = @"/mnt/c/git/guitarrapc/nativebuild-lab/fibowrapper/lib/libwrapper.so";
    }
    try
    {
      if (NativeLibrary.TryLoad(lib, out handleLib))
      {
        // my_fibo (これだけ呼べてほしい) -> 呼べてしまう
        {
          var fibo = Marshal.GetDelegateForFunctionPointer<WrapperNative.my_fibo>(NativeLibrary.GetExport(handleLib, "my_fibo"));
          var result = fibo.Invoke((IntPtr)10);
          Console.WriteLine($"my_fibo: result {result};");
        }

        // fibo (呼ばれてほしくない) -> 呼べてしまう
        {
          var fibo = Marshal.GetDelegateForFunctionPointer<FiboNative.fibo>(NativeLibrary.GetExport(handleLib, "fibo"));
          var result = fibo.Invoke((IntPtr)10);
          Console.WriteLine($"fibo: result {result};");
        }
      }
    }
    finally
    {
      if (handleLib != IntPtr.Zero)
      {
        NativeLibrary.Free(handleLib);
      }
    }
  }
}

unsafe class WrapperNative
{
  [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
  public delegate int my_fibo(IntPtr n);
}

unsafe class FiboNative
{
  [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
  public delegate int fibo(IntPtr n);
}

unsafe class SampleNative
{
  [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
  public delegate int echo_no_export(IntPtr n);

  [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
  public delegate void get_sample_data(ref SampleData output);

  [UnmanagedFunctionPointer(CallingConvention.StdCall, CharSet = CharSet.Unicode)]
  public delegate void set_sample_data(ref SampleData output, IntPtr input);
}

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
public struct SampleData
{
  public float value;
  public int num;
}
