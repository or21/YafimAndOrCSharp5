//-----------------------------------------------------------------------
// <copyright file="GameBoard.cs" company="GameLogic">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

namespace GameLogic
{
    /// <summary>
    /// Manage entire game
    /// </summary>
    public class GameBoard
    {
        /// <summary>
        /// eCoin matrix holds the current state of the game
        /// </summary>
        private readonly eCoin[,] r_Board;

        /// <summary>
        /// Size of the board
        /// </summary>
        private readonly int r_Size;

        /// <summary>
        /// Set player one
        /// </summary>
        private readonly Player r_PlayerOne;

        /// <summary>
        /// Set player two
        /// </summary>
        private readonly Player r_PlayerTwo;

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="i_Size"> Size of the board</param>
        /// <param name="i_NumberOfPlayers"> Number of human players </param>
        /// <param name="i_PlayerOneName">Name of the first player</param>
        /// <param name="i_PlayerTwoName">Name of the second player ("Comp" if computer)</param>
        public GameBoard(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName)
        {
            this.r_Size = i_Size;
            this.r_Board = new eCoin[i_Size, i_Size];
            this.r_PlayerOne = new Player(false, eCoin.X, i_PlayerOneName, i_Size);
            this.r_PlayerTwo = (i_NumberOfPlayers == 2)
                ? new Player(false, eCoin.O, i_PlayerTwoName, i_Size)
                : new Player(true, eCoin.O, i_PlayerTwoName, i_Size);
        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        public void SetNewGame()
        {
            initBoardForNewGame();

            // Place 4 coins in board
            int halfBoard = (r_Size / 2) - 1;
            r_Board[halfBoard + 1, halfBoard] = eCoin.X;
            r_Board[halfBoard, halfBoard + 1] = eCoin.X;

            r_Board[halfBoard, halfBoard] = eCoin.O;
            r_Board[halfBoard + 1, halfBoard + 1] = eCoin.O;

            // Update availble moves for each player
            Utils.UpadteAvailableMoves(this, r_PlayerOne);
            Utils.UpadteAvailableMoves(this, r_PlayerTwo);
        }

        /// <summary>
        /// Initialize new game board with no coins
        /// </summary>
        private void initBoardForNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < r_Size; x++)
            {
                for (int y = 0; y < r_Size; y++)
                {
                    r_Board[x, y] = eCoin.Null;
                }
            }
        }

        /// <summary>
        /// Gets size of the board
        /// </summary>
        public int Size
        {
            get { return r_Size; }
        }

        /// <summary>
        /// Current eCoin in specific cell
        /// </summary>
        /// <param name="i_I"> Coordinate i </param>
        /// <param name="i_J"> Coordinate j </param>
        /// <returns>eCoin value</returns>
        public eCoin this[int i_I, int i_J]
        {
            get { return r_Board[i_I, i_J]; }
            set { r_Board[i_I, i_J] = value; }
        }

        /// <summary>
        /// Print final results of the game.
        /// </summary>
        /// <param name="i_GameManager">Instance of Game Manger</param>
        /// <returns>The result</returns>
        public string GetFinalResult(GameBoard i_GameManager)
        {
            string result;

            // Count points for each player
            Utils.CountPoints(i_GameManager, r_PlayerOne, r_PlayerTwo);

            int currentPlayerPoints = r_PlayerOne.Points;
            int otherPlayerPoints = r_PlayerTwo.Points;
            string score = string.Format("({0}/{1})", r_PlayerOne.Points, r_PlayerTwo.Points);

            if (currentPlayerPoints == otherPlayerPoints)
            {
                result = string.Format(@"It's a tie! {0}", score);
            }
            else
            {
                // The winner is the one with more coins
                Player winner = (currentPlayerPoints > otherPlayerPoints) ? r_PlayerOne : r_PlayerTwo;
                winner.NumberOfWinnings++;
                result = string.Format("{0} Won!! {1} ({2}/{3})", winner.Name, score, r_PlayerOne.NumberOfWinnings, r_PlayerTwo.NumberOfWinnings);
            }

            return result;
        }

        /* GETTERS AND SETTERS */

        /// <summary>
        /// Gets game board
        /// </summary>
        public eCoin[,] Board
        {
            get { return this.r_Board; }
        }

        /// <summary>
        /// Gets player two
        /// </summary>
        public Player PlayerTwo
        {
            get { return r_PlayerTwo; }  
        }

        /// <summary>
        /// Gets player one
        /// </summary>
        public Player PlayerOne
        {
            get { return r_PlayerOne; }
        }
    }

    /// <summary>
    /// Represents 3 coin states
    /// </summary>
    public enum eCoin
    {
        /// <summary>
        /// Black coin
        /// </summary>
        X,

        /// <summary>
        /// White coin
        /// </summary>
        O,

        /// <summary>
        /// No coin
        /// </summary>
        Null
    }
}