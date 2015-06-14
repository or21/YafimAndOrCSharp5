//-----------------------------------------------------------------------
// <copyright file="FormPlayGame.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

using System;
using System.Drawing;
using System.Windows.Forms;
using GameLogic;

namespace GameUI
{
    public class FormPlayGame : Form
    {
        /* GAME */

        /// <summary>
        /// Player 1 name
        /// </summary>
        private const string k_Player1Name = "Black";

        /// <summary>
        /// Player 2 name
        /// </summary>
        private const string k_PlayerTwoName = "White";

        /// <summary>
        /// Form header
        /// </summary>
        private const string k_FormHeader = "Othello - ";

        /// <summary>
        /// Form Name
        /// </summary>
        private const string k_FormName = "GameBoard";

        /* Board dimentions */

        /// <summary>
        /// Button size
        /// </summary>
        private const int k_ButtonSize = 50;

        /// <summary>
        /// Gap between buttons
        /// </summary>
        private const int k_Distance = 2;

        /// <summary>
        /// X and Y Start position.
        /// </summary>
        private const int k_StartPos = 10;

        /// <summary>
        /// Board size
        /// </summary>
        private readonly int m_BoardSize;

        /// <summary>
        /// Current player
        /// </summary>
        private Player m_CurrenPlayer;

        /// <summary>
        /// Game manager instance
        /// </summary>
        private bool m_PlayerOneTurn;

        /// <summary>
        /// Current turn
        /// </summary>
        private GameManager m_GameManager;

        /// <summary>
        /// Number of players.
        /// </summary>
        private int m_NumberOfPlayers;

        public FormPlayGame(int i_Size, int i_NumberOfPlayers)
        {
            m_BoardSize = i_Size;
            m_NumberOfPlayers = i_NumberOfPlayers;
            m_GameManager = new GameManager(m_BoardSize, m_NumberOfPlayers, k_Player1Name, k_PlayerTwoName);

            initNewGame();
        }

        /// <summary>
        /// Initialize a new game
        /// </summary>
        private void initNewGame()
        {
            m_GameManager.SetNewGame();
            m_PlayerOneTurn = true;
            m_CurrenPlayer = m_GameManager.PlayerOne;
            int sizeOfTheBoard = (k_ButtonSize * m_BoardSize) + (2 * k_StartPos);
            this.Text = k_FormHeader;
            this.Name = k_FormName;
            ClientSize = new Size(sizeOfTheBoard, sizeOfTheBoard);
            SetCurrentStateOfTheBoard();
        }

        /// <summary>
        /// Set the current state of the board in the form
        /// </summary>
        public void SetCurrentStateOfTheBoard()
        {
            Controls.Clear();
            this.Text = k_FormHeader + m_CurrenPlayer.Name + "'s turn";
            for (int x = 0; x < m_BoardSize; x++)
            {
                for (int y = 0; y < m_BoardSize; y++)
                {
                    MyButton btnToDraw = new MyButton
                    {
                        Top = k_StartPos + (x * k_ButtonSize) + k_Distance,
                        Left = k_StartPos + (y * k_ButtonSize) + k_Distance,
                        Width = k_ButtonSize,
                        Height = k_ButtonSize,
                        X = x,
                        Y = y,
                        Enabled = false
                    };

                    // Possible add Buttonclick event etc..
                    Controls.Add(btnToDraw);
                    paintButton(x, y, btnToDraw);
                }
            }

            checkIfGameContinues();
        }

        /// <summary>
        /// Paint the button according to logic
        /// </summary>
        /// <param name="i_X"></param>
        /// <param name="i_Y"></param>
        /// <param name="i_BtnToDraw"></param>
        private void paintButton(int i_X, int i_Y, MyButton i_BtnToDraw)
        {
            switch (m_GameManager.GameBoard[i_X, i_Y])
            {
                case Coin.X:
                    i_BtnToDraw.BackColor = Color.Black;
                    i_BtnToDraw.Text = "O";
                    i_BtnToDraw.ForeColor = Color.White;
                    break;
                case Coin.O:
                    i_BtnToDraw.BackColor = Color.White;
                    i_BtnToDraw.Text = "O";
                    i_BtnToDraw.ForeColor = Color.Black;
                    break;
            }

            if (m_CurrenPlayer[i_X, i_Y])
            {
                i_BtnToDraw.BackColor = Color.Green;
                i_BtnToDraw.Enabled = true;
                i_BtnToDraw.Click += button_Clicked;
            }
        }

        /// <summary>
        /// Check if there availables moves
        /// </summary>
        private void checkIfGameContinues()
        {
            if (m_CurrenPlayer.AvailableMoves == 0)
            {
                m_CurrenPlayer = m_PlayerOneTurn ? m_GameManager.PlayerTwo : m_GameManager.PlayerOne;
                m_PlayerOneTurn = !m_PlayerOneTurn;
                if (m_CurrenPlayer.AvailableMoves == 0)
                {
                    bool toPlayAgaing = playAgain();
                    if (toPlayAgaing)
                    {
                        initNewGame();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    if (m_CurrenPlayer.IsComp)
                    {
                        invokeCompMove();
                    }

                    SetCurrentStateOfTheBoard();
                }
            }
        }

        /// <summary>
        /// Make a move according to the clicked button
        /// </summary>
        /// <param name="i_Sender"></param>
        /// <param name="i_E"></param>
        private void button_Clicked(object i_Sender, EventArgs i_E)
        {
            MyButton currentButton = i_Sender as MyButton;
            if (currentButton != null)
            {
                Utils.MakeMove(ref m_GameManager, m_CurrenPlayer, currentButton.X, currentButton.Y);
            }

            m_CurrenPlayer = m_PlayerOneTurn ? m_GameManager.PlayerTwo : m_GameManager.PlayerOne;
            Utils.UpadteAvailableMoves(m_GameManager, m_CurrenPlayer);
            m_PlayerOneTurn = !m_PlayerOneTurn;
            System.Threading.Thread.Sleep(250);

            if (m_CurrenPlayer.IsComp)
            {
                invokeCompMove();
            }

            SetCurrentStateOfTheBoard();
        }

        /// <summary>
        /// Make AI move
        /// </summary>
        private void invokeCompMove()
        {
            if (m_CurrenPlayer.AvailableMoves != 0)
            {
                int x;
                int y;
                Utils.GetAiMove(m_GameManager, m_CurrenPlayer, out x, out y);
                Utils.MakeMove(ref m_GameManager, m_CurrenPlayer, x, y);
            }

            m_CurrenPlayer = m_PlayerOneTurn ? m_GameManager.PlayerTwo : m_GameManager.PlayerOne;
            Utils.UpadteAvailableMoves(m_GameManager, m_CurrenPlayer);
            m_PlayerOneTurn = !m_PlayerOneTurn;
        }

        /// <summary>
        /// Ask the user if he wants play this game again
        /// </summary>
        /// <returns>true for play again. false for end game.</returns>
        private bool playAgain()
        {
            string header = "Othello";
            string message = string.Format(
@"{0}
Would you like to another round?", 
                                 m_GameManager.PrintResult(m_GameManager));

            DialogResult dialogResult = MessageBox.Show(message, header, MessageBoxButtons.YesNo);
            return dialogResult == DialogResult.Yes;
        }

        /// <summary>
        /// Gets or sets start position
        /// </summary>
        public int StartPos
        {
            get { return k_StartPos; }
        }

        /// <summary>
        /// Gets or sets board size
        /// </summary>
        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        /// <summary>
        /// Gets or sets buttons size
        /// </summary>
        public int ButtonSize
        {
            get { return k_ButtonSize; }
        }
    }
}
