using System;
using System.Data;
using System.Reflection;
using TauCode.Db.FluentMigrations;

namespace TauCode.Db.Testing
{
    public abstract class DbTestBase
    {
        #region Abstract

        protected abstract IDbConnection CreateDbConnection();
        protected abstract string GetConnectionString();
        protected abstract IDbUtilityFactory GetDbUtilityFactory();

        #endregion

        #region Private

        private IDbConnection GetPreparedDbConnection()
        {
            var connection = this.Connection;

            if (connection == null)
            {
                throw new InvalidOperationException($"'{nameof(Connection)}' is null.");
            }

            if (connection.ConnectionString == null)
            {
                throw new InvalidOperationException(
                    $"'{nameof(Connection)}.{nameof(IDbConnection.ConnectionString)}' is null.");
            }

            if (connection.State == ConnectionState.Closed)
            {
                throw new InvalidOperationException($"'{nameof(Connection)}' is or closed.");
            }

            return connection;
        }

        #endregion

        #region Virtual

        protected virtual string GetSchema() => null;


        protected virtual IDbInspector CreateDbInspector()
        {
            var factory = this.GetDbUtilityFactory();
            var dbInspector = factory.CreateDbInspector(this.GetPreparedDbConnection(), this.GetSchema());
            return dbInspector;
        }

        protected virtual IDbTableInspector CreateTableInspector(string tableName) =>
            this.GetDbUtilityFactory().CreateTableInspector(this.Connection, this.GetSchema(), tableName);

        protected virtual IDbSerializer CreateDbSerializer()
        {
            var factory = this.GetDbUtilityFactory();
            var dbSerializer = factory.CreateDbSerializer(this.GetPreparedDbConnection(), this.GetSchema());
            return dbSerializer;
        }

        protected virtual FluentDbMigrator CreateFluentMigrator(Assembly migrationsAssembly)
        {
            return new FluentDbMigrator(
                this.GetDbUtilityFactory().GetDialect().Name,
                this.GetConnectionString(),
                migrationsAssembly);
        }

        #endregion

        #region Utilities

        protected IDbConnection Connection { get; set; }
        protected IDbInspector DbInspector { get; set; }
        protected IDbSerializer DbSerializer { get; set; }
        protected IDbCruder Cruder { get; set; }
        protected IDbScriptBuilder ScriptBuilder { get; set; }

        #endregion

        #region Fixture Support

        protected virtual void OneTimeSetUpImpl()
        {
            this.Connection = this.CreateDbConnection();
            this.Connection.ConnectionString = this.GetConnectionString();
            this.Connection.Open();

            this.DbInspector = this.CreateDbInspector();
            this.DbSerializer = this.CreateDbSerializer();
            this.Cruder = this.DbSerializer.Cruder;
            this.ScriptBuilder = this.Cruder.ScriptBuilder;
        }

        protected virtual void OneTimeTearDownImpl()
        {
            this.Connection.Dispose();
            this.Connection = null;
        }

        protected virtual void SetUpImpl()
        {
        }

        protected virtual void TearDownImpl()
        {
        }

        #endregion
    }
}
