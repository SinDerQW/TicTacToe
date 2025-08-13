using Npgsql;
using System.Reflection;
using System.Text.Json;
using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services
{
    public class GameRepository
    {
        private readonly string _connectionString;
        public GameRepository(string connectionString) 
        { 
            _connectionString = connectionString;
        }

        public void SaveGame(Guid id, Game game)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string boardJson = JsonSerializer.Serialize(game.Board);

            using var cmd = new NpgsqlCommand(@"
                INSERT INTO ""SaveInformation"".""InfoTable"" (id, size, board, current_player, is_finished, winner, moves_count)
                VALUES (@id, @size, @board, @current_player, @is_finished, @winner, @moves_count)
                ", conn);

            cmd.Parameters.AddWithValue("id", game.Id);
            cmd.Parameters.AddWithValue("size", game.Size);
            cmd.Parameters.AddWithValue("board", NpgsqlTypes.NpgsqlDbType.Jsonb, boardJson);
            cmd.Parameters.AddWithValue("current_player", game.CurrentPlayer);
            cmd.Parameters.AddWithValue("is_finished", game.IsFinished);
            cmd.Parameters.AddWithValue("winner", (object ?)game.Winner ?? DBNull.Value);
            cmd.Parameters.AddWithValue("moves_count", game.MovesCount);
            cmd.ExecuteNonQuery();

        }
        public Game LoadGame(Guid id)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            using var cmd = new NpgsqlCommand(@"
                SELECT size, board, current_player, is_finished, winner, moves_count
                FROM  ""SaveInformation"".""InfoTable"" 
                WHERE id = @id
                ", conn);

            cmd.Parameters.AddWithValue("id", id);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read()) throw new Exception("Game not found");

            var game = new Game();
            
            game.Id = id;
            game.Size = reader.GetInt32(0);
            game.Board = JsonSerializer.Deserialize<string[][]>(reader.GetString(1));
            game.CurrentPlayer = reader.GetString(2);
            game.IsFinished = reader.GetBoolean(3);
            game.Winner = reader.IsDBNull(4) ? null : reader.GetString(4);
            game.MovesCount = reader.GetInt32(5);

            return game;    
        }

        public void UpdateGame(Guid id, Game game)
        {
            using var conn = new NpgsqlConnection(_connectionString);
            conn.Open();

            string boardJson = JsonSerializer.Serialize(game.Board);

            using var cmd = new NpgsqlCommand(@"
                UPDATE ""SaveInformation"".""InfoTable""
                SET board = @board,
                    current_player = @current_player,
                    is_finished = @is_finished,
                    winner = @winner,
                    moves_count = @moves_count
                WHERE id = @id
                ", conn);

            cmd.Parameters.AddWithValue("id", game.Id);
            cmd.Parameters.AddWithValue("size", game.Size);
            cmd.Parameters.AddWithValue("board", NpgsqlTypes.NpgsqlDbType.Jsonb, boardJson);
            cmd.Parameters.AddWithValue("current_player", game.CurrentPlayer);
            cmd.Parameters.AddWithValue("is_finished", game.IsFinished);
            cmd.Parameters.AddWithValue("winner", (object?)game.Winner ?? DBNull.Value);
            cmd.Parameters.AddWithValue("moves_count", game.MovesCount);
            cmd.ExecuteNonQuery();
        }
    }
}
