using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public delegate void AccountConnected(PurpleConnection conn, bool direction);
    internal delegate void SignedInOut(IntPtr conn, IntPtr data);

    public class PurpleConnection : PurpleBase
    {
        internal PurpleConnection(IntPtr handle) : base(handle)
        {
        }
        
        private static SignedInOut signed_on_evt;
        private static SignedInOut signed_out_evt;

        internal static void Init()
        {
            purple_connections_init();
            IntPtr handle = purple_connections_get_handle();

            signed_on_evt = new SignedInOut(signed_on);
            PurpleSignal.Connect(handle, "signed-on",
                Marshal.GetFunctionPointerForDelegate(signed_on_evt),
                IntPtr.Zero);

            signed_out_evt = new SignedInOut(signed_out);
            PurpleSignal.Connect(handle, "signed-off",
                Marshal.GetFunctionPointerForDelegate(signed_out_evt),
                IntPtr.Zero);
        }

        public static void DisconnectAll()
        {
            purple_connections_disconnect_all();
        }

        public static event AccountConnected OnAccountConnected;

        private static void signed_on(IntPtr conn, IntPtr data)
        {
            if(OnAccountConnected != null)
            {
                OnAccountConnected(new PurpleConnection(conn), true);
            }
        }

        private static void signed_out(IntPtr conn, IntPtr data)
        {
            if(OnAccountConnected != null)
            {
                OnAccountConnected(new PurpleConnection(conn), false);
            }
        }

        public PurpleAccount Account
        {
            get
            {
                IntPtr ptr = purple_connection_get_account(handle);
                return new PurpleAccount(ptr);
            }
            set
            {
                 purple_connection_set_account(handle, value.handle);
            }
        }

        public PurpleConnectionState State
        {
            get
            {
                return purple_connection_get_state(handle);
            }
            set
            {
                purple_connection_set_state(handle, value);
            }
        }

        public string DisplayName
        {
            get
            {
                IntPtr ptr = purple_connection_get_display_name(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
            set
            {
                purple_connection_set_display_name(handle, value);
            }
        }

        public PurpleServer Server
        {
            get
            {
                return new PurpleServer(this);
            }
        }

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_connection_get_account(IntPtr connection);

        [DllImport("libpurple.dll")]
        static extern void purple_connection_set_account(IntPtr connection, IntPtr account);          

        [DllImport("libpurple.dll")]
        static extern PurpleConnectionState purple_connection_get_state(IntPtr connection);          

        [DllImport("libpurple.dll")]
        static extern void purple_connection_set_state(IntPtr connection, PurpleConnectionState state);          

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_connection_get_display_name(IntPtr connection);          

        [DllImport("libpurple.dll")]
        static extern void purple_connection_set_display_name(IntPtr connection, string name);
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_connections_get_handle();

        [DllImport("libpurple.dll")]
        static extern void purple_connections_init();

        [DllImport("libpurple.dll")]
        static extern void purple_connections_disconnect_all();
    }
}
