//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System.Windows.Forms;

namespace GameUI
{
    /// <summary>
    /// Initializes a new game with given parameters.
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Initializes a new instance of the Game class.
        /// </summary>
        public Game()
        {
            FormSettings formSettings = new FormSettings();
            formSettings.ShowDialog();
        }

        /// <summary>
        /// Start the form of play the game.
        /// </summary>
        /// <param name="i_Size"></param>
        /// <param name="i_NumberOfPlayers"></param>
        public static void StartGame(int i_Size, int i_NumberOfPlayers)
        {
            FormPlayGame formPlayGame = new FormPlayGame(i_Size, i_NumberOfPlayers);
            formPlayGame.ShowDialog();
            MessageBox.Show("Thank You for playing...");
        }
    }
}
