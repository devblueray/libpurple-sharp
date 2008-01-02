#include <glib.h>

GHashTable *
purplesharp_str_hash_new()
{
  return g_hash_table_new(g_str_hash, g_str_equal);
}

void
purplesharp_str_hash_destroy(GHashTable *table)
{
  g_hash_table_destroy(table);
}

void
purplesharp_str_hash_insert(GHashTable *table, const char *key, const char *value)
{
  g_hash_table_insert(table, g_strdup(key), g_strdup(value));
}
