using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Npgsql;
using Pgvector;

namespace VectorDb.Postgres;

/*
    CREATE EXTENSION IF NOT EXISTS vector;

    CREATE TABLE embeddings
    (
        id uuid PRIMARY KEY,
        embedding vector(1536)
    );
 */

public class VectorStoreService
{
    private readonly NpgsqlDataSource _dataSource;
    private readonly string _tableName;

    public VectorStoreService(string connectionString, string tableName)
    {
        var builder = new NpgsqlDataSourceBuilder(connectionString);
        builder.UseVector();
        _tableName = tableName;

        _dataSource = builder.Build();
    }

    public async Task StoreVectorAsync(Guid id, float[] vector, string file)
    {
        await using var conn = await _dataSource.OpenConnectionAsync();

        const string sql = """
            INSERT INTO faceembeddings (id, embedding, file)
            VALUES (@id, @embedding, @file)
            """;

        await using var cmd = new NpgsqlCommand(sql, conn);

        // cmd.Parameters.AddWithValue("tableName", _tableName);
        cmd.Parameters.AddWithValue("id", id);
        cmd.Parameters.AddWithValue("embedding", new Vector(vector));
        cmd.Parameters.AddWithValue("file", file);

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<float[]?> GetVectorAsync(Guid id)
    {
        await using var conn = await _dataSource.OpenConnectionAsync();

        const string sql = """
            SELECT embedding
            FROM embeddings
            WHERE id = @id
            """;

        await using var cmd = new NpgsqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("id", id);

        var result = await cmd.ExecuteScalarAsync();

        if (result is Vector v)
            return v.Memory.ToArray();

        return null;
    }

    public async Task<List<string>> Search(
        float[] queryVector,
        int limit = 5)
    {
        await using var conn = await _dataSource.OpenConnectionAsync();

        const string sql = """
            SELECT file
            FROM faceembeddings
            ORDER BY embedding <-> @query
            LIMIT @limit
            """;

        await using var cmd = new NpgsqlCommand(sql, conn);

        cmd.Parameters.AddWithValue("query", new Vector(queryVector));
        cmd.Parameters.AddWithValue("limit", limit);

        var result = new List<string>();

        await using var reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            result.Add(reader.GetString(0));
        }

        return result;
    }
}
