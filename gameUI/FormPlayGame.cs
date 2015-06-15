using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using B15_Ex05_1;

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

        private const string k_FormHeader = "Othello - Black turn";
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
        /// Button to draw.
        /// </summary>
        private MyButton m_BtnToDraw;

        /// <summary>
        /// Board size
        /// </summary>
        private int m_BoardSize;
        /// <summary>
        /// Number of players.
        /// </summary>
        private int m_NuberOfPlayers;

        public FormPlayGame(int i_Size, int i_NumberOfPlayers)
        {
            m_BoardSize = i_Size;
            m_NuberOfPlayers = i_NumberOfPlayers;
            playerOneTurn = true;
            
            m_GM = new GameManager(i_Size, i_NumberOfPlayers, k_Player1Name, k_PlayerTwoName, true);
            m_CurrenPlayer = m_GM.PlayerOne;
            int sizeOfTheBoard = m_ButtonSize * m_BoardSize + (2 * m_StartPos);

            initForm(sizeOfTheBoard);

            SetCurrentStateOfTheBoard();

        }

        private void initForm(int i_Size)
        {
            Text = k_FormHeader;
            Name = k_FormName;
            ClientSize = new Size(i_Size, i_Size);
        }

        public void SetCurrentStateOfTheBoard()
        {
            //m_ListOfButtons = new MyButton[m_BoardSize, m_BoardSize];
            for (int x = 0; x < m_BoardSize; x++)
            {
                for (int y = 0; y < m_BoardSize; y++)
                {
                    m_BtnToDraw = new MyButton();
                    m_BtnToDraw.Top = m_StartPos + (x * m_ButtonSize + Distance);
                    m_BtnToDraw.Left = m_StartPos + (y * m_ButtonSize + Distance);
                    m_BtnToDraw.Width = m_ButtonSize;
                    m_BtnToDraw.Height = m_ButtonSize;

                    m_BtnToDraw.X = x;
                    m_BtnToDraw.Y = y;

                    // Possible add Buttonclick event etc..
                    //m_ListOfButtons[x, y] = m_BtnToDraw;

                    if (m_GM.GameBoard[x, y] == Coin.X)
                    {
                        m_BtnToDraw.BackColor = Color.Black;
                        m_BtnToDraw.Text = "O";
                        m_BtnToDraw.ForeColor = Color.White;
                    }
                    else if (m_GM.GameBoard[x, y] == Coin.O)
                    {
                        m_BtnToDraw.BackColor = Color.White;
                        m_BtnToDraw.Text = "O";
                        m_BtnToDraw.ForeColor = Color.Black;
                    }

                    if (m_CurrenPlayer[x, y])
                    {
                        m_BtnToDraw.BackColor = Color.Green;
                    }

                    m_BtnToDraw.Click += button_Clicked;

                    Controls.Add(m_BtnToDraw);

                }
            }
        }

        private void button_Clicked(object sender, EventArgs e)
        {
            MyButton currentButton = sender as MyButton;
            if (currentButton != null) Utils.MakeMove(ref m_GM, ref m_CurrenPlayer, currentButton.X, currentButton.Y);
            m_CurrenPlayer = playerOneTurn ? m_GM.PlayerTwo : m_GM.PlayerOne;
            Utils.UpadteAvailableMoves(m_GM, ref m_CurrenPlayer);
            playerOneTurn = !playerOneTurn;
            Controls.Clear();
            SetCurrentStateOfTheBoard();


        }

                //isGameOver = currentPlayerMove(m_CurrentPlayer, ref isGameOver, ref otherPlayer);

                // Switch turns for next move.
                //if (!isGameOver)
                //{
                //    playerOneTurn = !playerOneTurn;
                 //   otherPlayer = m_CurrentPlayer;
        //        }
        //    }
        //}

        private void updateForm()
        {
            
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
