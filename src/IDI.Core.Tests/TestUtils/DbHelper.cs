using System;
using System.Data.SqlClient;
using IDI.Core.Common;

namespace IDI.Core.Tests.TestUtils
{
    public static class DbHelper
    {
        private static string _connectionString = Contants.ConnectionStrings.EFTestDb;

        public static void SetConnectionString(string connectionString)
        {
            _connectionString = connectionString;
        }

        //public static void ClearEFTestDb()
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(Contants.ConnectionStrings.EFTestDb))
        //        {
        //            conn.Open();
        //            List<string> tablesToClear = new List<string> { Contants.EFTables.EFUsers };
        //            foreach (var table in tablesToClear)
        //            {
        //                using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM {0}", table), conn))
        //                {
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch { }
        //}

        //public static int ReadRecordCountFromEFTestDB(string table)
        //{
        //    int ret = 0;
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(Contants.ConnectionStrings.EFTestDb))
        //        {
        //            conn.Open();
        //            using (SqlCommand cmd = new SqlCommand(string.Format("SELECT COUNT(*) FROM {0}", table), conn))
        //            {
        //                ret = Convert.ToInt32(cmd.ExecuteScalar());
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch { }
        //    return ret;
        //}

        //public static void ExecuteNonQueryEFTestDb(string cmdText)
        //{
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(Contants.ConnectionStrings.EFTestDb))
        //        {
        //            conn.Open();
        //            List<string> tablesToClear = new List<string> { Contants.EFTables.EFUsers };
        //            foreach (var table in tablesToClear)
        //            {
        //                using (SqlCommand command = new SqlCommand(cmdText, conn))
        //                {
        //                    command.ExecuteNonQuery();
        //                }
        //            }
        //            conn.Close();
        //        }
        //    }
        //    catch { }
        //}

        public static void Clear(params string[] tables)
        {
            if (tables.IsNullOrEmpty())
                return;

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    foreach (var table in tables)
                    {
                        using (SqlCommand command = new SqlCommand(string.Format("DELETE FROM {0}", table), conn))
                        {
                            command.ExecuteNonQuery();
                        }
                    }
                    conn.Close();
                }
            }
            catch { }
        }

        public static int ReadRecordCount(string table)
        {
            int count = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(string.Format("SELECT COUNT(*) FROM {0}", table), conn))
                    {
                        count = Convert.ToInt32(cmd.ExecuteScalar());
                    }
                    conn.Close();
                }
            }
            catch { }
            return count;
        }

        public static void ExecuteNonQuery(string cmdText)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand(cmdText, conn))
                    {
                        command.ExecuteNonQuery();
                    }

                    conn.Close();
                }
            }
            catch { }
        }

        public static void ExecuteNonQuery(string[] cmdTexts)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    using (SqlCommand command = new SqlCommand())
                    {
                        command.Connection = conn;

                        foreach (var cmdText in cmdTexts)
                        {
                            command.CommandText = cmdText;
                            command.ExecuteNonQuery();
                        }
                    }

                    conn.Close();
                }
            }
            catch { }
        }
    }
}
