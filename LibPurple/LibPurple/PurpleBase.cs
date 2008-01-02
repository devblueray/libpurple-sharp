using System;

namespace KentState.Purple
{
    public class PurpleBase
    {
        internal IntPtr handle;

        internal PurpleBase(IntPtr handle)
        {
            this.handle = handle;
        }

        internal PurpleBase() : this(IntPtr.Zero)
        {
        }

        public override int GetHashCode()
        {
            return handle.GetHashCode();
        }

        public override bool Equals(Object o)
        {
            return o is PurpleBase && this == (PurpleBase)o;
        }

        public static bool operator== (PurpleBase a, PurpleBase b)
        {
            return a.handle == b.handle;
        }

        public static bool operator!= (PurpleBase a, PurpleBase b)
        {
            return !(a == b);
        }
    }
}
