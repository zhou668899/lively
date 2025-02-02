﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using livelywpf.Core.API;
using livelywpf.Model;

namespace livelywpf.Core
{
    public interface IWallpaper
    {
        /// <summary>
        /// Get lively wallpaper type.
        /// </summary>
        /// <returns></returns>
        WallpaperType GetWallpaperType();
        /// <summary>
        /// Get wallpaper metadata.
        /// </summary>
        /// <returns></returns>
        LibraryModel GetWallpaperData();
        /// <summary>
        /// Get window handle.
        /// </summary>
        /// <returns></returns>
        IntPtr GetHWND();
        /// <summary>
        /// Get process information.
        /// </summary>
        /// <returns>null if not a program wallpaper.</returns>
        Process GetProcess();
        /// <summary>
        /// Start wallpaper.
        /// </summary>
        void Show();
        /// <summary>
        /// Pause wallpaper playback.
        /// </summary>
        void Pause();
        /// <summary>
        /// Start/resume wallpaper playback.
        /// </summary>
        void Play();
        /// <summary>
        /// Stop wallpaper plabyack.
        /// </summary>
        void Stop();
        /// <summary>
        /// Close wallpaper gracefully.
        /// </summary>
        void Close();
        /// <summary>
        /// Immediately kill wallpaper.
        /// Only have effect if Program wallpaper, otherwise same as Close().
        /// </summary>
        void Terminate();
        /// <summary>
        /// Get display device in which wallpaper is currently running.
        /// </summary>
        /// <returns></returns>
        LivelyScreen GetScreen();
        /// <summary>
        /// Set display device in which wallpaper is currently running.<para>
        /// Only metadata, have no effect on actual wallpaper size/position.</para>
        /// </summary>
        /// <param name="display"></param>
        void SetScreen(LivelyScreen display);
        /// <summary>
        /// Send ipc message to program wallpaper.
        /// </summary>
        /// <param name="msg"></param>
        void SendMessage(string msg);
        /// <summary>
        /// Send ipc message to program wallpaper.
        /// </summary>
        /// <param name="msg"></param>
        void SendMessage(IpcMessage obj);
        /// <summary>
        /// Get location of LivelyProperties.json copy file in Savedata/wpdata.
        /// This will be a copy of the original file (different screens will have different copy.)
        /// </summary>
        /// <returns>null if no file.</returns>
        string GetLivelyPropertyCopyPath();
        /// <summary>
        /// Set wallpaper volume.
        /// </summary>
        /// <param name="volume">Range 0 - 100</param>
        void SetVolume(int volume);
        /// <summary>
        /// Sets wallpaper position in timeline. <br>
        /// Only value 0 works for non-video wallpapers.</br>
        /// </summary>
        /// <param name="pos">Range 0 - 100</param>
        void SetPlaybackPos(float pos, PlaybackPosType type);
        /// <summary>
        /// Capture wallpaper view and save as image (.jpg)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        Task ScreenCapture(string filePath);
        /// <summary>
        /// Fires after Show() method is called.
        /// Check success status to check if wallpaper ready/failed.
        /// </summary>
        event EventHandler<WindowInitializedArgs> WindowInitialized;
    }

    public enum PlaybackPosType
    {
        absolutePercent, 
        relativePercent
    }

    public class WindowInitializedArgs : EventArgs
    {
        /// <summary>
        /// True if wallpaper window is ready.
        /// </summary>
        public bool Success { get; set; }
        /// <summary>
        /// Error if any.
        /// Null if no error.
        /// </summary>
        public Exception Error { get; set; }
        /// <summary>
        /// Custom message.
        /// Null if no message.
        /// </summary>
        public string Msg { get; set; }
    }
}
