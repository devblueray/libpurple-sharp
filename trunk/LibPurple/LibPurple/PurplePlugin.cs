using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurplePlugin : PurpleBase
    {
        internal PurplePlugin(IntPtr handle) : base(handle)
        {
        }

        public string Name
        {
            get
            {
                IntPtr ptr = purple_plugin_get_name(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
        }

        public string ID
        {
            get
            {
                IntPtr ptr = purple_plugin_get_id(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
        }

        public static PurplePlugins Protocols
        {
            get
            {
                IntPtr ptr = purple_plugins_get_protocols();
                GList list = (GList)Marshal.PtrToStructure(ptr, typeof(GList));
                return new PurplePlugins(list);
            }
        }

        public static string SearchPath
        {
            set
            {
                purple_plugins_add_search_path(value);
            }
        }
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_plugin_get_name(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_plugin_get_id(IntPtr value);
        
        [DllImport("libpurple.dll")]
        static extern IntPtr purple_plugins_get_protocols();
        
        [DllImport("libpurple.dll")]
        static extern void purple_plugins_add_search_path(string dir);
    }
}
