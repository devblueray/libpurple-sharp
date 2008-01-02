using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public enum PurpleStatusPrimitive {
        Unset = 0,
        Offline,
        Available,
        Unavailable,
        Invisible,
        Away,
        ExtendedAway,
        Mobile,
        Count
    }

    public class PurpleStatus : PurpleBase
    {
        internal PurpleStatus(IntPtr handle) : base(handle)
        {
        }

        public bool Active
        {
            set
            {
                int ret = value == true ? 1 : 0;
                purple_status_set_active(handle, ret);
            }
        }

        [DllImport("libpurple.dll")]
        static extern void purple_status_set_active(IntPtr status, int active);
    }
}
