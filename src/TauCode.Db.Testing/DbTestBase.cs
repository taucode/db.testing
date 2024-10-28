using System.Data;

namespace TauCode.Db.Testing;

public abstract class DbTestBase
{
    #region Abstract

    protected abstract string GetConnectionString();

    //protected abstract IDbUtilityFactory GetDbUtilityFactory();

    protected abstract void TuneConnection(IDbConnection connection);

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

    //protected virtual string GetSchemaName() => null;

    //protected virtual IDbConnection CreateConnection() => this.GetDbUtilityFactory().CreateConnection();

    //protected virtual IDbInspector CreateDbInspector()
    //{
    //    var factory = this.GetDbUtilityFactory();
    //    var dbInspector = factory.CreateInspector(this.GetPreparedDbConnection(), this.GetSchemaName());
    //    return dbInspector;
    //}

    //protected virtual IDbTableInspector CreateTableInspector(string tableName) =>
    //    this.GetDbUtilityFactory().CreateTableInspector(this.Connection, this.GetSchemaName(), tableName);

    //protected virtual IDbSerializer CreateDbSerializer()
    //{
    //    var factory = this.GetDbUtilityFactory();
    //    var dbSerializer = factory.CreateSerializer(this.GetPreparedDbConnection(), this.GetSchemaName());
    //    return dbSerializer;
    //}

    #endregion

    #region Utilities

    protected IDbConnection Connection { get; set; }
    //protected IDbInspector DbInspector { get; set; }
    //protected IDbSerializer DbSerializer { get; set; }
    //protected IDbCruder Cruder { get; set; }
    //protected IDbScriptBuilder ScriptBuilder { get; set; }

    #endregion

    #region Fixture Support

    protected virtual void OneTimeSetUpImpl()
    {
        throw new NotImplementedException();

        //this.Connection = this.CreateConnection();

        //if (this.Connection == null)
        //{
        //    throw new InvalidOperationException($"'{nameof(CreateConnection)}' returned null.");
        //}

        //var connectionString = this.GetConnectionString();

        //if (connectionString == null)
        //{
        //    throw new InvalidOperationException($"'{nameof(GetConnectionString)}' returned null.");
        //}

        //this.Connection.ConnectionString = connectionString;
        //this.Connection.Open();

        //this.TuneConnection(this.Connection);

        //this.DbInspector = this.CreateDbInspector();
        //this.DbSerializer = this.CreateDbSerializer();
        //this.Cruder = this.DbSerializer.Cruder;
        //this.ScriptBuilder = this.Cruder.ScriptBuilder;
    }

    protected virtual void OneTimeTearDownImpl()
    {
        this.Connection?.Dispose();
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