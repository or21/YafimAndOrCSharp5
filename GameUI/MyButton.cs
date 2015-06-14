//-----------------------------------------------------------------------
// <copyright file="MyButton.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

using System.Windows.Forms;

namespace GameUI
{
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
