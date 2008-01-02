using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public delegate void UI_Init();

    [StructLayout(LayoutKind.Sequential)]
    public class PurpleCoreUiOps
    {
        public IntPtr ui_prefs_init;
        public IntPtr debug_ui_init;
        public UI_Init ui_init;
        public IntPtr quit;
        public IntPtr get_ui_info;
        public IntPtr resv1;
        public IntPtr resv2;
        public IntPtr resv3;
    };
}
