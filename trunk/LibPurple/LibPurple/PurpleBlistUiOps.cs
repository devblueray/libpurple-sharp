using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    [StructLayout(LayoutKind.Sequential)]
    public class PurpleBlistUiOps
    {
        public IntPtr new_list;
        public IntPtr new_node;
        public IntPtr show;
        public IntPtr update;
        public IntPtr _remove;
        public IntPtr destroy;
        public IntPtr set_visible;
        public IntPtr request_add_buddy;
        public IntPtr request_add_chat;
        public IntPtr request_add_group;
        public IntPtr resv1;
        public IntPtr resv2;
        public IntPtr resv3;
        public IntPtr resv4;
    }
}
