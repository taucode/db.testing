using System;
using System.IO;
using TauCode.Db.SQLite;

namespace TauCode.Db.Testing
{
    public class SQLiteTestHelper : IDisposable
    {
        public SQLiteTestHelper()
        {
            var tuple = SQLiteTools.CreateSQLiteDatabase();

            this.TempDbFilePath = tuple.Item1;
            this.ConnectionString = tuple.Item2;
        }

        public string TempDbFilePath { get; }
        public string ConnectionString { get; }

        public void Dispose()
        {
            try
            {
                File.Delete(this.TempDbFilePath);
            }
            catch
            {
                // silently dismiss
            }
        }
    }
}
