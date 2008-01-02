using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace KentState.Purple
{
  public class GStrHashTable : PurpleBase
  {
    internal GStrHashTable(IntPtr handle) : base(handle) {}

    public GStrHashTable()
    {
      handle = purplesharp_str_hash_new();
    }

    public GStrHashTable(Dictionary<string, string> dictionary)
    {
      handle = purplesharp_str_hash_new();
      foreach(KeyValuePair<string, string> kvp in dictionary)
        Insert(kvp.Key, kvp.Value);
    }

    ~GStrHashTable()
    {
      purplesharp_str_hash_destroy(handle);
    }

    public void Insert(string key, string value)
    {
      purplesharp_str_hash_insert(handle, key, value);
    }

    [DllImport("libpurple-sharp-glue.dll")]
    static extern IntPtr purplesharp_str_hash_new();

    [DllImport("libpurple-sharp-glue.dll")]
    static extern void purplesharp_str_hash_destroy(IntPtr value);

    [DllImport("libpurple-sharp-glue.dll")]
    static extern void purplesharp_str_hash_insert(IntPtr handle, string key, string value);
  }
}
