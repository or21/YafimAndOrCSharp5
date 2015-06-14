//-----------------------------------------------------------------------
// <copyright file="GameManager.cs" company="GameLogic">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

namespace GameLogic
{
    /// <summary>
    /// Manage entire game
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Coin matrix holds the current state of the game
        /// </summary>
        private readonly Coin[,] m_GameBoard;

        /// <summary>
        /// Size of the board
        /// </summary>
        private readonly int m_Size;

        /// <summary>
        /// Set the players
        /// </summary>
        private Player m_PlayerOne, m_PlayerTwo;

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="i_Size"> Size of the board</param>
        /// <param name="i_NumberOfPlayers"> Number of human players </param>
        /// <param name="i_PlayerOneName">Name of the first player</param>
        /// <param name="i_PlayerTwoName">Name of the second player ("Comp" if computer)</param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName)
        {
            this.m_Size = i_Size;
            this.m_GameBoard = new Coin[i_Size, i_Size];
            this.m_PlayerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
            this.m_PlayerTwo = (i_NumberOfPlayers == 2)
                ? new Player(false, Coin.O, i_PlayerTwoName, i_Size)
                : new Player(true, Coin.O, i_PlayerTwoName, i_Size);
        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        public void SetNewGame()
        {
            initBoardForNewGame();

            // Place 4 coins in board
            int halfBoard = (m_Size / 2) - 1;
            m_GameBoard[halfBoard + 1, halfBoard] = Coin.X;
            m_GameBoard[halfBoard, halfBoard + 1] = Coin.X;

            m_GameBoard[halfBoard, halfBoard] = Coin.O;
            m_GameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;

            // Update availble moves for each player
            Utils.UpadteAvailableMoves(this, m_PlayerOne);
            Utils.UpadteAvailableMoves(this, m_PlayerTwo);
        }

        /// <summary>
        /// Initialize new game board with no coins
        /// </summary>
        private void initBoardForNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < m_Size; x++)
            {
                for (int y = 0; y < m_Size; y++)
                {
                    m_GameBoard[x, y] = Coin.Null;
                }
            }
        }

        /// <summary>
        /// Gets size of the board
        /// </summary>
        public int Size
        {
            get { return m_Size; }
        }

        /// <summary>
        /// Current Coin in specific cell
        /// </summary>
        /// <param name="i_I"> Coordinate i </param>
        /// <param name="i_J"> Coordinate j </param>
        /// <returns>Coin value</returns>
        public Coin this[int i_I, int i_J]
        {
            get { return m_GameBoard[i_I, i_J]; }
            set { m_GameBoard[i_I, i_J] = value; }
        }

        /// <summary>
        /// Print final results of the game.
        /// </summary>
        public string PrintResult(GameManager i_GameManager)
        {
            string result;

            // Count points for each player
            Utils.CountPoints(i_GameManager, ref m_PlayerOne, ref m_PlayerTwo);

            int currentPlayerPoints = m_PlayerOne.Points;
            int otherPlayerPoints = m_PlayerTwo.Points;
            string score = string.Format("({0}/{1})", m_PlayerOne.Points, m_PlayerTwo.Points);

            if (currentPlayerPoints == otherPlayerPoints)
            {
                result = string.Format(@"It's a tie! {0}", score);
            }
            else
            {
                // The winner is the one with more coins
                Player winner = (currentPlayerPoints > otherPlayerPoints) ? m_PlayerOne : m_PlayerTwo;
                winner.NumberOfWinnings++;
                result = string.Format("{0} Won!! {1} ({2}/{3})", winner.Name, score, m_PlayerOne.NumberOfWinnings, m_PlayerTwo.NumberOfWinnings);
            }

            return result;
        }

        /* GETTERS AND SETTERS */

        /// <summary>
        /// Gets or sets game board
        /// </summary>
        public Coin[,] GameBoard
        {
            get { return this.m_GameBoard; }
        }

        /// <summary>
        /// Gets or sets player two
        /// </summary>
        public Player PlayerTwo
        {
            get { return m_PlayerTwo; }  
        }

        /// <summary>
        /// Gets or sets player one
        /// </summary>
        public Player PlayerOne
        {
            get { return m_PlayerOne; }
        }
    }

    /// <summary>
    /// Represents 3 coin states
    /// </summary>
    public enum Coin
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