using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
  public class PurpleAccountEnum : IEnumerator
  {
    GList list = null;
    GList last = null;
    bool unmoved = true;
    
    internal PurpleAccountEnum(GList list)
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
        return new PurpleAccount(last.data);
      }
    }
  }
  
  public class PurpleAccounts : IEnumerable
  {
    internal GList list;
    internal PurpleAccounts(GList list)
    {
      this.list = list;
    }
    
    public IEnumerator GetEnumerator()
    {
      return new PurpleAccountEnum(list);
    }
    
    public static PurpleAccounts Accounts
    {
      get
      {
        IntPtr ptr = purple_accounts_get_all();
        if( ptr == IntPtr.Zero )
        {
          return null;
        }
        GList list = (GList)Marshal.PtrToStructure(ptr, typeof(GList));
        return new PurpleAccounts(list);
      }
    }
    
    public static PurpleAccounts Active
    {
      get
      {
        IntPtr ptr = purple_accounts_get_all_active();
        if( ptr == IntPtr.Zero )
        {
          return null;
        }
        GList list = (GList)Marshal.PtrToStructure(ptr, typeof(GList));
        return new PurpleAccounts(list);
      }
    }
    
    [DllImport("libpurple.dll")]
    static extern IntPtr purple_accounts_get_all();
   
    [DllImport("libpurple.dll")]
    static extern IntPtr purple_accounts_get_all_active();
  }
}