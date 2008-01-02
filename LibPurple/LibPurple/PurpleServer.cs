using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
  public class PurpleServer
  {
    private PurpleConnection connection;

    public PurpleServer(PurpleConnection connection)
    {
      this.connection = connection;
    }

    /*
    public void SendTyping(PurpleTypingState state, string name)
    {
      serv_send_typing(connection.handle, name, state);
    }

    public void GetInfo(string name)
    {
      serv_get_info(connection.handle, name);
    }

    public void SetInfo(string message)
    {
      serv_set_info(connection.handle, message);
    }

    public void AddPermit(string name)
    {
      serv_add_permit(connection.handle, name);
    }

    public void AddDeny(string name)
    {
      serv_add_deny(connection.handle, name);
    }

    public void RemovePermit(string name)
    {
      serv_rem_permit(connection.handle, name);
    }

    public void RemoveDeny(string name)
    {
      serv_rem_deny(connection.handle, name);
    }

    public void PermitDeny()
    {
      serv_set_permit_deny(connection.handle);
    }

    public void ChatInvite(int id, string name, string message)
    {
      serv_chat_invite(connection.handle, id, name, message);
    }

    public void ChatLeave(int id)
    {
      serv_chat_leave(connection.handle, id);
    }
    //*/

    public void JoinChat(Dictionary<string, string> hash)
    {
      GStrHashTable ghash = new GStrHashTable(hash);
      serv_join_chat(connection.handle, ghash.handle);
    }

    [DllImport("libpurple.dll")]
    static extern void serv_join_chat(IntPtr handle, IntPtr hash);
  }
}
