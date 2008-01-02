using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleSignal
    {
        public static int Connect(IntPtr handle, string signal, IntPtr func,
            IntPtr data)
        {
            int ret = 0;
            purple_signal_connect(handle, signal, ref ret, func, data);
            return ret;
        }
        
        [DllImport("libpurple.dll")]
        static extern void purple_signal_connect(IntPtr handle, string signal,
            ref int rhandle, IntPtr func, IntPtr data);
    }
}
