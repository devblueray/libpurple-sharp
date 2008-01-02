using System;
using System.Runtime.InteropServices;
using GLib;

namespace KentState.Purple
{
    public delegate uint TimeoutAdd(uint interval, IntPtr func,
        IntPtr data);
    public delegate uint TimeoutAddSeconds(uint interval, IntPtr func,
        IntPtr data);
    public delegate int SourceRemove(uint handle);
    public delegate uint InputAdd(int fd, int cond, IntPtr func,
        IntPtr data);
    public delegate int InputRemove(uint handle);
    public delegate int InputGetError(int fd, IntPtr error);

    public delegate bool GSourceFunc(IntPtr data);
    public delegate void PurpleInputFunction(IntPtr ptr, int val, int cond);

    [StructLayout(LayoutKind.Sequential)]
    public class PurpleEventLoopUiOps
    {
        public TimeoutAdd timeout_add;
        public SourceRemove timeout_remove;
        public InputAdd input_add;
        public InputRemove input_remove;
        public IntPtr input_get_error;
        public TimeoutAddSeconds timeout_add_secs;
        public IntPtr field1;
        public IntPtr field2;
        public IntPtr field3;
    }
}
