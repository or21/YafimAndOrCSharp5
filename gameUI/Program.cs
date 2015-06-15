//-----------------------------------------------------------------------
// <copyright file="Program.cs" company="GameUI">
// Yafim Vodkov 308973882 Or Brand 302521034
// </copyright>
//----------------------------------------------------------------------

using System.Windows.Forms;

namespace GameUI
{
    /// <summary>
    /// Program class
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Runs a new instance of game
        /// </summary>
        public static void Main()
        {
            new Game();
            MessageBox.Show("Thank You for playing...");
        }
    }
}
