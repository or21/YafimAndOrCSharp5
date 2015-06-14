//-----------------------------------------------------------------------
// <copyright file="Player.cs" company="GameLogic">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System.Collections.Generic;

namespace GameLogic
{
    /// <summary>
    /// This class hold player's properties.
    /// </summary>
    public class Player
    {
        /// <summary>
        /// If player is computer..
        /// </summary>
        private readonly bool r_IsComputer;

        /// <summary>
        /// Shape of the player
        /// </summary>
        private readonly Coin r_Shape;

        /// <summary>
        /// Possible moves
        /// </summary>
        private readonly bool[,] r_AvailableMoves;

        /// <summary>
        /// Size of the board
        /// </summary>
        private readonly int r_BoardSize;

        /// <summary>
        /// The name of the player
        /// </summary>
        private string m_Name;

        /// <summary>
        /// Current points of the player
        /// </summary>
        private int m_CurrentPoints;

        /// <summary>
        /// Number of available moves left
        /// </summary>
        private int m_NumberOfAvailableMoves;

        /// <summary>
        /// The possible coordinates player can move
        /// </summary>
        private List<Coord> m_PossibleMovesCoordinates;

        /// <summary>
        /// The number of points in this session of games
        /// </summary>
        private int m_NumberOfWinnings;

        /// <summary>
        /// Initializes a new instance of the Player class.
        /// </summary>
        /// <param name="i_IsComputer">Player is computer</param>
        /// <param name="i_Shape">Coin shape of the player</param>
        /// <param name="i_PlayerName">Name of the player</param>
        /// <param name="i_BoardSize">Game board size</param>
        public Player(bool i_IsComputer, Coin i_Shape, string i_PlayerName, int i_BoardSize)
        {
            this.m_Name = i_PlayerName;
            this.r_IsComputer = i_IsComputer;
            this.r_Shape = i_Shape;
            this.r_BoardSize = i_BoardSize;
            this.r_AvailableMoves = new bool[r_BoardSize, r_BoardSize];
            this.m_CurrentPoints = 0;
            this.m_NumberOfAvailableMoves = 0;
            this.m_PossibleMovesCoordinates = new List<Coord>();
            this.m_NumberOfWinnings = 0;

            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    this.r_AvailableMoves[i, j] = false;
                }
            }
        }

        /// <summary>
        /// Get if (x,y) is possible move.
        /// </summary>
        /// <param name="i_X">x coordinate</param>
        /// <param name="i_Y">y coordinate</param>
        /// <returns>True if possible move</returns>
        public bool this[int i_X, int i_Y]
        {
            get { return r_AvailableMoves[i_X, i_Y]; }
            set { this.r_AvailableMoves[i_X, i_Y] = value; }
        }

        /// <summary>
        /// Gets a value indicating whether its a computer or not
        /// </summary>
        public bool IsComp
        {
            get { return r_IsComputer; }
        }

        /// <summary>
        /// Gets player's coin share
        /// </summary>
        public Coin ShapeCoin
        {
            get { return r_Shape; }
        }

        /// <summary>
        /// Gets or sets current points of the player
        /// </summary>
        public int Points
        {
            get { return m_CurrentPoints; }
            set { this.m_CurrentPoints = value; }
        }

        /// <summary>
        /// Gets or sets player's points.
        /// </summary>
        public int AvailableMoves
        {
            get { return m_NumberOfAvailableMoves; }
            set { this.m_NumberOfAvailableMoves = value; }
        }

        /// <summary>
        /// Gets or sets player's name.
        /// </summary>
        public string Name
        {
            get { return m_Name; }
            set { this.m_Name = value; }
        }

        /// <summary>
        /// Gets the size of the board.
        /// </summary>
        public int BoardSize
        {
            get { return r_BoardSize; }
        }

        /// <summary>
        /// Gets or sets the possible coordinates player can go.
        /// </summary>
        public List<Coord> PossibleMovesCoordinates
        {
            get { return m_PossibleMovesCoordinates; }
            set { this.m_PossibleMovesCoordinates = value; }
        }

        /// <summary>
        /// Gets or sets player's session points.
        /// </summary>
        public int NumberOfWinnings
        {
            get { return m_NumberOfWinnings; }
            set { this.m_NumberOfWinnings = value; }
        }
    }

    /// <summary>
    /// Struct holds coordinate (x,y)
    /// </summary>
    public struct Coord
    {
        /// <summary>
        /// X Coordinate
        /// </summary>
        public int m_X;

        /// <summary>
        /// Y Coordinate
        /// </summary>
        public int m_Y;
    }
}
