// <summary>
// Handles game sound effects.
// </summary>
// <copyright file="Media.cs" company="HBHS">
// Copyright (c) HBHS. All rights reserved.
// </copyright>

namespace Students_vs_teachers
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Handles playing media sounds.
    /// </summary>
    internal class Media
    {
        /// <summary>
        /// A pointer for a signal when sounds want to stop.
        /// </summary>
        public const int MM_MCINOTIFY = 0x3B9;

        private string fileName;
        private bool isOpen = false;
        private Form notifyForm;
        private string mediaName = "media";

        /// <summary>
        /// Initializes a new instance of the <see cref="Media"/> class.
        /// </summary>
        /// <param name="fileName">The name of the sound to play.</param>
        public Media(string fileName)
        {
            this.fileName = fileName;

            mediaName = "media" + fileName;
        }

        /// <summary>
        /// Plays a file.
        /// </summary>
        /// <param name="notifyForm">The form to play it on.</param>
        public void Play(Form notifyForm)
        {
            this.notifyForm = notifyForm;
            OpenMediaFile();
            PlayMediaFile();
        }

        /// <summary>
        /// Stops a sound from playing.
        /// </summary>
        public void Stop()
        {
            ClosePlayer();
        }

        [DllImport("winmm.dll")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:Element should begin with upper-case letter", Justification = "DLL Import.")]
        private static extern long mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            IntPtr winHandle);

        private void ClosePlayer()
        {
            if (isOpen)
            {
                var playCommand = "Close " + mediaName;
                mciSendString(playCommand, null, 0, IntPtr.Zero);
                isOpen = false;
            }
        }

        private void OpenMediaFile()
        {
            ClosePlayer();
            string playCommand = "Open \"" + fileName +
                                "\" type mpegvideo alias " + mediaName;
            mciSendString(playCommand, null, 0, IntPtr.Zero);
            isOpen = true;
        }

        private void PlayMediaFile()
        {
            if (isOpen)
            {
                string playCommand = "Play " + mediaName + " notify";
                mciSendString(playCommand, null, 0, notifyForm.Handle);
            }
        }
    }
}
