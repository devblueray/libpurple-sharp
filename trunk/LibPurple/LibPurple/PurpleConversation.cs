using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public enum PurpleConversationType
    {
        Unknown = 0,
        IM,
        Chat,
        Misc,
        Any,
    }

    public delegate void ConversationWriteEvent(PurpleConversation conv,
        string who, string alias, string message, int flags, DateTime time);

    public delegate void ReceivedIMMsg(PurpleAccount account, string sender,
        string message, PurpleConversation conv, PurpleMessageFlags flags);

    public delegate void ReceivedChatMsg(PurpleAccount account, string sender,
        string message, PurpleConversation conv, PurpleMessageFlags flags);

    internal delegate void ReceivedIMMsgC(IntPtr account, IntPtr sender,
        IntPtr message, IntPtr conv, PurpleMessageFlags flags);

    internal delegate void ReceivedChatMsgC(IntPtr account, IntPtr sender,
        IntPtr message, IntPtr conv, PurpleMessageFlags flags);

    public class PurpleConversation : PurpleBase
    {
        public static event ConversationWriteEvent OnConversationWrite;
        public static event ReceivedIMMsg OnReceivedIMMsg;
        public static event ReceivedChatMsg OnReceivedChatMsg;

        private static ReceivedIMMsgC received_im_msg_evt;
        private static ReceivedChatMsgC received_chat_msg_evt;

        internal PurpleConversation(IntPtr handle) : base(handle)
        {
        }

        public PurpleConversation(PurpleConversationType type,
            PurpleAccount account, string name)
        {
            handle= purple_conversation_new(type, account.handle, name);
        }

        internal static void Init()
        {
            purple_conversations_init();

            IntPtr handle = purple_conversations_get_handle();

            received_im_msg_evt = new ReceivedIMMsgC(received_im_msg);
            PurpleSignal.Connect(handle, "received-im-msg",
                Marshal.GetFunctionPointerForDelegate(received_im_msg_evt),
                IntPtr.Zero);

            received_chat_msg_evt = new ReceivedChatMsgC(received_chat_msg);
            PurpleSignal.Connect(handle, "received-chat-msg",
                Marshal.GetFunctionPointerForDelegate(received_chat_msg_evt),
                IntPtr.Zero);
        }

        private static void received_im_msg(IntPtr account, IntPtr sender,
            IntPtr message, IntPtr conv, PurpleMessageFlags flags)
        {
            if(OnReceivedIMMsg != null)
            {
                PurpleConversation pconv;
                PurpleAccount pacct = new PurpleAccount(account);
                string sender_str = Marshal.PtrToStringAuto(sender);
                string message_str = Marshal.PtrToStringAuto(message);

                /*We don't really want to pass null pointers around, and libpurple
                  checks to see if a new conversation was created after this event,
                  so we're safe creating our own*/
                if(conv == IntPtr.Zero)
                    pconv = new PurpleConversation(PurpleConversationType.IM,
                        pacct, sender_str);
                else
                    pconv = new PurpleConversation(conv);

                OnReceivedIMMsg(pacct, sender_str,
                    message_str, pconv, flags);
            }
        }

        private static void received_chat_msg(IntPtr account, IntPtr sender,
            IntPtr message, IntPtr conv, PurpleMessageFlags flags)
        {
          if(OnReceivedChatMsg != null)
          {
            PurpleConversation pconv;
            PurpleAccount pacct = new PurpleAccount(account);
            string sender_str = Marshal.PtrToStringAuto(sender);
            string message_str = Marshal.PtrToStringAuto(message);

            if(conv == IntPtr.Zero)
                pconv = new PurpleConversation(PurpleConversationType.Chat,
                    pacct, sender_str);
            else
                pconv = new PurpleConversation(conv);

            OnReceivedChatMsg(pacct, sender_str, message_str, pconv, flags);
          }
        }

        public PurpleAccount Account
        {
            get
            {
                IntPtr ptr = purple_conversation_get_account(handle);
                return new PurpleAccount(ptr);
            }
            set
            {
                 purple_conversation_set_account(handle, value.handle);
            }
        }

        public PurpleConnection Connection
        {
            get
            {
                return new PurpleConnection(purple_conversation_get_gc(handle));
            }
        }

        public string Title
        {
            get
            {
                IntPtr ptr = purple_conversation_get_title(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
            set
            {
                purple_conversation_set_title(handle, value);
            }
        }

        public string Name
        {
            get
            {
                IntPtr ptr = purple_conversation_get_name(handle);
                return Marshal.PtrToStringAuto(ptr);
            }
            set
            {
                purple_conversation_set_name(handle, value);
            }
        }

        public PurpleConversationType Type
        {
            get
            {
                return purple_conversation_get_type(handle);
            }
        }

        public PurpleConvIM IM
        {
            get
            {
                return new PurpleConvIM(purple_conversation_get_im_data(handle));
            }
        }

        public PurpleConvChat Chat
        {
            get
            {
                return new PurpleConvChat(purple_conversation_get_chat_data(handle));
            }
        }
        
        static PurpleConversationUiOps uiops = new PurpleConversationUiOps();
        static IntPtr uiops_ptr;

        public static PurpleConversationUiOps UiOps
        {
            set
            {
                uiops = value;
                uiops_ptr = Marshal.AllocCoTaskMem(Marshal.SizeOf(uiops));
                Marshal.StructureToPtr(uiops, uiops_ptr, false);
                purple_conversations_set_ui_ops(uiops_ptr);
            }
        }

        public static void write_conv(IntPtr conv, IntPtr who,
            IntPtr alias, IntPtr message, int flags, int time)
        {
            if(OnConversationWrite != null)
            {
                string alias_str = Marshal.PtrToStringAuto(alias);
                string who_str = Marshal.PtrToStringAuto(who);
                string message_str = Marshal.PtrToStringAuto(message);
                
                OnConversationWrite(new PurpleConversation(conv), who_str,
                    alias_str, message_str, flags, UnixToDateTime(time));
            }
        }

        public static DateTime UnixToDateTime(int time)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(time);
        }

        public override string ToString()
        {
            return string.Format("PurpleConversation({0:X}) [{1},{2}]", handle, Type, Name);
        }

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_new(PurpleConversationType type,
            IntPtr account, string who);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_account(IntPtr conversation);

        [DllImport("libpurple.dll")]
        static extern void purple_conversation_set_account(IntPtr conversation, IntPtr account);          

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_title(IntPtr conversation);          

        [DllImport("libpurple.dll")]
        static extern void purple_conversation_set_title(IntPtr conversation, string title);          

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_name(IntPtr conversation);          

        [DllImport("libpurple.dll")]
        static extern void purple_conversation_set_name(IntPtr conversation, string name);

        [DllImport("libpurple.dll")]
        static extern PurpleConversationType purple_conversation_get_type(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_im_data(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_chat_data(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversation_get_gc(IntPtr value);

        [DllImport("libpurple.dll")]
        static extern void purple_conversations_init();

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_conversations_get_handle();

        [DllImport("libpurple.dll")]
        static extern void purple_conversations_set_ui_ops(IntPtr value);
    }
}
