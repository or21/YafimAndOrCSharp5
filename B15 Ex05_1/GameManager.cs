//-----------------------------------------------------------------------
// <copyright file="GameManager.cs" company="B15_Ex02">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System;

namespace B15_Ex05_1
{
    /// <summary>
    /// Manage entire game
    /// </summary>
    public class GameManager
    {
        /// <summary>
        /// Set the players
        /// </summary>
        private Player m_playerOne, m_playerTwo;

        /// <summary>
        /// Coin matrix holds the current state of the game
        /// </summary>
        private Coin[,] m_gameBoard;

        /// <summary>
        /// Size of the board
        /// </summary>
        private int m_size;

        /// <summary>
        /// Controls and manages the running game
        /// </summary>
        private GameManager m_gameManager;

        /// <summary>
        /// Initializes a new instance of the GameManager class.
        /// </summary>
        /// <param name="i_Size"> Size of the board</param>
        /// <param name="i_NumberOfPlayers"> Number of human players </param>
        /// <param name="i_PlayerOneName">Name of the first player</param>
        /// <param name="i_PlayerTwoName">Name of the second player ("Comp" if computer)</param>
        /// <param name="i_InitGame">Start new game</param>
        public GameManager(int i_Size, int i_NumberOfPlayers, string i_PlayerOneName, string i_PlayerTwoName,
            bool i_InitGame)
        {
            this.m_size = i_Size;
            this.m_gameBoard = new Coin[i_Size, i_Size];
            this.m_playerOne = new Player(false, Coin.X, i_PlayerOneName, i_Size);
            this.m_playerTwo = (i_NumberOfPlayers == 2)
                ? new Player(false, Coin.O, i_PlayerTwoName, i_Size)
                : new Player(true, Coin.O, i_PlayerTwoName, i_Size);

            if (i_InitGame)
            {
                setNewGame();
            }
        }

        /// <summary>
        /// Initialize a new game.
        /// </summary>
        private void setNewGame()
        {
            initBoardForNewGame();

            // Place 4 coins in board
            int halfBoard = (m_size / 2) - 1;
            m_gameBoard[halfBoard + 1, halfBoard] = Coin.X;
            m_gameBoard[halfBoard, halfBoard + 1] = Coin.X;

            m_gameBoard[halfBoard, halfBoard] = Coin.O;
            m_gameBoard[halfBoard + 1, halfBoard + 1] = Coin.O;

            // Update availble moves for each player
            Utils.UpadteAvailableMoves(this, ref m_playerOne);
            Utils.UpadteAvailableMoves(this, ref m_playerTwo);
        }

        /// <summary>
        /// Initialize new game board with no coins
        /// </summary>
        private void initBoardForNewGame()
        {
            // Set all board to null.
            for (int x = 0; x < m_size; x++)
            {
                for (int y = 0; y < m_size; y++)
                {
                    m_gameBoard[x, y] = Coin.Null;
                }
            }
        }

        /// <summary>
        /// Gets size of the board
        /// </summary>
        public int Size
        {
            get { return m_size; }
        }

        /// <summary>
        /// Current Coin in specific cell
        /// </summary>
        /// <param name="i_I"> Coordinate i </param>
        /// <param name="i_J"> Coordinate j </param>
        /// <returns>Coin value</returns>
        public Coin this[int i_I, int i_J]
        {
            get { return m_gameBoard[i_I, i_J]; }
            set { m_gameBoard[i_I, i_J] = value; }
        }

        /// <summary>
        /// Runs the game
        /// </summary>
        public void RunGame()
        {
            // Some flags
            bool playerOneTurn = true;
            bool isGameOver = false;
            m_gameManager = this;

            // Players to play
            Player otherPlayer = m_playerTwo;

            // Ex02.ConsoleUtils.Screen.Clear();
            Drawer.DrawBoard(this);

            while (!isGameOver)
            {
                // Check whose turn now
                Player currentPlayer = playerOneTurn ? m_playerOne : m_playerTwo;

                isGameOver = currentPlayerMove(currentPlayer, ref isGameOver, ref otherPlayer);

                // Switch turns for next move.
                if (!isGameOver)
                {
                    playerOneTurn = !playerOneTurn;
                    otherPlayer = currentPlayer;
                }
            }
        }

        /// <summary>
        /// Check if players can make moves
        /// </summary>
        /// <param name="i_CurrentPlayer">Current player</param>
        /// <param name="io_IsGameOver">Update if game over</param>
        /// <param name="io_OtherPlayer">Other player</param>
        /// <returns>True if game over, Otherwise false</returns>
        private bool currentPlayerMove(Player i_CurrentPlayer, ref bool io_IsGameOver, ref Player io_OtherPlayer)
        {
            // Check if current player can move
            bool playerCanMove = i_CurrentPlayer.AvailableMoves != 0;

            if (playerCanMove)
            {
                io_IsGameOver = makePlayerMove(i_CurrentPlayer, io_IsGameOver, ref io_OtherPlayer);
            }
            else
            {
                // Other player can move, Otherwise No moves left so end current game.
                if (io_OtherPlayer.AvailableMoves != 0)
                {
                    Console.WriteLine("No move left for {0}!", i_CurrentPlayer.Name);
                }
                else
                {
                    io_IsGameOver = true;
                }
            }

            return io_IsGameOver;
        }

        /// <summary>
        /// Make a move for current player
        /// </summary>
        /// <param name="i_CurrentPlayer">Current player</param>
        /// <param name="i_IsGameOver">Update if game over</param>
        /// <param name="i_OtherPlayer">Other player</param>
        /// <returns>True if game over, Otherwise false</returns>
        private bool makePlayerMove(Player i_CurrentPlayer, bool i_IsGameOver, ref Player i_OtherPlayer)
        {
            int newX;
            int newY;

            // Computer's turn, Otherwise Human player's turn.
            if (i_CurrentPlayer.IsComp)
            {
                Utils.GetAiMove(m_gameManager, i_CurrentPlayer, out newX, out newY);
            }
            else
            {
                getMove(i_CurrentPlayer, out newX, out newY, ref i_IsGameOver);
            }

            if (!i_IsGameOver)
            {
                Utils.MakeMove(ref m_gameManager, i_CurrentPlayer, newX, newY);
                Utils.UpadteAvailableMoves(this, ref i_OtherPlayer);
            }

            //     Ex02.ConsoleUtils.Screen.Clear();
            Drawer.DrawBoard(this);

            return i_IsGameOver;
        }

        /// <summary>
        /// Print final results of the game.
        /// </summary>
        public string PrintResult()
        {
            string result;
            // Count points for each player
            Utils.CountPoints(m_gameManager, ref m_playerOne, ref m_playerTwo);

            int currentPlayerPoints = m_playerOne.Points;
            int otherPlayerPoints = m_playerTwo.Points;
            string score = string.Format("({0}/{1})", m_playerOne.Points, m_playerTwo.Points);

            if (currentPlayerPoints == otherPlayerPoints)
            {
                result = string.Format(@"It's a tie! {0}", score);
            }
            else
            {
                // The winner is the one with more coins
                Player winner = (currentPlayerPoints > otherPlayerPoints) ? m_playerOne : m_playerTwo;
                result = string.Format("{0} Won!! {1}", winner.Name, score);
            }
            return result;
        }

        /// <summary>
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
}