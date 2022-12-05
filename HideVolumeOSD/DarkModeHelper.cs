using Microsoft.Win32;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HideVolumeOSD
{
    public class DarkModeHelper
    {     
        public static bool IsDarkMode()
        {
            try
            {
                int res = (int)Registry.GetValue("HKEY_CURRENT_USER\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Themes\\Personalize", "AppsUseLightTheme", -1);

                if (res == 0)
                {
                    return true;
                }
            }
            catch
            {
            }

            return false;
        }       
    }
}
