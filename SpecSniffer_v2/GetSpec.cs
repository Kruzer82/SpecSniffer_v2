using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class GetSpec
    {
        public static string Manufacturer()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Manufacturer"));
        }

        public static string Model()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model"));
        }

        public static string Cpu()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_Processor", "Name"));
        }

        public static string Serial()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_SystemEnclosure", "SerialNumber"));
        }

        public static List<string> Gpu()
        {
            var strings = GetFromWmi("root\\CIMV2", "Win32_VideoController", "Caption")
                .OfType<string>()
                .Select(s => s)
                .ToList();

            return strings;
        }

        public static string OpticalDrive()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_CDROMDrive", "MediaType"));
        }

        public static string Ram()
        {
            var ramList = GetFromWmi("root\\CIMV2", "Win32_PhysicalMemory", "Capacity")
                .OfType<ulong>()
                .Select(s => s / (1024 * 1024 * 1024))
                .Select(Convert.ToInt32)
                .ToList();

            var ramFull = string.Format($"{ramList.Sum().ToString()}GB ({string.Join("+", ramList)})");

            return ramFull;
        }

        public static string Diagonal()
        {
            var diagonalList = new List<double>
            {
                10.1,
                11.6,
                12,
                12.5,
                14,
                15.6,
                17.3,
                18,
                19,
                20,
                20.1,
                21,
                21.3,
                22,
                22.2,
                23,
                24,
                26,
                27
            };

            double verticalSize = 0;
            double horizontalSize = 0;
            double diagonal = 0;

            try
            {
                verticalSize =
                    Convert.ToDouble(
                        GetFromWmi("\\root\\wmi", "WmiMonitorBasicDisplayParams", "MaxVerticalImageSize")[0]) / 2.54;

                horizontalSize =
                    Convert.ToDouble(
                        GetFromWmi("\\root\\wmi", "WmiMonitorBasicDisplayParams", "MaxHorizontalImageSize")[0]) / 2.54;
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error occurred during reading diagonal.
                               {ex.Message}");
            }

            diagonal = Math.Sqrt(verticalSize * verticalSize + horizontalSize * horizontalSize);

            var roundedDiagonal = diagonalList.Select(n => new {n, distance = Math.Abs(n - diagonal)})
                .OrderBy(p => p.distance)
                .First()
                .n;

            return string.Format($"{roundedDiagonal}");
        }

        public static string ResName()
        {
            var resolution = Msg.WmiError;

            var resNameDictionary = new Dictionary<string, string>()
            {
                {"1280x1024", "SXGA"},
                {"1360x768", "HD"},
                {"1366x768", "HD"},
                {"1600x900", "HD+"},
                {"1920x1080", "FHD"},
                {"1280x800", "WXGA"},
                {"1280x768", "WXGA"},
                {"1280x720", "WXGA"},
                {"1440x900", "WXGA"},
                {"1680x1050", "WSXGA"},
                {"1920x1200", "WUXGA"},
                {"1152x864", "XGA+"},
                {"1024x768", "XGA"},
                {"1024x600", "WSVGA"},
                {"800x600", "SVGA"},
                {"2560x1440", "WQHD"},
                {"3840x2160", "UHD"},
                {"4096x2160", "UHD"},
                {"2560×1600", "WQXGA"}
            };

            try
            {
                resolution = GetFromWmi("root\\CIMV2", "Win32_VideoController", "CurrentHorizontalResolution")[0] +
                             "x" + GetFromWmi("root\\CIMV2", "Win32_VideoController", "CurrentVerticalResolution")[0];
            }
            catch (Exception ex)
            {
                MessageBox.Show($@"Error occurred during reading resolution.
                               {ex.Message}");
            }

            foreach (var resName in resNameDictionary)
                if (resName.Key == resolution)
                {
                    resolution = resName.Value;
                    break;
                }

            return resolution;
        }

        public static string OsName()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_OperatingSystem", "Caption"));
        }

        public static string OsBuild()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_OperatingSystem", "buildnumber"));
        }

        public static string OsLanguages()
        {
            var listString = GetFromWmiAsStringArr("root\\CIMV2", "Win32_OperatingSystem", "MUILanguages")
                .Select(s => s = s.Remove(0, 3))
                .ToArray();

            return string.Join(" ", listString);
        }

        public static string OsLicence()
        {
            return ReturnWmiString(GetFromWmi("root\\CIMV2", "SoftwareLicensingService", "OA3xOriginalProductKey"));
        }

        public static int BatteryCharge()
        {
            UInt16 charge = 0;

            try
            {
                charge = (UInt16) GetFromWmi("root\\CIMV2", "Win32_Battery", "EstimatedChargeRemaining")[0];
            }
            catch (Exception)
            {
                // ignored
            }

            return charge;
        }

        public static UInt32 BatterHealth()
        {
            UInt32 maxBatteryCapacity = 0;
            UInt32 currentBatteryCapacity = 0;
            UInt32 health = 0;

            try
            {
                maxBatteryCapacity = (UInt32) GetFromWmi("root\\WMI", "BatteryStaticData", "DesignedCapacity")[0];
                currentBatteryCapacity =
                    (UInt32) GetFromWmi("root\\WMI", "BatteryFullChargedCapacity", "FullChargedCapacity")[0];

                health = (currentBatteryCapacity * 100) / maxBatteryCapacity;
            }
            catch (Exception)
            {
                // ignored
            }

            return health;
        }

        public static int ChargeRate()
        {
            int chargeRate = 0;

            try
            {
                if ((bool) GetFromWmi("root\\WMI", "BatteryStatus", "Charging")[0] == true)
                {
                    chargeRate = (int) GetFromWmi("root\\WMI", "BatteryStatus", "Charging")[0];
                }
                else if ((bool) GetFromWmi("root\\WMI", "BatteryStatus", "Discharging")[0] == true)
                {
                    chargeRate = -(int) GetFromWmi("root\\WMI", "BatteryStatus", "DischargeRate")[0];
                }
            }
            catch (Exception)
            {
                //ignored
            }

            return chargeRate;
        }

        public static bool WwanPresence()
        {
            bool wwan = false;
            var devices = GetFromWmi("root\\StandardCimv2", "MSFT_NetAdapter", "NdisPhysicalMedium");
            foreach (var device in devices)
            {
                if ((UInt32) device == 8)
                {
                    wwan = true;
                    break;
                }
            }

            return wwan;
        }

        public static bool BluetoothPresence()
        {
            bool bt = false;
            var devices = GetFromWmi("root\\StandardCimv2", "MSFT_NetAdapter", "NdisPhysicalMedium");
            foreach (var device in devices)
            {
                if ((UInt32)device == 10)
                {
                    bt = true;
                    break;
                }
            }

            return bt;
        }

        public static bool FprPresence()
        {
            bool fpr = false;

            try
            {
                foreach (ManagementObject obj in new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT PNPClass FROM  Win32_PnPEntity  WHERE PNPClass='BIOMETRIC'").Get())
                    fpr = true;

                if (fpr == false)
                {
                    foreach (ManagementObject obj in new ManagementObjectSearcher("root\\CIMV2", "SELECT PNPClass,Description FROM  Win32_PnPEntity WHERE PNPClass='CVAULT'").Get())
                    {
                        if (((string)obj["Description"]).Contains("w/ Fingerprint"))
                        {
                            fpr = true;
                            break;
                        }
                    }
                }
            }
            catch
            {
                //ignored
            }

            return fpr;
        }

        public static string DriverStatus()
        {
            bool status = true;
            var devices =
                GetFromWmi("root\\CIMV2", "Win32_PnPEntity", "ConfigManagerErrorCode");

            foreach (var device in devices)
            {
                if ((UInt32)device != 0)
                {
                    status = false;
                    break;
                }
            }

            return status == true ? "OK" : "Warning";
        }



        private static string ReturnWmiString(IEnumerable<object> wmiObject)
        {
            IEnumerable<string> stringList = wmiObject.OfType<string>().Select(s => s).ToList();

            return string.Join("/", stringList.ToArray());
        }

        private static List<object> GetFromWmi(string scopeNamespace, string scopeClass, string scopeProperty)
        {
            var queryReturn = new List<object>();
            try
            {
                foreach (var queryObj in new ManagementObjectSearcher(@scopeNamespace,
                    $"SELECT {scopeProperty} FROM {scopeClass}").Get())
                    queryReturn.Add(queryObj[scopeProperty]);
            }
            catch (Exception)
            {
                //MessageBox.Show($@"Error occurred during reading {scopeProperty}.
                //               {ex.Message}");
            }

            return queryReturn;
        }

        private static string[] GetFromWmiAsStringArr(string scopeNamespace, string scopeClass, string scopeProperty)
        {
            string[] queryReturn = null;
            try
            {
                foreach (var queryObj in new ManagementObjectSearcher(@scopeNamespace,
                    $"SELECT {scopeProperty} FROM {scopeClass}").Get())
                    queryReturn = (string[]) (queryObj[scopeProperty]);
            }
            catch (Exception)
            {
                //MessageBox.Show($@"Error occurred during reading {scopeProperty}.
                //               {ex.Message}");
            }

            return queryReturn;
        }
    }
}
