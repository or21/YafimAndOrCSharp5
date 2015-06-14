//-----------------------------------------------------------------------
// <copyright file="FormSettings.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameUI
{
    public class FormSettings : Form
    {
        /// <summary>
        /// Form header
        /// </summary>
        private const string k_Header = "Othello - Game Settings";

        /// <summary>
        /// Button of choose size
        /// </summary>
        private const string k_ChooseSize = "Board Size: 6x6 (click to increase)";

        /// <summary>
        /// Button choose size name
        /// </summary>
        private const string k_ChooseSizeName = "buttonBoardSize";

        /// <summary>
        /// Button choose one player text
        /// </summary>
        private const string k_Choose1Player = "Play against the computer ";

        /// <summary>
        /// Button choose one player name
        /// </summary>
        private const string k_Choose1PlayerName = "buttonOnePlayer";

        /// <summary>
        /// Button choose two players text
        /// </summary>
        private const string k_Choose2Players = "Play against your friend";

        /// <summary>
        /// Button choose two players name
        /// </summary>
        private const string k_Choose2PlayerName = "buttonTwoPlayers";

        /// <summary>
        /// Board size button
        /// </summary>
        private readonly Button m_BoardSizeButton;

        /// <summary>
        /// Player Vs Player button
        /// </summary>
        private readonly Button m_PlayerVsPlayerButton;

        /// <summary>
        /// Player Vs Computer button
        /// </summary>
        private readonly Button m_PlayerVsComputerButton;

        /// <summary>
        /// Board size
        /// </summary>
        private int m_BoardSize;

        /// <summary>
        /// Number of players
        /// </summary>
        private int m_NumberOfPlayers;

        public FormSettings()
        {
            this.m_BoardSize = 6;
            this.m_BoardSizeButton = new Button();
            this.m_PlayerVsPlayerButton = new Button();
            this.m_PlayerVsComputerButton = new Button();

            initForm();
        }
        
        /// <summary>
        /// Initialize the form
        /// </summary>
        private void initForm()
        {
            // init button of BoardSize
            this.m_BoardSizeButton.Text = k_ChooseSize;
            this.m_BoardSizeButton.Name = k_ChooseSizeName;
            this.m_BoardSizeButton.Size = new Size(260, 40);
            this.m_BoardSizeButton.Location = new Point(12, 29);
            this.m_BoardSizeButton.Click += this.boardSizeButtonClick;

            // init button Choose 1 player
            this.m_PlayerVsComputerButton.Text = k_Choose1Player;
            this.m_PlayerVsComputerButton.Name = k_Choose1PlayerName;
            this.m_PlayerVsComputerButton.Size = new Size(127, 40);
            this.m_PlayerVsComputerButton.Location = new Point(12, 88);
            this.m_PlayerVsComputerButton.Click += this.gameStart_Click;

            // init button Choose 2 players
            this.m_PlayerVsPlayerButton.Text = k_Choose2Players;
            this.m_PlayerVsPlayerButton.Name = k_Choose2PlayerName;
            this.m_PlayerVsPlayerButton.Size = new Size(127, 40);
            this.m_PlayerVsPlayerButton.Location = new Point(145, 88);
            this.m_PlayerVsPlayerButton.Click += this.gameStart_Click;

            // init the form
            this.Text = k_Header;
            this.Name = "Settings";
            this.ClientSize = new Size(284, 153);
            this.Controls.Add(this.m_BoardSizeButton);
            this.Controls.Add(this.m_PlayerVsComputerButton);
            this.Controls.Add(this.m_PlayerVsPlayerButton);
        }

        /// <summary>
        /// Start game according to the user choise
        /// </summary>
        /// <param name="i_Sender"></param>
        /// <param name="i_E"></param>
        private void gameStart_Click(object i_Sender, EventArgs i_E)
        {
            Button chosenButton = i_Sender as Button;
            bool isPlayerVsPlayer = chosenButton != null && chosenButton.Name == k_Choose2PlayerName;
            this.m_NumberOfPlayers = isPlayerVsPlayer ? 2 : 1;
            this.Hide();
            Game.StartGame(m_BoardSize, m_NumberOfPlayers);
        }

        /// <summary>
        /// Increase size according to user choise
        /// </summary>
        /// <param name="i_Sender"></param>
        /// <param name="i_E"></param>
        private void boardSizeButtonClick(object i_Sender, EventArgs i_E)
        {
            m_BoardSize = (m_BoardSize + 2) <= 12 ? m_BoardSize + 2 : 6;

            string boardSizeTxt = "Board Size: " + m_BoardSize + "x" + m_BoardSize + "(click to increase)";
            m_BoardSizeButton.Text = boardSizeTxt;
        }
    }
}
