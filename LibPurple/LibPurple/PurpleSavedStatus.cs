using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleSavedStatus : PurpleBase
    {
        internal PurpleSavedStatus(IntPtr handle) : base(handle)
        {
        }

        public PurpleSavedStatus(string text, PurpleStatusPrimitive prim)
        {
            handle = purple_savedstatus_new(text, prim);
        }

        public void Activate()
        {
            purple_savedstatus_activate(handle);
        }

        public void Activate(PurpleAccount acct)
        {
            purple_savedstatus_activate_for_account(handle, acct.handle);
        }

        public string Title
        {
            get
            {
                IntPtr ptr = purple_savedstatus_get_title(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
            set
            {
                purple_savedstatus_set_title(handle, value);
            }
        }

        public PurpleStatusPrimitive Type
        {
            get
            {
                return purple_savedstatus_get_type(handle);
            }
            set
            {
                purple_savedstatus_set_type(handle, value);
            }
        }

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_savedstatus_new(string text, PurpleStatusPrimitive prim);

        [DllImport("libpurple.dll")]
        static extern void purple_savedstatus_activate(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern void purple_savedstatus_activate_for_account(IntPtr status, IntPtr account);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_savedstatus_get_title(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern void purple_savedstatus_set_title(IntPtr value, string text);

        [DllImport("libpurple.dll")]
        static extern PurpleStatusPrimitive purple_savedstatus_get_type(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern void purple_savedstatus_set_type(IntPtr value, PurpleStatusPrimitive prim);
    }
}
