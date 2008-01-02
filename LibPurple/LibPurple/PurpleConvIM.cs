using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleConvIM : PurpleBase
    {
        internal PurpleConvIM(IntPtr handle) : base(handle)
        {
        }

        public PurpleConversation Conversation
        {
            get
            {
                return new PurpleConversation(
                    purple_conv_im_get_conversation(handle)
                );
            }
        }

        public PurpleTypingState TypingState
        {
            get
            {
                return purple_conv_im_get_typing_state(handle);
            }
            set
            {
                purple_conv_im_set_typing_state(handle, value);
            }
        }

        public void Send(string message)
        {
            purple_conv_im_send(handle, message);
        }

        public static PurpleConvIM Find(PurpleAccount account, string who)
        {
            IntPtr ptr = purple_find_conversation_with_account(
                PurpleConversationType.IM, who, account.handle);

            PurpleConversation conv;

            if(ptr == IntPtr.Zero)
                conv = new PurpleConversation(PurpleConversationType.IM,
                        account, who);
            else
                conv = new PurpleConversation(ptr);

            return conv.IM;
        }

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conv_im_get_conversation(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern PurpleTypingState purple_conv_im_get_typing_state(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern void purple_conv_im_set_typing_state(IntPtr value, PurpleTypingState state);

        [DllImport("libpurple.dll")]
        static extern void purple_conv_im_send(IntPtr value, string message);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_find_conversation_with_account(
            PurpleConversationType type, string who, IntPtr account);
    }
}
