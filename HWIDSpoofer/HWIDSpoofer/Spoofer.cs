using System;
using System.Text;
using System.Linq;
using Microsoft.Win32;

namespace HWIDSpoofer
{
    class Spoofer
    {
        public static class HWID
        {
            // Update the registry path based on Windows 22H2 structure
            public static Regedit regeditOBJ = new Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
            public static readonly string Key = "HwProfileGuid";

            public static string GetValue()
            {
                return regeditOBJ.Read(Key);
            }

            public static bool SetValue(object value)
            {
                return regeditOBJ.Write(Key, value);
            }

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                string oldValue = GetValue();
                bool result = SetValue("{" + Guid.NewGuid().ToString() + "}");
                if (result)
                {
                    Log.Append("  [SPOOFER] HWID Changed from " + oldValue + " to " + GetValue());
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Error accessing the Registry... Maybe run as admin");
                }
                return result;
            }
        }

        public static class PCGuid
        {
            // Update the registry path based on Windows 22H2 structure
            public static Regedit regeditOBJ = new Regedit(@"SOFTWARE\Microsoft\Cryptography");
            public static readonly string Key = "MachineGuid";

            public static string GetValue()
            {
                return regeditOBJ.Read(Key);
            }

            public static bool SetValue(object value)
            {
                return regeditOBJ.Write(Key, value);
            }

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                string oldValue = GetValue();
                bool result = SetValue(Guid.NewGuid().ToString());
                if (result)
                {
                    Log.Append("  [SPOOFER] Guid Changed from " + oldValue + " to " + GetValue());
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Error accessing the Registry... Maybe run as admin");
                }
                return result;
            }
        }

        public static class PCName
        {
            // Update the registry path based on Windows 22H2 structure
            public static Regedit regeditOBJ = new Regedit(@"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName");
            public static readonly string Key = "ComputerName";

            public static string GetValue()
            {
                return regeditOBJ.Read(Key);
            }

            public static bool SetValue(object value)
            {
                return regeditOBJ.Write(Key, value);
            }

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                string oldValue = GetValue();
                bool result = SetValue("DESKTOP-" + Utilities.GenerateString(15));
                if (result)
                {
                    Log.Append("  [SPOOFER] Computer Name Changed from " + oldValue + " to " + GetValue());
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Error accessing the Registry... Maybe run as admin");
                }
                return result;
            }
        }

        public static class ProductId
        {
            // Update the registry path based on Windows 22H2 structure
            public static Regedit regeditOBJ = new Regedit(@"SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion");
            public static readonly string Key = "ProductID";

            public static string GetValue()
            {
                return regeditOBJ.Read(Key);
            }

            public static bool SetValue(object value)
            {
                return regeditOBJ.Write(Key, value);
            }

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                string oldValue = GetValue();
                bool result = SetValue(Utilities.GenerateString(5) + "-" + Utilities.GenerateString(5) + "-" + Utilities.GenerateString(5) + "-" + Utilities.GenerateString(5));
                if (result)
                {
                    Log.AppendLine("  [SPOOFER] Computer ProductID Changed from " + oldValue + " to " + GetValue());
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Error accessing the Registry... Maybe run as admin");
                }
                return result;
            }
        }

        public static class MacAddress
        {
            // Multiple possible registry paths for network adapters
            private static string[] possiblePaths = {
                @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0001",
                @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0002",
                @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0003",
                @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0004",
                @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0005"
            };
            
            public static readonly string Key = "NetworkAddress";

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                bool success = false;
                int successCount = 0;
                
                // Try to spoof all network adapters
                foreach (string path in possiblePaths)
                {
                    try
                    {
                        Regedit regeditOBJ = new Regedit(path);
                        string oldValue = regeditOBJ.Read(Key);
                        
                        // Generate a valid MAC address (first byte should be even for unicast)
                        string newMac = GenerateValidMacAddress();
                        bool result = regeditOBJ.Write(Key, newMac);
                        
                        if (result)
                        {
                            Log.AppendLine($"  [SPOOFER] MAC Address {successCount + 1} Changed from {oldValue} to {newMac}");
                            Log.AppendLine($"  [SPOOFER] Registry Path: {path}");
                            successCount++;
                            success = true;
                        }
                    }
                    catch
                    {
                        continue;
                    }
                }
                
                if (success)
                {
                    Log.AppendLine($"  [SPOOFER] Successfully spoofed {successCount} network adapter(s)");
                    Log.AppendLine("  [CRITICAL] To apply MAC changes effectively:");
                    Log.AppendLine("  [STEP 1] Disable your network adapter in Device Manager");
                    Log.AppendLine("  [STEP 2] Wait 5 seconds");
                    Log.AppendLine("  [STEP 3] Re-enable your network adapter");
                    Log.AppendLine("  [STEP 4] Or reboot your computer");
                    Log.AppendLine("  [WARNING] Hardware-level MAC may still be visible to advanced tools");
                    Log.AppendLine("  [INFO] Registry MAC affects most applications and basic network tools");
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Could not find accessible network adapter in registry");
                    Log.AppendLine("  [SPOOFER] Try running as administrator or disable adapter before spoofing");
                }
                
                return success;
            }
            
            private static string GenerateValidMacAddress()
            {
                // Generate a valid MAC address with proper format
                Random rand = new Random();
                string[] macBytes = new string[6];
                
                // First byte - make sure it's even (unicast) and locally administered
                int firstByte = (rand.Next(0, 64) * 4) + 2; // Ensures locally administered bit is set
                macBytes[0] = firstByte.ToString("X2");
                
                for (int i = 1; i < 6; i++)
                {
                    macBytes[i] = rand.Next(0, 256).ToString("X2");
                }
                
                return string.Join("", macBytes); // Registry expects no separators
            }
        }

        public static class MotherboardSerial
        {
            // Multiple possible registry paths for motherboard information
            private static string[] possiblePaths = {
                @"SYSTEM\CurrentControlSet\Control\SystemInformation",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion",
                @"SYSTEM\CurrentControlSet\Services\mssmbios\Data",
                @"HARDWARE\DESCRIPTION\System\BIOS"
            };
            
            private static string[] possibleKeys = {
                "SystemProductName",
                "SystemManufacturer", 
                "SMBiosData",
                "BaseBoardSerialNumber"
            };

            public static StringBuilder Log = new StringBuilder();
            public static bool Spoof()
            {
                Log.Clear();
                bool success = false;
                int changedCount = 0;
                
                // Try different registry locations for motherboard info
                for (int i = 0; i < possiblePaths.Length; i++)
                {
                    for (int j = 0; j < possibleKeys.Length; j++)
                    {
                        try
                        {
                            Regedit regeditOBJ = new Regedit(possiblePaths[i]);
                            string oldValue = regeditOBJ.Read(possibleKeys[j]);
                            
                            if (oldValue != "ERR")
                            {
                                string newValue = "MB" + Utilities.GenerateString(10) + DateTime.Now.Millisecond.ToString();
                                bool result = regeditOBJ.Write(possibleKeys[j], newValue);
                                
                                if (result)
                                {
                                    Log.AppendLine($"  [SPOOFER] Motherboard ID {changedCount + 1} Changed from {oldValue} to {newValue}");
                                    Log.AppendLine($"  [SPOOFER] Registry Path: {possiblePaths[i]}\\{possibleKeys[j]}");
                                    changedCount++;
                                    success = true;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                
                if (success)
                {
                    Log.AppendLine($"  [INFO] Changed {changedCount} motherboard identifier(s) in registry");
                    Log.AppendLine("  [WARNING] Hardware-level SMBIOS data cannot be changed via registry");
                    Log.AppendLine("  [WARNING] Tools reading SMBIOS directly will still see original values");
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Could not find accessible motherboard identifier in registry");
                    Log.AppendLine("  [SPOOFER] Try running as administrator or motherboard info may be hardware protected");
                }
                
                return success;
            }
        }

        public static class BiosUUID
        {
            // More comprehensive registry paths for system identification
            private static string[] possiblePaths = {
                @"SOFTWARE\Microsoft\Cryptography",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion",
                @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001",
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion",
                @"SYSTEM\CurrentControlSet\Control\SystemInformation"
            };
            
            private static string[] possibleKeys = {
                "MachineGuid",
                "InstallDate",
                "HwProfileGuid", 
                "InstallTime",
                "SystemBiosDate",
                "SystemBiosVersion"
            };

            public static StringBuilder Log = new StringBuilder();
            
            public static bool Spoof()
            {
                Log.Clear();
                bool success = false;
                int changedCount = 0;
                
                // Try different registry locations for system identification
                for (int i = 0; i < possiblePaths.Length; i++)
                {
                    for (int j = 0; j < possibleKeys.Length; j++)
                    {
                        try
                        {
                            Regedit regeditOBJ = new Regedit(possiblePaths[i]);
                            string oldValue = regeditOBJ.Read(possibleKeys[j]);
                            
                            if (oldValue != "ERR")
                            {
                                string newValue;
                                // Generate appropriate value based on key type
                                if (possibleKeys[j].Contains("Guid"))
                                {
                                    newValue = "{" + Guid.NewGuid().ToString() + "}";
                                }
                                else if (possibleKeys[j].Contains("Date") || possibleKeys[j].Contains("Time"))
                                {
                                    // Generate a realistic date/time value
                                    Random rand = new Random();
                                    DateTime randomDate = DateTime.Now.AddDays(-rand.Next(365, 1095)); // 1-3 years ago
                                    newValue = possibleKeys[j].Contains("Date") ? 
                                        randomDate.ToString("MM/dd/yyyy") : 
                                        ((DateTimeOffset)randomDate).ToUnixTimeSeconds().ToString();
                                }
                                else if (possibleKeys[j].Contains("Version"))
                                {
                                    newValue = $"V{Utilities.GenerateString(1)}.{Utilities.GenerateString(2)}";
                                }
                                else
                                {
                                    newValue = Utilities.GenerateString(12);
                                }
                                
                                bool result = regeditOBJ.Write(possibleKeys[j], newValue);
                                
                                if (result)
                                {
                                    Log.AppendLine($"  [SPOOFER] System ID {changedCount + 1} Changed from {oldValue} to {newValue}");
                                    Log.AppendLine($"  [SPOOFER] Registry Path: {possiblePaths[i]}\\{possibleKeys[j]}");
                                    changedCount++;
                                    success = true;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                
                if (success)
                {
                    Log.AppendLine($"  [INFO] Changed {changedCount} system identifier(s) in registry");
                    Log.AppendLine("  [WARNING] Hardware BIOS UUID cannot be changed via registry");
                    Log.AppendLine("  [WARNING] Tools reading SMBIOS directly will still see original UUID");
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Could not find accessible system identifiers in registry");
                    Log.AppendLine("  [SPOOFER] Try running as administrator");
                    Log.AppendLine("  [INFO] Some system identifiers may be hardware-protected or non-existent");
                }
                
                return success;
            }
        }

        public static class VolumeSerial
        {
            // Registry paths for volume serial numbers - expanded for better coverage
            private static string[] possiblePaths = {
                @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001",
                @"SOFTWARE\Microsoft\Windows\CurrentVersion",
                @"SYSTEM\MountedDevices",
                @"HARDWARE\DESCRIPTION\System\CentralProcessor\0",
                @"SYSTEM\CurrentControlSet\Enum\STORAGE",
                @"SOFTWARE\Microsoft\Windows NT\CurrentVersion"
            };
            
            private static string[] possibleKeys = {
                "VolumeSerialNumber",
                "SystemDriveSerial", 
                "BootVolumeSerial",
                "ProcessorNameString",
                "InstallDate",
                "DigitalProductId"
            };

            public static StringBuilder Log = new StringBuilder();
            
            public static bool Spoof()
            {
                Log.Clear();
                bool success = false;
                int changedCount = 0;
                
                // Try different registry locations for volume serial
                for (int i = 0; i < possiblePaths.Length; i++)
                {
                    for (int j = 0; j < possibleKeys.Length; j++)
                    {
                        try
                        {
                            Regedit regeditOBJ = new Regedit(possiblePaths[i]);
                            string oldValue = regeditOBJ.Read(possibleKeys[j]);
                            
                            if (oldValue != "ERR")
                            {
                                // Generate a realistic volume serial number (8 hex digits)
                                string newValue = GenerateVolumeSerial();
                                bool result = regeditOBJ.Write(possibleKeys[j], newValue);
                                
                                if (result)
                                {
                                    Log.AppendLine($"  [SPOOFER] Volume Serial {changedCount + 1} Changed from {oldValue} to {newValue}");
                                    Log.AppendLine($"  [SPOOFER] Registry Path: {possiblePaths[i]}\\{possibleKeys[j]}");
                                    changedCount++;
                                    success = true;
                                }
                            }
                        }
                        catch
                        {
                            continue;
                        }
                    }
                }
                
                // Also try to create new volume serial entries if none exist
                if (!success)
                {
                    try
                    {
                        Regedit regeditOBJ = new Regedit(@"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001");
                        string newValue = GenerateVolumeSerial();
                        bool result = regeditOBJ.Write("VolumeSerialNumber", newValue);
                        
                        if (result)
                        {
                            Log.AppendLine($"  [SPOOFER] Created new Volume Serial: {newValue}");
                            Log.AppendLine($"  [SPOOFER] Registry Path: SYSTEM\\CurrentControlSet\\Control\\IDConfigDB\\Hardware Profiles\\0001\\VolumeSerialNumber");
                            success = true;
                            changedCount = 1;
                        }
                    }
                    catch
                    {
                        // Continue to final message
                    }
                }
                
                if (success)
                {
                    Log.AppendLine($"  [INFO] Changed {changedCount} volume serial number(s) in registry");
                    Log.AppendLine("  [WARNING] Hardware volume serial cannot be changed via registry");
                    Log.AppendLine("  [WARNING] Tools reading directly from drive will still see original serial");
                    Log.AppendLine("  [INFO] This affects software-based volume identification only");
                }
                else
                {
                    Log.AppendLine("  [SPOOFER] Could not find or create volume serial entries in registry");
                    Log.AppendLine("  [SPOOFER] Try running as administrator for registry write access");
                }
                
                return success;
            }
            
            private static string GenerateVolumeSerial()
            {
                // Generate a realistic 8-character hex volume serial (like 89231113)
                Random rand = new Random();
                string serial = "";
                for (int i = 0; i < 8; i++)
                {
                    serial += rand.Next(0, 16).ToString("X");
                }
                return serial;
            }
        }

        public class Regedit
        {
            private string regeditPath = string.Empty;
            public Regedit(string regeditPath)
            {
                this.regeditPath = regeditPath;
            }

            public string Read(string keyName)
            {
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regeditPath))
                    {
                        if (key != null)
                        {
                            return key.GetValue(keyName)?.ToString() ?? "ERR";
                        }
                        else
                        {
                            return "ERR";
                        }
                    }
                }
                catch (Exception)
                {
                    return "ERR";
                }
            }

            public bool Write(string keyName, object value)
            {
                try
                {
                    using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regeditPath, true))
                    {
                        if (key != null)
                        {
                            key.SetValue(keyName, value);
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public static class SystemInfo
        {
            public static StringBuilder DisplayCurrentValues()
            {
                StringBuilder info = new StringBuilder();
                info.AppendLine("  [SYSTEM INFO] Current Registry Values:");
                info.AppendLine("  ==========================================");
                
                // Display key system identifiers in registry format
                var checkPaths = new[]
                {
                    new { Path = @"SOFTWARE\Microsoft\Cryptography", Key = "MachineGuid", Name = "Machine GUID" },
                    new { Path = @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001", Key = "HwProfileGuid", Name = "Hardware Profile GUID" },
                    new { Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", Key = "ProductID", Name = "Product ID" },
                    new { Path = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0001", Key = "NetworkAddress", Name = "MAC Address 1" },
                    new { Path = @"SYSTEM\CurrentControlSet\Control\SystemInformation", Key = "SystemProductName", Name = "System Product" },
                    new { Path = @"SOFTWARE\Microsoft\Windows\CurrentVersion", Key = "VolumeSerialNumber", Name = "Volume Serial" }
                };

                foreach (var item in checkPaths)
                {
                    try
                    {
                        Regedit reg = new Regedit(item.Path);
                        string value = reg.Read(item.Key);
                        string valueType = DetectRegistryValueType(value);
                        
                        if (value != "ERR")
                        {
                            info.AppendLine($"  [{item.Name}]");
                            info.AppendLine($"  Path: HKLM\\{item.Path}");
                            info.AppendLine($"  Key: {item.Key}");
                            info.AppendLine($"  Type: {valueType}");
                            info.AppendLine($"  Value: {value}");
                            info.AppendLine("  " + new string('-', 50));
                        }
                        else
                        {
                            info.AppendLine($"  [{item.Name}] - Not accessible or doesn't exist");
                        }
                    }
                    catch
                    {
                        info.AppendLine($"  [{item.Name}] - Access denied or path invalid");
                    }
                }
                
                return info;
            }

            public static StringBuilder DisplayChangesTable()
            {
                StringBuilder table = new StringBuilder();
                table.AppendLine("  ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                table.AppendLine("  ║                                            SYSTEM IDENTIFIERS STATUS TABLE                                         ║");
                table.AppendLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                table.AppendLine();
                
                // Table header
                table.AppendLine("  ┌─────────────────────────────┬─────────────────┬─────────────────┬─────────────────┬─────────────────────────────┐");
                table.AppendLine("  │ IDENTIFIER TYPE             │ STATUS          │ RESTART REQ     │ VALUE TYPE      │ CURRENT VALUE               │");
                table.AppendLine("  ├─────────────────────────────┼─────────────────┼─────────────────┼─────────────────┼─────────────────────────────┤");

                // Define all system identifiers that can be checked/spoofed
                var identifiers = new[]
                {
                    new { Name = "Hardware Profile GUID", Path = @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001", Key = "HwProfileGuid", RestartReq = "NO", Command = "!hwid" },
                    new { Name = "Machine GUID", Path = @"SOFTWARE\Microsoft\Cryptography", Key = "MachineGuid", RestartReq = "NO", Command = "!guid" },
                    new { Name = "Computer Name", Path = @"SYSTEM\CurrentControlSet\Control\ComputerName\ActiveComputerName", Key = "ComputerName", RestartReq = "YES", Command = "!pcName" },
                    new { Name = "Product ID", Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", Key = "ProductID", RestartReq = "NO", Command = "!productId" },
                    new { Name = "MAC Address (Adapter 1)", Path = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0001", Key = "NetworkAddress", RestartReq = "ADAPTER", Command = "!mac" },
                    new { Name = "MAC Address (Adapter 2)", Path = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0002", Key = "NetworkAddress", RestartReq = "ADAPTER", Command = "!mac" },
                    new { Name = "MAC Address (Adapter 3)", Path = @"SYSTEM\CurrentControlSet\Control\Class\{4d36e972-e325-11ce-bfc1-08002be10318}\0003", Key = "NetworkAddress", RestartReq = "ADAPTER", Command = "!mac" },
                    new { Name = "System Product Name", Path = @"SYSTEM\CurrentControlSet\Control\SystemInformation", Key = "SystemProductName", RestartReq = "NO", Command = "!motherboard" },
                    new { Name = "System Manufacturer", Path = @"SOFTWARE\Microsoft\Windows\CurrentVersion", Key = "SystemManufacturer", RestartReq = "NO", Command = "!motherboard" },
                    new { Name = "BIOS Date", Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", Key = "SystemBiosDate", RestartReq = "NO", Command = "!bios" },
                    new { Name = "Install Date", Path = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion", Key = "InstallDate", RestartReq = "NO", Command = "!bios" },
                    new { Name = "Volume Serial Number", Path = @"SYSTEM\CurrentControlSet\Control\IDConfigDB\Hardware Profiles\0001", Key = "VolumeSerialNumber", RestartReq = "NO", Command = "!volume" }
                };

                foreach (var item in identifiers)
                {
                    try
                    {
                        Regedit reg = new Regedit(item.Path);
                        string value = reg.Read(item.Key);
                        string status = value != "ERR" ? "ACCESSIBLE" : "NOT FOUND";
                        string valueType = DetectRegistryValueType(value);
                        string displayValue = value != "ERR" ? (value.Length > 25 ? value.Substring(0, 22) + "..." : value) : "N/A";
                        
                        // Determine if value looks spoofed
                        bool looksOriginal = IsOriginalValue(item.Key, value);
                        if (status == "ACCESSIBLE" && !looksOriginal)
                        {
                            status = "MODIFIED";
                        }
                        
                        table.AppendLine($"  │ {item.Name,-27} │ {status,-15} │ {item.RestartReq,-15} │ {valueType,-15} │ {displayValue,-27} │");
                    }
                    catch
                    {
                        table.AppendLine($"  │ {item.Name,-27} │ {"ACCESS DENIED",-15} │ {item.RestartReq,-15} │ {"UNKNOWN",-15} │ {"ACCESS DENIED",-27} │");
                    }
                }
                
                table.AppendLine("  └─────────────────────────────┴─────────────────┴─────────────────┴─────────────────┴─────────────────────────────┘");
                table.AppendLine();
                table.AppendLine("  ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                table.AppendLine("  ║                                              RESTART REQUIREMENTS                                                    ║");
                table.AppendLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                table.AppendLine("  │ NO      = Changes take effect immediately                                                                           │");
                table.AppendLine("  │ YES     = Full system reboot required                                                                               │");
                table.AppendLine("  │ ADAPTER = Network adapter disable/enable or reboot                                                                  │");
                table.AppendLine("  └──────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘");
                table.AppendLine();
                table.AppendLine("  ╔══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╗");
                table.AppendLine("  ║                                                STATUS LEGEND                                                        ║");
                table.AppendLine("  ╚══════════════════════════════════════════════════════════════════════════════════════════════════════════════════╝");
                table.AppendLine("  │ ACCESSIBLE   = Registry key exists and can be read                                                                  │");
                table.AppendLine("  │ MODIFIED     = Value appears to have been changed from original                                                     │");
                table.AppendLine("  │ NOT FOUND    = Registry key doesn't exist or is empty                                                               │");
                table.AppendLine("  │ ACCESS DENIED = Registry key exists but cannot be accessed (run as admin)                                          │");
                table.AppendLine("  └──────────────────────────────────────────────────────────────────────────────────────────────────────────────────┘");
                
                return table;
            }
            
            private static bool IsOriginalValue(string keyName, string value)
            {
                if (value == "ERR" || string.IsNullOrEmpty(value)) return true;
                
                // Check for patterns that suggest spoofed values
                switch (keyName.ToLower())
                {
                    case "computername":
                        // Original computer names usually don't start with "DESKTOP-" + random chars
                        return !value.StartsWith("DESKTOP-") || value.Length < 15;
                    
                    case "networkaddress":
                        // Original MACs are usually not set in registry (would be ERR)
                        return false; // If it exists in registry, it's probably spoofed
                    
                    case "systemproductname":
                        // Check if it starts with "MB" which our spoofer uses
                        return !value.StartsWith("MB");
                    
                    case "volumeserialnumber":
                        // Check if it's exactly 8 hex chars (our pattern)
                        return !(value.Length == 8 && value.All(c => "0123456789ABCDEF".Contains(c)));
                    
                    case "machineguid":
                    case "hwprofileguid":
                        // GUIDs are harder to detect as spoofed, assume original unless recently changed
                        return true;
                    
                    default:
                        return true;
                }
            }
            
            private static string DetectRegistryValueType(string value)
            {
                if (string.IsNullOrEmpty(value) || value == "ERR") return "REG_UNKNOWN";
                
                // Check if it's a GUID format
                if (value.StartsWith("{") && value.EndsWith("}") && value.Length == 38)
                    return "REG_SZ (GUID)";
                
                // Check if it's a hex string (like MAC address)
                if (value.Length == 12 && value.All(c => "0123456789ABCDEF".Contains(c)))
                    return "REG_SZ (HEX)";
                
                // Check if it's numeric
                if (int.TryParse(value, out _))
                    return "REG_DWORD";
                
                // Check if it's a date
                if (DateTime.TryParse(value, out _))
                    return "REG_SZ (DATE)";
                
                return "REG_SZ";
            }
        }

        public static class Utilities
        {
            private static Random rand = new Random();
            public const string Alphabet = "ABCDEF0123456789";

            public static string GenerateString(int size)
            {
                char[] array = new char[size];
                for (int i = 0; i < size; i++)
                {
                    array[i] = Alphabet[rand.Next(Alphabet.Length)];
                }
                return new string(array);
            }

            public static string GenerateMacAddress()
            {
                char[] array = new char[12];
                for (int i = 0; i < 12; i++)
                {
                    array[i] = Alphabet[rand.Next(Alphabet.Length)];
                }
                return new string(array);
            }
        }
    }
}
