using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using gameUI1;

namespace gameUI
{
    public class FormSettings : Form
    {
        private const string k_Header = "Othello - Game Settings";
        private const string k_ChooseSize = "Board Size: 6x6 (click to increase)";
        private const string k_ChooseSizeName = "buttonBoardSize";
        private const string k_Choose1Player = "Play against the computer ";
        private const string k_Choose1PlayerName = "buttonOnePlayer";
        private const string k_Choose2Players = "Play against your friend";
        private const string k_Choose2PlayerName = "buttonTwoPlayers";

        private int m_BoardSize;
        private int m_NumberOfPlayers;
        private Button m_BoardSizeButton;
        private Button m_PlayerVsPlayerButton;
        private Button m_PlayerVsComputerButton;

        public FormSettings()
        {
            this.m_BoardSize = 6;
            this.m_BoardSizeButton = new Button();
            this.m_PlayerVsPlayerButton = new Button();
            this.m_PlayerVsComputerButton = new Button();

            initForm();
        }

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

        private void gameStart_Click(object i_Sender, EventArgs i_E)
        {
            Button chosenButton = i_Sender as Button;
            bool isPlayerVsPlayer = chosenButton != null && chosenButton.Name == k_Choose2PlayerName;
            this.m_NumberOfPlayers = isPlayerVsPlayer ? 2 : 1;
            this.Hide();
            Game.StartGame(m_BoardSize, m_NumberOfPlayers);
        }

        private void boardSizeButtonClick(object i_Sender, EventArgs i_E)
        {
            m_BoardSize = (m_BoardSize + 2) <= 12 ? m_BoardSize + 2 : 6;

            string boardSizeTxt = "Board Size: " + m_BoardSize + "x" + m_BoardSize + "(click to increase)";
            m_BoardSizeButton.Text = boardSizeTxt;
        }
    }
}
