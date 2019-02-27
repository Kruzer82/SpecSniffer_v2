using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SpecSniffer_v2
{
    internal class Pc
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
        private string _hdd1Size;
        private string _hdd1Model;
        private string _hdd1Serial;
        private string _hdd2Size;
        private string _hdd2Model;
        private string _hdd2Serial;
        private string _resolutionName;
        private int _batteryCharge;

        public string Manufacturer
        {
            get => _manufacturer;
            set => _manufacturer = string.IsNullOrWhiteSpace(value) ? Resources.WmiNoData : value;
        }

        public string Model
        {
            get => _model;
            set
            {
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
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                    _serial = Manufacturer.Contains("LENOVO")
                        ? "1S" + GetSpec.Model().ToLower() + value.ToUpper()
                        : value;
                else
                    _serial = "n/a";
            }
        }

        public string Cpu
        {
            get => _cpu;
            set
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
            set => _opticalDrive = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Ram
        {
            get => _ram;
            set => _ram = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd1Size
        {
            get => _hdd1Size;
            set => _hdd1Size = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd1Model
        {
            get => _hdd1Model;
            set => _hdd1Model = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd1Serial
        {
            get => _hdd1Serial;
            set => _hdd1Serial = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd2Size
        {
            get => _hdd2Size;
            set => _hdd2Size = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd2Model
        {
            get => _hdd2Model;
            set => _hdd2Model = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Hdd2Serial
        {
            get => _hdd2Serial;
            set => _hdd2Serial = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public List<string> GpuList { get; set; } = new List<string>();

        public string ResolutionName
        {
            get => _resolutionName;
            set => _resolutionName = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string Diagonal
        {
            get => _diagonal;
            set => _diagonal = string.IsNullOrWhiteSpace(value) ? "n/a" : value + @"""";
        }

        public string OsName
        {
            get => _osName;
            set => _osName = string.IsNullOrWhiteSpace(value) ? "n/a" : value.Replace("Microsoft", "").Trim();
        }

        public string OsBuild
        {
            get => _osBuild;
            set => _osBuild = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string OsLanguage
        {
            get => _osLanguage;
            set => _osLanguage = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public string OsLicenceKey
        {
            get => _osLicenceKey;
            set => _osLicenceKey = string.IsNullOrWhiteSpace(value) ? "n/a" : value;
        }

        public uint BatteryHealth { get; set; }

        public int BatteryChargeRate { get; set; }

        public int BatteryCharge
        {
            get => _batteryCharge;
            set => _batteryCharge = value > 100 ? 100  : value;
        }


        public bool Wlan { get; set; }
        public bool Wwan { get; set; }
        public bool BlueTooth { get; set; }
        public bool Camera { get; set; }
        public bool FingerPrint { get; set; }
        public string DriverStatus { get; set; }

        public Pc()
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
            BatteryCharge = 0;
            BatteryChargeRate = 0;

            Hdd1Size = Resources.NoHdd;
            Hdd1Model = Resources.NoHdd;
            Hdd1Serial = Resources.NoHdd;
            Hdd2Size = Resources.NoHdd;
            Hdd2Model = Resources.NoHdd;
            Hdd2Serial = Resources.NoHdd;

            Wlan = false;
            Wwan = false;
            BlueTooth = false;
            Camera = false;
            FingerPrint = false;
            DriverStatus = Resources.PcNotLoaded;

        }
    }
}