using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public delegate void BuddySignInOut(PurpleBuddy buddy);
    public delegate void BuddySignedOn(IntPtr buddy);

    public class PurpleBlist : PurpleBase
    {
        private static BuddySignedOn bsigned_on_evt;
        private static BuddySignedOn bsigned_off_evt;

        public event BuddySignInOut OnBuddySignOn;
        public event BuddySignInOut OnBuddySignOff;

        internal PurpleBlist(IntPtr handle) : base(handle)
        {
        }

        public PurpleBlist()
        {
            handle = purple_blist_new();
        }

        public void Load()
        {
            purple_blist_load();

            IntPtr sig_handle = purple_blist_get_handle();

            bsigned_on_evt = new BuddySignedOn(bsigned_on);
            PurpleSignal.Connect(sig_handle, "buddy-signed-on",
                    Marshal.GetFunctionPointerForDelegate(bsigned_on_evt),
                    IntPtr.Zero);

            bsigned_off_evt = new BuddySignedOn(bsigned_off);
            PurpleSignal.Connect(sig_handle, "buddy-signed-off",
                    Marshal.GetFunctionPointerForDelegate(bsigned_off_evt),
                    IntPtr.Zero);
        }

        private void bsigned_on(IntPtr buddy)
        {
            if(OnBuddySignOn != null)
            {
                OnBuddySignOn(new PurpleBuddy(buddy));
            }
        }

        private void bsigned_off(IntPtr buddy)
        {
            if(OnBuddySignOff != null)
            {
                OnBuddySignOff(new PurpleBuddy(buddy));
            }
        }
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_blist_new();
        
        [DllImport("libpurple.dll")]
        static extern void purple_blist_load();
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_blist_get_handle();
    }
}
