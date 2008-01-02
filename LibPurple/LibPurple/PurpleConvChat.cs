using System;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
  public class PurpleConvChat : PurpleBase
  {
    internal PurpleConvChat(IntPtr handle) : base(handle)
    {
    }

    public PurpleConversation Conversation
    {
      get
      {
        return new PurpleConversation(purple_conv_chat_get_conversation(handle));
      }
    }

    public int ID
    {
      get
      {
        return purple_conv_chat_get_id(handle);
      }
      set
      {
        purple_conv_chat_set_id(handle, value);
      }
    }

    public void Send(string message)
    {
      purple_conv_chat_send(handle, message);
    }

    public static PurpleConvChat Find(PurpleAccount account, string name)
    {
      IntPtr ptr = purple_find_conversation_with_account(
        PurpleConversationType.Chat, name, account.handle);

      PurpleConversation conv;

      if(ptr == IntPtr.Zero)
        conv = new PurpleConversation(PurpleConversationType.Chat,
                account, name);
      else
        conv = new PurpleConversation(ptr);

      return conv.Chat;
    }
    
    [DllImport("libpurple.dll")]
    static extern IntPtr purple_conv_chat_get_conversation(IntPtr value);

    [DllImport("libpurple.dll")]
    static extern void purple_conv_chat_set_id(IntPtr handle, int value);

    [DllImport("libpurple.dll")]
    static extern int purple_conv_chat_get_id(IntPtr handle);

    [DllImport("libpurple.dll")]
    static extern void purple_conv_chat_send(IntPtr value, string message);

    [DllImport("libpurple.dll")]
    static extern IntPtr purple_find_conversation_with_account(
      PurpleConversationType type, string who, IntPtr account);
  }
}
