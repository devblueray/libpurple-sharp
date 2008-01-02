using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurpleConversationEnum : IEnumerator
    {
        GList list = null;
        GList last = null;
        bool unmoved = true;
        
        internal PurpleConversationEnum(GList list)
        {
            this.list = list;
        }

        public bool MoveNext()
        {
            if(unmoved)
            {
                last = list;
                unmoved = false;
            }
            else
            {
                if(last.next == IntPtr.Zero)
                    return false;

                last = (GList)Marshal.PtrToStructure(last.next, typeof(GList));
            }

            if(last == null || last.data == IntPtr.Zero)
                return false;
            else
                return true;
        }

        public void Reset()
        {
            last = null;
            unmoved = true;
        }

        public object Current
        {
            get
            {
                return new PurpleConversation(last.data);
            }
        }
    }

    public class PurpleConversations : IEnumerable
    {
        internal GList list;

        internal PurpleConversations(GList list)
        {
            this.list = list;
        }

        public IEnumerator GetEnumerator()
        {
            return new PurpleConversationEnum(list);
        }

        public static PurpleConversations Conversations
        {
            get
            {
                IntPtr ptr = purple_get_conversations();
                if( ptr == IntPtr.Zero )
                {
                    return null;
                }
                GList list = (GList)Marshal.PtrToStructure(ptr, typeof(GList));
                return new PurpleConversations(list);
            }
        }

        [DllImport("libpurple.dll")]
        static extern IntPtr purple_get_conversations();
    }
}
