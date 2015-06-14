using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace gameUI
{
    public class FormPlayGame : Form
    {
        private const string k_FormHeader = "Othello - Black turn";
        private const string k_FormName = "GameBoard";

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
        private Button m_BtnToDraw;

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

            int sizeOfTheBoard = m_ButtonSize * m_BoardSize + (2 * m_StartPos);
            InitializeNewBoard();

            initForm(sizeOfTheBoard);
        }

        private void initForm(int i_Size)
        {
            this.Text = k_FormHeader;
            this.Name = k_FormName;
            this.ClientSize = new Size(i_Size, i_Size);
            InitializeNewBoard();
        }

        public void InitializeNewBoard()
        {
            for (int x = 0; x < m_BoardSize; x++)
            {
                for (int y = 0; y < m_BoardSize; y++)
                {
                    m_BtnToDraw = new Button();
                    m_BtnToDraw.Top = m_StartPos + (x * m_ButtonSize + Distance);
                    m_BtnToDraw.Left = m_StartPos + (y * m_ButtonSize + Distance);
                    m_BtnToDraw.Width = m_ButtonSize;
                    m_BtnToDraw.Height = m_ButtonSize;

                    // Possible add Buttonclick event etc..
                    Controls.Add(m_BtnToDraw);
                }

            }
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
}
