using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B15_Ex05_1;
using gameUI1;

namespace gameUI
{
    public class FormPlayGame : Form
    {
        /* GAME */
        private Player m_CurrenPlayer;
        private bool playerOneTurn;
        private GameManager m_GM;
        private const string k_Player1Name = "Black";
        private const string k_PlayerTwoName = "White";

        private const string k_FormHeader = "Othello - ";
        private const string k_FormName = "GameBoard";

        private MyButton[,] m_ListOfButtons;

        /* Board dimentions */
        /// <summary>
        /// Button size
        /// </summary>
        private int m_ButtonSize = 50;
        /// <summary>
        /// Gap between buttons
        /// </summary>
        private int Distance = 2;
        /// <summary>
        /// X and Y Start position.
        /// </summary>
        private int m_StartPos = 10;

        /// <summary>
        /// Board size
        /// </summary>
        private int m_BoardSize;
        /// <summary>
        /// Number of players.
        /// </summary>
        private int m_NumberOfPlayers;

        public FormPlayGame(int i_Size, int i_NumberOfPlayers)
        {
            m_BoardSize = i_Size;
            m_NumberOfPlayers = i_NumberOfPlayers;
            playerOneTurn = true;


            initForm();
        }

        private void initForm()
        {
            m_GM = new GameManager(m_BoardSize, m_NumberOfPlayers, k_Player1Name, k_PlayerTwoName, true);
            m_CurrenPlayer = m_GM.PlayerOne;
            int sizeOfTheBoard = m_ButtonSize * m_BoardSize + (2 * m_StartPos);

            Text = k_FormHeader;
            Name = k_FormName;
            ClientSize = new Size(sizeOfTheBoard, sizeOfTheBoard);
            SetCurrentStateOfTheBoard();
        }

        public void SetCurrentStateOfTheBoard()
        {
            Controls.Clear();
            m_ListOfButtons = new MyButton[m_BoardSize, m_BoardSize];
            this.Text = k_FormHeader + m_CurrenPlayer.Name + "'s turn";
            for (int x = 0; x < m_BoardSize; x++)
            {
                for (int y = 0; y < m_BoardSize; y++)
                {
                    MyButton btnToDraw = new MyButton();
                    btnToDraw.Top = m_StartPos + (x * m_ButtonSize + Distance);
                    btnToDraw.Left = m_StartPos + (y * m_ButtonSize + Distance);
                    btnToDraw.Width = m_ButtonSize;
                    btnToDraw.Height = m_ButtonSize;

                    btnToDraw.X = x;
                    btnToDraw.Y = y;

                    // Possible add Buttonclick event etc..
                    Controls.Add(btnToDraw);
                    //m_ListOfButtons[x, y] = m_BtnToDraw;

                    switch (m_GM.GameBoard[x, y])
                    {
                        case Coin.X:
                            btnToDraw.BackColor = Color.Black;
                            btnToDraw.Text = "O";
                            btnToDraw.ForeColor = Color.White;
                            break;
                        case Coin.O:
                            btnToDraw.BackColor = Color.White;
                            btnToDraw.Text = "O";
                            btnToDraw.ForeColor = Color.Black;
                            break;
                    }

                    if (m_CurrenPlayer[x, y])
                    {
                        btnToDraw.BackColor = Color.Green;
                        btnToDraw.Enabled = true;
                        btnToDraw.Click += button_Clicked;
                    }
                    else
                    {
                        btnToDraw.Enabled = false;
                    }
                }
            }
            if (m_CurrenPlayer.AvailableMoves == 0)
            {
                m_CurrenPlayer = playerOneTurn ? m_GM.PlayerTwo : m_GM.PlayerOne;
                playerOneTurn = !playerOneTurn;
                if (m_CurrenPlayer.AvailableMoves == 0)
                {
                    bool toPlayAgaing = playAgain();
                    if (toPlayAgaing)
                    {
                        initForm();
                    }
                    else
                    {
                        Close();
                    }
                }
                else
                {
                    SetCurrentStateOfTheBoard();
                }
            }

        }

        private void button_Clicked(object sender, EventArgs e)
        {

            MyButton currentButton = sender as MyButton;
            if (currentButton != null) Utils.MakeMove(ref m_GM, m_CurrenPlayer, currentButton.X, currentButton.Y);
            m_CurrenPlayer = playerOneTurn ? m_GM.PlayerTwo : m_GM.PlayerOne;
            Utils.UpadteAvailableMoves(m_GM, m_CurrenPlayer);
            playerOneTurn = !playerOneTurn;
            System.Threading.Thread.Sleep(250);

            if (m_CurrenPlayer.IsComp)
            {
                int x;
                int y;
                Utils.GetAiMove(m_GM, m_CurrenPlayer, out x, out y);
                if (currentButton != null) Utils.MakeMove(ref m_GM, m_CurrenPlayer, x, y);
                m_CurrenPlayer = playerOneTurn ? m_GM.PlayerTwo : m_GM.PlayerOne;
                Utils.UpadteAvailableMoves(m_GM, m_CurrenPlayer);
                playerOneTurn = !playerOneTurn;
            }
            SetCurrentStateOfTheBoard();
        }

        /// <summary>
        /// Ask the user if he wants play this game again
        /// </summary>
        /// <returns>true for play again. false for end game.</returns>
        private bool playAgain()
        {
            string header = "Othello";
            string message = string.Format(@"{0}
Would you like to another round?", m_GM.PrintResult(m_GM));

            DialogResult dialogResult = MessageBox.Show(message, header, MessageBoxButtons.YesNo);
            return (dialogResult == DialogResult.Yes);
        }

        public int StartPos
        {
            get { return m_StartPos; }
        }

        public int BoardSize
        {
            get { return m_BoardSize; }
        }

        public int ButtonSize
        {
            get { return m_ButtonSize; }
        }
    }


    public class MyButton : Button
    {
        private int m_X;
        private int m_Y;

        public int X
        {
            get { return this.m_X; }
            set { this.m_X = value; }
        }

        public int Y
        {
            get { return this.m_Y; }
            set { this.m_Y = value; }
        }
    }
}
