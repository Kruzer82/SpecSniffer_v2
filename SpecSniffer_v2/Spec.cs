using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SpecSniffer_v2
{
    internal class Spec
    {
        private string _serial;
        private string _model;
        private string _cpu;
        private string _opticalDrive;
        private string _manufacturer;
        private string _ram;
        private string _diagonal;
        private string _osName;
        private string _osBuild;
        private string _osLanguage;
        private string _osLicenceKey;
        private string _resolutionName;

        public string Manufacturer
        {
            get => _manufacturer;
            private set => _manufacturer = string.IsNullOrWhiteSpace(value) ? Resources.WmiNoData : value.ToUpper();
        }

        public string ModelRaw { get; private set; }
        public string Model
        {
            get => _model;
            private set
            {
                ModelRaw = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var model = new StringBuilder(value);
                    switch (Manufacturer)
                    {
                        case "DELL":
                        {
                            model.Replace("Latitude", "");
                            model.Replace("OptiPlex", "");
                            model.Replace("Precision", "");
                            model.Replace("Workstation", "");
                            model.Replace("Vostro", "");
                            model.Replace("non-vPro", "");
                            model.Replace("Tower", "");
                            model.Replace("AIO", "");
                            model.Replace("Inspiron", "");
                            break;
                        }

                        case "HEWLETT-PACKARD":
                        {
                            model.Replace("All-in-One", " AiO ");
                            model.Replace("Workstation", "");
                            model.Replace("EliteBook", "");
                            model.Replace("Precision", "");
                            model.Replace("EliteDesk", "");
                            model.Replace("Notebook", "");
                            model.Replace("ProBook", "");
                            model.Replace("Compaq", "");
                            model.Replace("Elite", "");
                            model.Replace("Desk", "");
                            model.Replace("Pro", "");
                            model.Replace("HP", "");
                            model.Replace("PC", "");

                            //  model.Replace("TWR", "");
                            //  model.Replace("SFF", "");

                            break;
                        }

                        case "LENOVO":
                        {
                            var lenovoDictionary = new Dictionary<string, string>
                            {
                                {"4236K63", "T420"},
                                {"2349P25", "T430"},
                                {"2349FC4", "T430"},
                                {"2347G2U", "T430"},
                                {"2351BH6", "T430"},
                                {"20AWS1U308", "T440p"},
                                {"20AWS2CH00", "T440p"},
                                {"20AWS1DA09", "T440p"},
                                {"42404BG", "T520"},
                                {"42435UG", "T520"},
                                {"20BH002QMS", "W540"},
                                {"2429AQ9", "T530"},
                                {"24295XG", "T530"},
                                {"20BE003YMH", "T540p"},
                                {"20BE0088MS", "T540p"},
                                {"7033GQ1", "M91p"},
                                {"10A6A0WB00", "M93p"},
                                {"10A7000LMH", "M93p"},
                                {"10M90004UK", "M710t"},
                                {"10MQS2KG00", "M710q"},
                                {"20CDS04J00", "S1 Yoga"},
                                {"20C3S0YQ00", "ThinkPad 10"},
                                {"10AAS0G50L", "M93p Tiny"},
                                {"20FRS4U802", "X1 Yoga 1"}
                            };

                            foreach (var len in lenovoDictionary)
                            {
                                if (len.Key != model.ToString()) continue;
                                model = new StringBuilder(len.Value);
                                break;
                            }

                            break;
                        }

                        case "FUJITSU":
                        {
                            model.Replace("LIFEBOOK", "");
                            break;
                        }

                        default:
                            _model = model.ToString();
                            break;
                    }

                    _model = Regex.Replace(model.ToString(), @"\s+", " ").Trim();
                }
                else
                {
                    _model = Resources.WmiNoData;
                }
            }
        }

        public string Serial
        {
            get => _serial;
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _serial = Manufacturer.Contains("LENOVO")
                        ? "1S" + ModelRaw.ToLower() + value.ToUpper()
                        : value;
                else
                    _serial = "n/a";
            }
        }

        public string Cpu
        {
            get => _cpu;
            private set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    var cpuTrim = new StringBuilder(value);
                    cpuTrim.Replace("Intel(R) Core(TM)", "");
                    cpuTrim.Replace("Intel(R) Xeon(R)", "");
                    cpuTrim.Replace("Intel(R) Atom(TM)", "");
                    cpuTrim.Replace("Intel(R) Pentium(R)", "");
                    cpuTrim.Replace("CPU", "");

                    _cpu = Regex.Replace(cpuTrim.ToString(), @"\s+", " ").Trim();
                }
                else
                {
                    _cpu = "n/a";
                }
            }
        }

        public string OpticalDrive
        {
            get => _opticalDrive;
            private set => _opticalDrive = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Ram
        {
            get => _ram;
            private set => _ram = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public List<string> GpuList { get; private set; } = new List<string>();

        public string ResolutionName
        {
            get => _resolutionName;
            private set => _resolutionName = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Diagonal
        {
            get => _diagonal;
            private set => _diagonal = string.IsNullOrWhiteSpace(value) ? "n/a" : value + @"""";
        }

        public string OsName
        {
            get => _osName;
            private set => _osName = string.IsNullOrWhiteSpace(value) ? "n/a" : value.Replace("Microsoft", "").Trim();
        }

        public string OsBuild
        {
            get => _osBuild;
            private set => _osBuild = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string OsLanguage
        {
            get => _osLanguage;
            private set => _osLanguage = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string OsLicenceKey
        {
            get => _osLicenceKey;
            private set => _osLicenceKey = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public uint BatteryHealth { get; private set; }

        public bool Wlan { get; private set; }

        public bool Wwan { get; private set; }

        public bool BlueTooth { get; private set; }

        public bool Camera { get; private set; }

        public bool FingerPrint { get; private set; }

        public string DriverStatus { get; private set; }

        public Spec()
        {
            Manufacturer = Resources.PcNotLoaded;
            Model = Resources.PcNotLoaded;
            Serial = Resources.PcNotLoaded;
            Cpu = Resources.PcNotLoaded;
            Ram = Resources.PcNotLoaded;
            OpticalDrive = Resources.PcNotLoaded;
            ResolutionName = Resources.PcNotLoaded;
            Diagonal = Resources.PcNotLoaded;
            OsName = Resources.PcNotLoaded;
            OsBuild = Resources.PcNotLoaded;
            OsLanguage = Resources.PcNotLoaded;
            OsLicenceKey = Resources.PcNotLoaded;

            GpuList.Add(Resources.PcNotLoaded);
            BatteryHealth = 0;

            Wlan = false;
            Wwan = false;
            BlueTooth = false;
            Camera = false;
            FingerPrint = false;
            DriverStatus = Resources.PcNotLoaded;
        }


        public void GetManufacturer()
        {
            Manufacturer = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Manufacturer"));
        }

        public void GetModel()
        {
            Model = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_ComputerSystem", "Model"));
        }

        public void GetCpu()
        {
            Cpu = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_Processor", "Name"));
        }

        public void GetSerial()
        {
            Serial = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_SystemEnclosure", "SerialNumber"));
        }

        public void GetGpu()
        {
            GpuList = GetFromWmi("root\\CIMV2", "Win32_VideoController", "Caption")
                .OfType<string>()
                .Select(s => s)
                .ToList();
        }

        public void GetOpticalDrive()
        {
            OpticalDrive = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_CDROMDrive", "MediaType"));
        }

        public void GetRam()
        {
            var ramList = GetFromWmi("root\\CIMV2", "Win32_PhysicalMemory", "Capacity")
                .OfType<ulong>()
                .Select(s => s / (1024 * 1024 * 1024))
                .Select(Convert.ToInt32)
                .ToList();

            var ramFull = string.Format($"{ramList.Sum().ToString()}GB ({string.Join("+", ramList)})");

            Ram = ramFull;
        }

        public void GetDiagonal()
        {
            double verticalSize = 0;
            double horizontalSize = 0;
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

            var diagonal = Math.Sqrt(verticalSize * verticalSize + horizontalSize * horizontalSize);

            var roundedDiagonal = Resources.DiagonalList.Select(n => new {n, distance = Math.Abs(n - diagonal)})
                .OrderBy(p => p.distance)
                .First()
                .n;

            Diagonal = roundedDiagonal.ToString();
        }

        public void GetResName()
        {
            var resolution = Resources.WmiError;
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

            foreach (var resName in Resources.ResolutionNames)
                if (resName.Key == resolution)
                {
                    resolution = resName.Value;
                    break;
                }

            ResolutionName = resolution;
        }

        public void GetOsName()
        {
            OsName = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_OperatingSystem", "Caption"));
        }

        public void GetOsBuild()
        {
            OsBuild = ReturnWmiString(GetFromWmi("root\\CIMV2", "Win32_OperatingSystem", "buildnumber"));
        }

        public void GetOsLanguages()
        {
            var listString = GetFromWmiAsStringArr("root\\CIMV2", "Win32_OperatingSystem", "MUILanguages")
                .Select(s => s = s.Remove(0, 3))
                .ToArray();

            OsLanguage = string.Join(" ", listString);
        }

        public void GetOsLicence()
        {
            OsLicenceKey =
                ReturnWmiString(GetFromWmi("root\\CIMV2", "SoftwareLicensingService", "OA3xOriginalProductKey"));
        }

        public string BatteryCharge()
        {
            try
            {
                ushort charge = (ushort) GetFromWmi("root\\CIMV2", "Win32_Battery", "EstimatedChargeRemaining")[0];
                if (charge > 100)
                    return "100%";
                else
                    return charge+"%";
            }
            catch (Exception)
            {
                return  "n/a";
            }
        }

        public void GetBatterHealth()
        {
            try
            {
                var maxBatteryCapacity = (uint) GetFromWmi("root\\WMI", "BatteryStaticData", "DesignedCapacity")[0];
                var currentBatteryCapacity =
                    (uint) GetFromWmi("root\\WMI", "BatteryFullChargedCapacity", "FullChargedCapacity")[0];

                BatteryHealth = currentBatteryCapacity * 100 / maxBatteryCapacity;
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public int BatteryChargeRate()
        {
            try
            {
                if ((bool) GetFromWmi("root\\WMI", "BatteryStatus", "Charging")[0])
                    return (int) GetFromWmi("root\\WMI", "BatteryStatus", "ChargeRate")[0];
                else if ((bool) GetFromWmi("root\\WMI", "BatteryStatus", "Discharging")[0])
                    return -(int) GetFromWmi("root\\WMI", "BatteryStatus", "DischargeRate")[0];
                else
                    return 0;
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public void GetNetAdapters()
        {
            var devices = GetFromWmi("root\\StandardCimv2", "MSFT_NetAdapter", "NdisPhysicalMedium");
            foreach (var device in devices)
                switch ((uint) device)
                {
                    case 1:
                        Wlan = true;
                        break;

                    case 8:
                        Wwan = true;
                        break;
                    case 9:
                        goto case 1;

                    case 10:
                        BlueTooth = true;
                        break;
                }
        }

        public void GetFprPresence()
        {
            try
            {
                foreach (ManagementObject obj in new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT PNPClass FROM  Win32_PnPEntity  WHERE PNPClass='BIOMETRIC'").Get())
                {
                    FingerPrint = true;
                    break;
                }

                if (FingerPrint == false)
                    foreach (ManagementObject obj in new ManagementObjectSearcher("root\\CIMV2",
                        "SELECT PNPClass,Description FROM  Win32_PnPEntity WHERE PNPClass='CVAULT'").Get())
                        if (((string) obj["Description"]).Contains("w/ Fingerprint"))
                        {
                            FingerPrint = true;
                            break;
                        }
            }
            catch
            {
                //ignored
            }
        }

        public void GetCameraPresence()
        {
            try
            {
                foreach (ManagementObject obj in new ManagementObjectSearcher("root\\CIMV2",
                    "SELECT PNPClass FROM  Win32_PnPEntity  WHERE PNPClass='Camera'").Get())
                {
                    Camera = true;
                    break;
                }
            }
            catch
            {
                //ignored
            }
        }

        public void GetDriverStatus()
        {
            var status = true;
            var devices =
                GetFromWmi("root\\CIMV2", "Win32_PnPEntity", "ConfigManagerErrorCode");

            foreach (var device in devices)
                if ((uint) device != 0)
                {
                    status = false;
                    break;
                }

            DriverStatus = status ? "OK" : "Warning";
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
                foreach (var queryObj in new ManagementObjectSearcher(scopeNamespace,
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
                foreach (var queryObj in new ManagementObjectSearcher(scopeNamespace,
                    $"SELECT {scopeProperty} FROM {scopeClass}").Get())
                    queryReturn = (string[]) queryObj[scopeProperty];
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