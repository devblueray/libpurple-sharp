using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
    public class PurplePluginEnum : IEnumerator
    {
        GList list = null;
        GList last = null;
        bool unmoved = true;
        
        internal PurplePluginEnum(GList list)
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
                return new PurplePlugin(last.data);
            }
        }
    }

    public class PurplePlugins : IEnumerable
    {
        internal GList list;

        internal PurplePlugins(GList list)
        {
            this.list = list;
        }

        public IEnumerator GetEnumerator()
        {
            return new PurplePluginEnum(list);
        }
    }
}
