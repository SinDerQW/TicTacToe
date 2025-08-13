namespace TicTacToe.Api.Models
{
    public class Move
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public string Player { get; set; } = "";
    }
}
