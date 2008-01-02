using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleBuddy : PurpleBase
    {
        internal PurpleBuddy(IntPtr handle) : base(handle)
        {
        }

        public string Name
        {
            get
            {
                IntPtr ptr = purple_buddy_get_name(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
        }

        public PurpleAccount Account
        {
            get
            {
                return new PurpleAccount(purple_buddy_get_account(handle));
            }
        }
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_buddy_get_name(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_buddy_get_account(IntPtr value);
    }
}
