using Microsoft.Win32;
using System;

// Original code from here: 
// https://docs.microsoft.com/en-us/dotnet/framework/migration-guide/how-to-determine-which-versions-are-installed

namespace dotnetversion
{
    public class Program
    {
        private const string _subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

        public static void Main(string[] args) => 
            Console.WriteLine(GetFrameworkVersion());

        private static string GetFrameworkVersion()
        {
            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(_subkey))
            {
                if (ndpKey?.GetValue("Release") != null)
                    return ".NET Framework Version: " + GetFrameworkVersionFromReleaseKey((int)ndpKey.GetValue("Release"));
                else
                    return ".NET Framework Version 4.5 or later is not detected.";
            }
        }

        private static string GetFrameworkVersionFromReleaseKey(int releaseKey)
        {
            if (releaseKey >= 461808)
                return "4.7.2 or later";
            if (releaseKey >= 461308)
                return "4.7.1";
            if (releaseKey >= 460798)
                return "4.7";
            if (releaseKey >= 394802)
                return "4.6.2";
            if (releaseKey >= 394254)
                return "4.6.1";
            if (releaseKey >= 393295)
                return "4.6";
            if (releaseKey >= 379893)
                return "4.5.2";
            if (releaseKey >= 378675)
                return "4.5.1";
            if (releaseKey >= 378389)
                return "4.5";

            // A non-null release key should mean that 4.5 or later is installed
            return "No 4.5 or later version detected";
        }
    }
}
