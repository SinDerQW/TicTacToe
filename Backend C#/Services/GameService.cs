using TicTacToe.Api.Models;

namespace TicTacToe.Api.Services
{
    public class GameService
    {
        private readonly Dictionary<Guid, Game> _games = new ();
        GameRepository gameRepository = new GameRepository("Host=localhost;Port=5432;Database=GameSave;Username=postgres;Password=12345\r\n");

        public Game CreateGame(int size)
        {

            if (size < 1 || size > 100) // можно задать свой разумный лимит
                throw new ArgumentException("Size must be >= 1");

            var game = new Game();
            game.Size = size;
            game.InitializeBoard();

            //_games.Add(game.Id, game);
            gameRepository.SaveGame(game.Id,  game);


            return game;
        }
       
        public Game? GetGame(Guid id)
        {
            
            var game = gameRepository.LoadGame(id);
            if (game == null) return null;
            return game;
            //return _games.TryGetValue(id, out var game) ? game : null;
        }

        
        public Game? MakeMove(Guid gameId, Move move)
        {
            // Получение игры через ID
            var game = GetGame(gameId);

            // Проверка, на то что игра существует и не закончена
            if (game == null || game.IsFinished == true) return null;

            // Возможен ли такое ход впринципе
            if (move.Row < 0 || move.Row >= game.Size || move.Column < 0 || move.Column >= game.Size) return null;

            // Есть ли значение в этой клетке
            if (!string.IsNullOrEmpty(game.Board[move.Row][move.Column])) return null; 
             
            // Назначение хода игрока
            string symbolToPlace = move.Player;
            if (string.IsNullOrEmpty(symbolToPlace)) return null;
            if (symbolToPlace != game.CurrentPlayer) return null;

            // Каждый 3-й ход — с вероятностью 10% поменять символ на противоположный
            Random rnd = new Random();
            if (game.MovesCount > 0 && game.MovesCount % 3 == 0 && rnd.NextDouble() < 0.1) symbolToPlace = symbolToPlace == "X" ? "O" : "X";

            // Устанавливаем значение на доску
            game.Board[move.Row][move.Column] = symbolToPlace;

            // Проверка победы

            if (game.CheckWin(symbolToPlace))
            {
                game.Winner = symbolToPlace;
                game.IsFinished = true;
            }else if (game.isDraw())
            {
                game.IsFinished = true;
                game.Winner = null;
            }

            // Переключаем игрока (если ход сделал X — следующий O, и наоборот)в
            game.CurrentPlayer = symbolToPlace == "X" ? "O" : "X";

            game.MovesCount++;

            gameRepository.UpdateGame(gameId, game);

            return game;
        }

        
    }
}
