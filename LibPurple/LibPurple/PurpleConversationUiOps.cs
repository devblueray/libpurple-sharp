using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public delegate void WriteConv(IntPtr conv, IntPtr who, IntPtr alias,
        IntPtr msg, int flags, int time);

    [StructLayout(LayoutKind.Sequential)]
    public class PurpleConversationUiOps
    {
	    public IntPtr field1;                      /* create_conversation  */
    	public IntPtr field2;                      /* destroy_conversation */
	    public IntPtr field3;                      /* write_chat           */
    	public IntPtr field4;                      /* write_im             */
    	public WriteConv write_conv;              /* write_conv           */
    	public IntPtr field5;                      /* chat_add_users       */
    	public IntPtr field6;                      /* chat_rename_user     */
    	public IntPtr field7;                      /* chat_remove_users    */
    	public IntPtr field8;                      /* chat_update_user     */
    	public IntPtr field9;                      /* present              */
    	public IntPtr field10;                      /* has_focus            */
    	public IntPtr field11;                      /* custom_smiley_add    */
    	public IntPtr field12;                      /* custom_smiley_write  */
    	public IntPtr field13;                      /* custom_smiley_close  */
    	public IntPtr field14;                      /* send_confirm         */
    	public IntPtr field15;
    	public IntPtr field16;
    	public IntPtr field17;
        public IntPtr field18;
    }
}
