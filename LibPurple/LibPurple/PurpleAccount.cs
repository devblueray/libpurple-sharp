using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
  public class PurpleAccount : PurpleBase
  {
    internal PurpleAccount(IntPtr handle) : base(handle)
    {
    }
    
    public PurpleAccount(string username, string prpl)
    {
      handle = purple_account_new(username, prpl);
    }
    
    public string Username
    {
      get
      {
        IntPtr ptr = purple_account_get_username(handle);
        return Marshal.PtrToStringAuto(ptr);
      }
      set
      {
        purple_account_set_username(handle, value);
      }
    }
    
    public string ProtocolID
    {
      get
      {
        IntPtr ptr = purple_account_get_protocol_id(handle);
        return Marshal.PtrToStringAuto(ptr);
      }
      set
      {
        purple_account_set_protocol_id(handle, value);
      }
    }
    
    public string Password
    {
      get
      {
        IntPtr ptr = purple_account_get_password(handle);
        return Marshal.PtrToStringAuto(ptr);
      }
      set
      {
        purple_account_set_password(handle, value);
      }
    }
    
    public string Alias
    {
      get
      {
        IntPtr ptr = purple_account_get_alias(handle);
        return Marshal.PtrToStringAuto(ptr);
      }
      set
      {
        purple_account_set_alias(handle, value);
      }
    }
    
    public string UserInfo
    {
      get
      {
        IntPtr ptr = purple_account_get_user_info(handle);
        return Marshal.PtrToStringAuto(ptr);
      }
      set
      {
        purple_account_set_user_info(handle, value);
      }
    }
    
    public PurpleConnection Connection
    {
      get
      {
        return new PurpleConnection(purple_account_get_connection(handle));
      }
      set
      {
        purple_account_set_connection(handle, value.handle);
      }
    }
    
    public bool RememberPassword
    {
      get
      {
        int ret = purple_account_get_remember_password(handle);
        return ret == 1 ? true : false;
      }
      set
      {
        int ret = value == true ? 1 : 0;
        purple_account_set_remember_password(handle, ret);
      }
    }

    public bool CheckMail
    {
      get
      {
        int ret = purple_account_get_check_mail(handle);
        return ret == 1 ? true : false;
      }
      set
      {
        int ret = value == true ? 1 : 0;
        purple_account_set_check_mail(handle, ret);
      }
    }
    
    public bool Connected
    {
      get
      {
        int ret = purple_account_is_connected(handle);
        return ret == 1 ? true : false;
      }
    }
    
    public bool Connecting
    {
      get
      {
        int ret = purple_account_is_connecting(handle);
        return ret == 1 ? true : false;
      }
    }
    
    public bool Disconnected
    {
      get
      {
        int ret = purple_account_is_disconnected(handle);
        return ret == 1 ? true : false;
      }
    }
    
    public void SetEnabled(string id, bool value)
    {
      purple_account_set_enabled(handle, id, value == true ? 1 : 0);
    }
    
    public bool GetEnabled(string id)
    {
      int ret = purple_account_get_enabled(handle, id);
      return ret == 1 ? true : false;
    }
    
    public void Connect()
    {
      purple_account_connect(handle);
    }
    
    public void Disconnect()
    {
      purple_account_disconnect(handle);
    }

    public void SetString(string key, string value)
    {
      purple_account_set_string(handle, key, value);
    }

    public void SetInt(string key, int value)
    {
      purple_account_set_int(handle, key, value);
    }

    public void SetBool(string key, bool value)
    {
      purple_account_set_bool(handle, key, value ? 1 : 0);
    }
    
    public override string ToString()
    {
      return string.Format("PurpleAccount({0:X}) [{1}@{2}]", handle, Username, ProtocolID);
    }
    
    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_new(string username, string prpl);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_password(IntPtr account, string password);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_password(IntPtr account);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_enabled(IntPtr account, string name, int value);

    [DllImport("libpurple.dll")]
    static extern int purple_account_get_enabled(IntPtr account, string name);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_username(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_username(IntPtr value, string svalue);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_protocol_id(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_protocol_id(IntPtr value, string svalue);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_alias(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_alias(IntPtr value, string svalue);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_user_info(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_user_info(IntPtr value, string svalue);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_account_get_connection(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_connection(IntPtr value, IntPtr conn);

    [DllImport("libpurple.dll")]
    static extern int purple_account_get_remember_password(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_remember_password(IntPtr value, int ret);

    [DllImport("libpurple.dll")]
    static extern int purple_account_get_check_mail(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_check_mail(IntPtr value, int ret);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_string(IntPtr handle, string key, string value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_int(IntPtr handle, string key, int value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_set_bool(IntPtr handle, string key, int value);

    [DllImport("libpurple.dll")]
    static extern int purple_account_is_connected(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern int purple_account_is_connecting(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern int purple_account_is_disconnected(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_connect(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_account_disconnect(IntPtr value);
  }
}