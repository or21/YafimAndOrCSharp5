//-----------------------------------------------------------------------
// <copyright file="MyButton.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

using System.Windows.Forms;

namespace GameUI
{
    /// <summary>
    /// MyButton Class extends Button
    /// </summary>
    public class MyButton : Button
    {
        /// <summary>
        /// X Coordinate
        /// </summary>
        private int m_X;
        
        /// <summary>
        /// Y coordinate
        /// </summary>
        private int m_Y;

        /// <summary>
        /// Gets or sets the X coordinate
        /// </summary>
        public int X
        {
            get { return this.m_X; }
            set { this.m_X = value; }
        }

        /// <summary>
        /// Gets or sets the Y coordinate
        /// </summary>
        public int Y
        {
            get { return this.m_Y; }
            set { this.m_Y = value; }
        }
    }
}
