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
        private const string k_Player1Name = "Black";
        private const string k_PlayerTwoName = "White";

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
            while (runGame)
            {
                // TODO: Start form of game
                FormPlayGame formPlayGame = new FormPlayGame(i_Size, i_NumberOfPlayers);
                formPlayGame.ShowDialog();
                GameManager gameManager = new GameManager(i_Size, i_NumberOfPlayers, k_Player1Name, k_PlayerTwoName, runGame);
                gameManager.RunGame();
                runGame = playAgain(gameManager);
            }

            MessageBox.Show("Thank You for playing...");
        }

        /// <summary>
        /// Ask the user if he wants play this game again
        /// </summary>
        /// <returns>true for play again. false for end game.</returns>
        private static bool playAgain(GameManager i_GameManager)
        {
            string header = "Othello";
            string message = string.Format(@"{0}
Would you like to another round?", i_GameManager.PrintResult());

            DialogResult dialogResult = MessageBox.Show(message, header, MessageBoxButtons.YesNo);
            return (dialogResult == DialogResult.Yes);
        }
    }
}
