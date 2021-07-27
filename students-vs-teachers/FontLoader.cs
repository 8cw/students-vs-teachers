// <summary>
// Contains the FontLoader class.
// </summary>
// <copyright file="FontLoader.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Text;
    using System.Windows.Forms;

    /// <summary>
    /// This class allows us to interpolate with the c++ world to load a custom font.
    /// This class contains helper methods to do such an activity, such as the <b>LoadFont</b> method.
    /// </summary>
    internal class FontLoader
    {
        /// <summary>
        /// The collection which will hold all our custom fonts.
        /// </summary>
        private static readonly PrivateFontCollection GameFonts = new PrivateFontCollection();

        /// <summary>
        /// The "gameria" font.
        /// </summary>
        private static readonly Dictionary<float, Font> GameriaFonts = new Dictionary<float, Font>();

        static FontLoader()
        {
            // create the font data and add it to our font family.
            byte[] fontData = Properties.Resources.GAMERIA;
            IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            GameFonts.AddMemoryFont(fontPtr, Properties.Resources.GAMERIA.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.GAMERIA.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            LoadGameriaFontWithSize(16.0F);
            LoadGameriaFontWithSize(12.0F);
        }

        /// <summary>
        /// Loads the "gameria" font onto an item.
        /// </summary>
        /// <param name="item">The gui element to load the "gameria" font onto.</param>
        /// <param name="fontSize">The font size for the specified element.</param>
        public static void LoadFont(Control item, float fontSize)
        {
            if (!GameriaFonts.ContainsKey(fontSize))
            {
                throw new ArgumentException($"Invalid font size `{fontSize}`");
            }

            item.Font = GameriaFonts[fontSize];
        }

        // import Graphics Device Interface
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr fontPtr, uint fontSize, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint fontsCountPtr);

        private static void LoadGameriaFontWithSize(float fontSize)
        {
            GameriaFonts.Add(fontSize, new Font(GameFonts.Families[0], fontSize));
        }
    }
}
