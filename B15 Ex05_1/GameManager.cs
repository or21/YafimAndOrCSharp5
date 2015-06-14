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
        private Player m_playerOne, m_playerTwo, m_CurrentPlayer;

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
            Utils.UpadteAvailableMoves(this, m_playerOne);
            Utils.UpadteAvailableMoves(this, m_playerTwo);
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
        /// Check if players can make moves
        /// </summary>
        /// <param name="i_CurrentPlayer">Current player</param>
        /// <param name="io_IsGameOver">Update if game over</param>
        /// <param name="io_OtherPlayer">Other player</param>
        /// <returns>True if game over, Otherwise false</returns>
        //private bool currentPlayerMove(Player i_CurrentPlayer, ref bool io_IsGameOver, ref Player io_OtherPlayer)
        //{
        //    // Check if current player can move
        //    bool playerCanMove = i_CurrentPlayer.AvailableMoves != 0;

        //    if (playerCanMove)
        //    {
        //        io_IsGameOver = makePlayerMove(i_CurrentPlayer, io_IsGameOver, ref io_OtherPlayer);
        //    }
        //    else
        //    {
        //        // Other player can move, Otherwise No moves left so end current game.
        //        if (io_OtherPlayer.AvailableMoves != 0)
        //        {
        //            Console.WriteLine("No move left for {0}!", i_CurrentPlayer.Name);
        //        }
        //        else
        //        {
        //            io_IsGameOver = true;
        //        }
        //    }

        //    return io_IsGameOver;
        //}

        /// <summary>
        /// Make a move for current player
        /// </summary>
        /// <param name="i_CurrentPlayer">Current player</param>
        /// <param name="i_IsGameOver">Update if game over</param>
        /// <param name="i_OtherPlayer">Other player</param>
        /// <returns>True if game over, Otherwise false</returns>
       // private bool makePlayerMove(Player i_CurrentPlayer, bool i_IsGameOver, ref Player i_OtherPlayer)
       // {
       //     int newX;
       //     int newY;

       //     // Computer's turn, Otherwise Human player's turn.
       //     if (i_CurrentPlayer.IsComp)
       //     {
       //         Utils.GetAiMove(m_gameManager, i_CurrentPlayer, out newX, out newY);
       //     }
       //     else
       //     {
       //         getMove(i_CurrentPlayer, out newX, out newY, ref i_IsGameOver);
       //     }

       //     if (!i_IsGameOver)
       //     {
       //         Utils.MakeMove(ref m_gameManager, ref i_CurrentPlayer, newX, newY);
       //         Utils.UpadteAvailableMoves(this, ref i_OtherPlayer);
       //     }

       ////     Ex02.ConsoleUtils.Screen.Clear();
       //     Drawer.DrawBoard(this);

       //     return i_IsGameOver;
       // }

        /// <summary>
        /// Print final results of the game.
        /// </summary>
        public string PrintResult(GameManager i_GameManager)
        {
            string result;
            // Count points for each player
            Utils.CountPoints(i_GameManager, ref m_playerOne, ref m_playerTwo);

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
        /// Check if the input is a valid move, and set (x,y) as next potential move.
        /// </summary>
        /// <param name="o_X">x Coordinate</param>
        /// <param name="o_Y">y Coordinate</param>
        /// <param name="i_PlayerInput">Input to check</param>
        /// <param name="i_Player">Current player</param>
        /// <returns>True if a valid move</returns>
        private bool areLettersValidMove(out int o_X, out int o_Y, string i_PlayerInput, Player i_Player)
        {
            // Letter to Y, Number to X
            o_Y = char.ToUpper(i_PlayerInput[0]) - 64 - 1;
            o_X = i_PlayerInput[1] - '0' - 1;

            // Possible input should be within game borders.
            bool isPossible = o_X >= 0 && o_Y >= 0 && o_X < m_size && o_Y < m_size;

            if (!isPossible)
            {
                Console.WriteLine("Invalid Input! Please Try again...");
            }
            else
            {
                isPossible = i_Player[o_X, o_Y];
                if (!isPossible)
                {
                    Console.WriteLine("Can't Move here. Try again...");
                }
            }

            return isPossible;
        }

        /* GETTERS AND SETTERS */

        public Coin[,] GameBoard
        {
            get { return this.m_gameBoard; }
        }

        public Player CurrenPlayer
        {
            get { return this.m_CurrentPlayer; }
        }

        public Player PlayerTwo
        {
            get { return m_playerTwo; }  
        }

        public Player PlayerOne
        {
            get { return m_playerOne; }
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