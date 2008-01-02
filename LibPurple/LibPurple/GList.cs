using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    [StructLayout(LayoutKind.Sequential)]
    public class GList
    {
        public IntPtr data;
        public IntPtr next;
        public IntPtr prev;
    }
}
