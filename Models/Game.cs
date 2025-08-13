using System.Drawing;
using System.Linq.Expressions;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;

namespace TicTacToe.Api.Models
{
    public class Game
    {
        public int Size { get; set; }

        public string[][] Board { get; set; } 

        public string CurrentPlayer { get; set; } = "X";

        public Guid Id { get; set; } = Guid.NewGuid();

        public bool IsFinished { get; set; }

        public string? Winner { get; set; }

        public int MovesCount { get; set; } = 0;

        public void InitializeBoard()
        {
            Board = new string[Size][];
            
            for(int i = 0; i<Size; i++)
            {
                Board[i] = new string[Size];
                for (int j = 0; j< Size; j++)
                {
                    Board[i][j] = "";
                }
               
            }
        }

      
           public bool CheckWin(string symbol)
        {
            // Горизонтали
            for (int row = 0; row < Size; row++)
            {
                bool win = true;
                for (int col = 0; col < Size; col++)
                {
                    if (Board[row][col] != symbol)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }

            // Вертикали
            for (int col = 0; col < Size; col++)
            {
                bool win = true;
                for (int row = 0; row < Size; row++) // ← исправлено col++ → row++
                {
                    if (Board[row][col] != symbol)
                    {
                        win = false;
                        break;
                    }
                }
                if (win) return true;
            }

            // Главная диагональ
            bool mainDiagonalWin = true;
            for (int i = 0; i < Size; i++)
            {
                if (Board[i][i] != symbol)
                {
                    mainDiagonalWin = false;
                    break;
                }
            }
            if (mainDiagonalWin) return true;

            // Побочная диагональ
            bool secondaryDiagonalWin = true;
            for (int i = 0; i < Size; i++)
            {
                if (Board[i][Size - 1 - i] != symbol)
                {
                    secondaryDiagonalWin = false;
                    break;
                }
            }
            if (secondaryDiagonalWin) return true;

            return false;
        }
        


        public bool isDraw()
        {
             if (string.IsNullOrEmpty(Winner)) return false;

            for(int row = 0; row < Size; row++)
            {
                for (int col = 0; col < Size; col++) 
                {
                    if (Board[row][col] == "") return false;
                }
            }
            return true;
        }
    }
}
