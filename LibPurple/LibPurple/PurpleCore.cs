using System;
using System.Runtime.InteropServices;
using GLib;

namespace KentState.Purple
{
    public class PurpleCore
    {
        PurpleCoreUiOps ui_ops;
        IntPtr ui_ops_ptr;
        bool inited = false;

        //PurpleCoreUiOps core_uiops = new PurpleCoreUiOps();

        public PurpleCore(string id)
        {
            if(!inited && !Init(id))
                throw new Exception("Failed to init libpurple");
            else
                inited = true;            
        }

        public bool Init(string id)
        {
            if(!inited)
            {
                /* This isn't necessary now that we get the received-im-msg signal */
                //core_uiops.ui_init = new UI_Init(ui_init);
                //UiOps = core_uiops;

                PurpleEventLoop.Init();

                int ret = purple_core_init(id);

                PurpleConnection.Init();
                PurpleConversation.Init();
                
                if(ret == 1)
                    return true;
                else
                    return false;
            }
            return true;
        }

        public static bool Debug
        {
            set
            {
                int debug = value == true ? 1 : 0;
                purple_debug_set_enabled(debug);
            }
        }

        public PurpleCoreUiOps UiOps
        {
            set
            {
                ui_ops = value;
                ui_ops_ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(ui_ops));
                Marshal.StructureToPtr(ui_ops, ui_ops_ptr, false);
                purple_core_set_ui_ops(ui_ops_ptr);
            }
        }
       
        PurpleConversationUiOps conv_uiops = new PurpleConversationUiOps(); 

        public void ui_init()
        {
            conv_uiops.write_conv = new WriteConv(PurpleConversation.write_conv);
            PurpleConversation.UiOps = conv_uiops;
        }

        public static PurpleBlist Blist
        {
            set
            {
                purple_set_blist(value.handle);
            }
            get
            {
                IntPtr ptr = purple_get_blist();
                return new PurpleBlist(ptr);
            }
        }

        public static string UserDirectory
        {
            set
            {
                purple_util_set_user_dir(value);
            }
        }

        public void Run()
        {
            MainLoop loop = new MainLoop();
            loop.Run();
        }

        [DllImport("libpurple.dll")]
        static extern int purple_core_init(string id);

        [DllImport("libpurple.dll")]
        static extern void purple_core_set_ui_ops(IntPtr value);
        
        [DllImport("libpurple.dll")]
        static extern void purple_debug_set_enabled(int value);
        
        [DllImport("libpurple.dll")]
        static extern void purple_set_blist(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_get_blist();
        
        [DllImport("libpurple.dll")]
        static extern void purple_util_set_user_dir(string dir);
        
    }
}
