using System;

namespace HWIDSpoofer
{
    class Program
    {
        public static string textLogo = @"   ____  ____  ____      ____  _____  ______      ______   _______     ___      ___   ________  ________  _______    " + Environment.NewLine +
            @"  |_   ||   _||_  _|    |_  _||_   _||_   _ `.  .' ____ \ |_   __ \  .'   `.  .'   `.|_   __  ||_   __  ||_   __ \   " + Environment.NewLine +
            @"    | |__| |    \ \  /\  / /    | |    | | `. \ | (___ \_|  | |__) |/  .-.  \/  .-.  \ | |_ \_|  | |_ \_|  | |__) |  " + Environment.NewLine +
            @"    |  __  |     \ \/  \/ /     | |    | |  | |  _.____`.   |  ___/ | |   | || |   | | |  _|     |  _| _   |  __ /   " + Environment.NewLine +
            @"   _| |  | |_     \  /\  /     _| |_  _| |_.' / | \____) | _| |_    \  `-'  /\  `-'  /_| |_     _| |__/ | _| |  \ \_ " + Environment.NewLine +
            @"  |____||____|     \/  \/     |_____||______.'   \______.'|_____|    `.___.'  `.___.'|_____|   |________||____| |___|" + Environment.NewLine; // for create ascii text signature https://www.kammerl.de/ascii/AsciiSignature.php

        static void Main(string[] args) // Spoofer Main
        {
        begin:
            Console.Title = "[LF1337] Simple Hwid Spoofer - github.com/Lufzys/SimpleHWIDSpoofer";
            Console.ResetColor();
            Console.Clear();
            Console.WriteLine(textLogo);
            Console.WriteLine("  ┌ FUNCS ──────────────────────────────────┐");
            Console.WriteLine("  │ [!hwid] Spoof HWID                      │");
            Console.WriteLine("  │ [!guid] Spoof Guid                      │");
            Console.WriteLine("  │ [!pcName] Spoof your Computer Name      │");
            Console.WriteLine("  │ [!productId] Spoof ProductId            │");
            Console.WriteLine("  │ [!mac] Spoof MAC Address                │");
            Console.WriteLine("  │ [!motherboard] Spoof Motherboard Serial │");
            Console.WriteLine("  │ [!bios] Spoof BIOS UUID                 │");
            Console.WriteLine("  │ [!volume] Spoof Volume Serial Number    │");
            Console.WriteLine("  │ [!changes] Show changes table & status  │");
            Console.WriteLine("  │ [!check] Check current registry values  │");
            Console.WriteLine("  │ [!info] Show spoofing limitations       │");
            Console.WriteLine("  └─────────────────────────────────────────┘");

            string input = Console.ReadLine();
            if(input == "!hwid")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.HWID.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.HWID.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.HWID.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!guid")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.PCGuid.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.PCGuid.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.PCGuid.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!pcName")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.PCName.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.PCName.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.PCName.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!productId")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.ProductId.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.ProductId.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.ProductId.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!mac")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.MacAddress.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.MacAddress.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.MacAddress.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!motherboard")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.MotherboardSerial.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.MotherboardSerial.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.MotherboardSerial.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!bios")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.BiosUUID.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.BiosUUID.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.BiosUUID.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!volume")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                if (Spoofer.VolumeSerial.Spoof())
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(Spoofer.VolumeSerial.Log.ToString());
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Spoofer.VolumeSerial.Log.ToString());
                }
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!changes")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(Spoofer.SystemInfo.DisplayChangesTable().ToString());
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("  [SUCCESS] Use the table above to see what can be spoofed and current status");
                Console.WriteLine("  [INFO] MODIFIED status indicates the value has likely been changed from original");
                Console.WriteLine("  [INFO] Use individual spoof commands (!hwid, !mac, etc.) to change values");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  [RESTART GUIDE]");
                Console.WriteLine("  • NO: Changes work immediately");
                Console.WriteLine("  • YES: Full reboot required");
                Console.WriteLine("  • ADAPTER: Disable/Enable network adapter or reboot");
                Console.ResetColor();
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!check")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(Spoofer.SystemInfo.DisplayCurrentValues().ToString());
                Console.WriteLine();
                Console.WriteLine("  [INFO] This shows current registry values that are accessible");
                Console.WriteLine("  [INFO] Compare with other tools to see differences");
                Console.WriteLine("  [INFO] Some values may not match hardware-level identifiers");
                Console.ResetColor();
                Console.ReadLine();
                goto begin;
            }
            else if (input == "!info")
            {
                Console.Clear();
                Console.WriteLine(textLogo);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("  ╔══════════════════════════════════════════════════════════════╗");
                Console.WriteLine("  ║                    IMPORTANT INFORMATION                     ║");
                Console.WriteLine("  ╚══════════════════════════════════════════════════════════════╝");
                Console.WriteLine();
                Console.WriteLine("  [!] Hardware Spoofing Limitations:");
                Console.WriteLine("  • Registry spoofing only affects some applications");
                Console.WriteLine("  • Hardware-level identifiers (SMBIOS, CPUID) cannot be changed");
                Console.WriteLine("  • Advanced tools may read directly from hardware");
                Console.WriteLine("  • Network adapters may need to be disabled/enabled after MAC spoofing");
                Console.WriteLine();
                Console.WriteLine("  [!] What this spoofer can do:");
                Console.WriteLine("  • Change Windows registry values");
                Console.WriteLine("  • Fool basic system information queries");
                Console.WriteLine("  • Modify software-readable identifiers");
                Console.WriteLine();
                Console.WriteLine("  [!] What this spoofer CANNOT do:");
                Console.WriteLine("  • Change hardware firmware identifiers");
                Console.WriteLine("  • Modify BIOS/UEFI stored values");
                Console.WriteLine("  • Spoof CPU ID or hardware serial numbers");
                Console.WriteLine("  • Bypass advanced hardware fingerprinting");
                Console.WriteLine();
                Console.WriteLine("  [!] For best results:");
                Console.WriteLine("  • Run as Administrator");
                Console.WriteLine("  • Disable network adapters before MAC spoofing");
                Console.WriteLine("  • Reboot after spoofing for some changes to take effect");
                Console.WriteLine("  • Test with multiple tools to verify effectiveness");
                Console.ResetColor();
                Console.ReadLine();
                goto begin;
            }
            else
            {
                goto begin;
            }
        }
    }
}
