﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FishingBotFoffosEdition
{
    public class MouseUtility
    {
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const int MOUSEEVENTF_RIGHTUP = 0x0010;
        private const int KEY_1 = 0x31;
        private const int SPACEBAR = 0x20;

        public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
        public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag

        public Point CurrentTrackedCursorPos;
        public MouseUtility()
        {
            CurrentTrackedCursorPos = GetCurrentCursorPosition();
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll", SetLastError = true)]
        static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        public void KeyboardPressFishing()
        {
            keybd_event(KEY_1, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(KEY_1, 0, KEYEVENTF_KEYUP, 0);
        }
        public void KeyboardPressJump()
        {
            keybd_event(SPACEBAR, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(SPACEBAR, 0, KEYEVENTF_KEYUP, 0);
        }

        public void KeyboardPress(byte key)
        {
            keybd_event(key, 0, KEYEVENTF_EXTENDEDKEY, 0);
            keybd_event(key, 0, KEYEVENTF_KEYUP, 0);
        }

        public void MoveMouseToPos(int xpos, int ypos)
        {
            CurrentTrackedCursorPos = new Point(xpos, ypos);
            SetCursorPos(xpos, ypos);
        }

        public void MoveAndLeftMouseClick(int xpos, int ypos)
        {
            MoveMouseToPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_LEFTDOWN, xpos, ypos, 0, 0);
            Thread.Sleep(80);
            mouse_event(MOUSEEVENTF_LEFTUP, xpos, ypos, 0, 0);
        }

        public void MoveAndRightMouseClick(int xpos, int ypos)
        {
            MoveMouseToPos(xpos, ypos);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, xpos, ypos, 0, 0);
            Thread.Sleep(80);
            mouse_event(MOUSEEVENTF_RIGHTUP, xpos, ypos, 0, 0);
        }

        public void LeftMouseClick()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, CurrentTrackedCursorPos.X, CurrentTrackedCursorPos.Y, 0, 0);
            Thread.Sleep(80);
            mouse_event(MOUSEEVENTF_LEFTUP, CurrentTrackedCursorPos.X, CurrentTrackedCursorPos.Y, 0, 0);
        }

        public void RightMouseClick()
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, CurrentTrackedCursorPos.X, CurrentTrackedCursorPos.Y, 0, 0);
            Thread.Sleep(150);
            mouse_event(MOUSEEVENTF_RIGHTUP, CurrentTrackedCursorPos.X, CurrentTrackedCursorPos.Y, 0, 0);
        }

        // Declare the INPUT struct
        [StructLayout(LayoutKind.Sequential)]
        public struct INPUT
        {
            internal uint type;
            internal static int Size
            {
                get { return Marshal.SizeOf(typeof(INPUT)); }
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCurrentCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);
            return lpPoint;
        }

    }
}
