//-----------------------------------------------------------------------
// <copyright file="Game.cs" company="B15_Ex02">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------
using System;
using System.Windows.Forms;
using B15_Ex05_1;
using gameUI;

namespace gameUI1
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

        public static void StartGame(int i_Size, int i_NumberOfPlayers)
        {
            bool runGame = true;
            //  while (runGame)
            //  {
            // TODO: Start form of game
            FormPlayGame formPlayGame = new FormPlayGame(i_Size, i_NumberOfPlayers);
            formPlayGame.ShowDialog();

//                runGame = playAgain(gameManager);
            //    }

            MessageBox.Show("Thank You for playing...");
        }
    }

}
