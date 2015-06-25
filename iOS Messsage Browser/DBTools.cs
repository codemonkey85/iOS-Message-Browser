using System;
using System.Data;
using System.Data.Common;
using System.Text;

namespace iOS_Messsage_Browser
{
    internal class DBTools
    {
        private static DbConnection con;
        private static DataTable _MessagesTable;

        public static void OpenDB(string DBFile)
        {
            if (con != null) return;
            try
            {
                var connString = string.Format(@"Data Source=""{0}""; Pooling=false; FailIfMissing=false;", DBFile);
                using (var factory = new System.Data.SQLite.SQLiteFactory())
                    con = factory.CreateConnection();
                con.ConnectionString = connString;
                con.Open();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error opening database: {0}", ex.Message);
            }
        }

        public static void CloseDB()
        {
            try
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error closing database: {0}", ex.Message);
            }
        }

        public static DataTable GetMessagesTable
        {
            get
            {
                if (con == null) return null;
                if (con.State != ConnectionState.Open) return null;
                if (_MessagesTable != null) return _MessagesTable;
                var sbSQL = new StringBuilder();

                sbSQL.AppendFormat(
                    "SELECT datetime(m.date + strftime('%s', '2001-01-01 00:00:00'), 'unixepoch', 'localtime') as date, m.is_from_me, h.id, m.text FROM message m, handle h WHERE m.handle_id = h.rowid"
                );

                using (var cmd = con.CreateCommand())
                {
                    cmd.CommandText = string.Format(sbSQL.ToString());
                    _MessagesTable = new DataTable();
                    _MessagesTable.Load(cmd.ExecuteReader());
                }
                return _MessagesTable;
            }
        }

        public enum MessageColumns
        {
            ROWID,
            guid,
            text,
            replace,
            service_center,
            handle_id,
            subject,
            country,
            attributedBody,
            version,
            type,
            service,
            account,
            account_guid,
            error,
            date,
            date_read,
            date_delivered,
            is_delivered,
            is_finished,
            is_emote,
            is_from_me,
            is_empty,
            is_delayed,
            is_auto_reply,
            is_prepared,
            is_read,
            is_system_message,
            is_sent,
            has_dd_results,
            is_service_message,
            is_forward,
            was_downgraded,
            is_archive,
            cache_has_attachments,
            cache_roomnames,
            was_data_detected,
            was_deduplicated,
            is_audio_message,
            is_played,
            date_played,
            item_type,
            other_handle,
            group_title,
            group_action_type,
            share_status,
            share_direction,
            is_expirable,
            expire_state,
            message_action_type,
            message_source
        }
    }
}