using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleEventLoop
    {
        static PurpleEventLoopUiOps ui_ops = new PurpleEventLoopUiOps();
        static IntPtr ui_ops_ptr;
        static bool inited = false;

        public static void Init()
        {
            if(!inited)
            {
                ui_ops.timeout_add = new TimeoutAdd(timeout_add);
                ui_ops.timeout_remove = new SourceRemove(source_remove);
                ui_ops.input_add = new InputAdd(input_add);
                ui_ops.timeout_add_secs = new TimeoutAddSeconds(timeout_add);
                ui_ops.input_remove = new InputRemove(source_remove);
                ui_ops_ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(ui_ops));
                Marshal.StructureToPtr(ui_ops, ui_ops_ptr, false);
                purple_eventloop_set_ui_ops(ui_ops_ptr);
            }
        }
        
        static uint timeout_add(uint interval, IntPtr func, IntPtr data)
        {
            return g_timeout_add(interval, func, data);
        }

        static int source_remove(uint handle)
        {
            return g_source_remove(handle);
        }

        static uint input_add(int fd, int cond, IntPtr func, IntPtr data)
        {
            return purplesharp_input_add(fd, cond, func, data);
        }

        [DllImport("libpurple.dll")]
        static extern void purple_eventloop_set_ui_ops(IntPtr value);
        
        [DllImport("libglib-2.0.dll")]
        static extern uint g_timeout_add(uint intervae, IntPtr func,
            IntPtr data);

        [DllImport("libglib-2.0.dll")]
        static extern int g_source_remove(uint tag);

        [DllImport("libpurple-sharp-glue.dll")]
        static extern uint purplesharp_input_add(int fd, int cond, IntPtr func,
            IntPtr data);
    }
}
