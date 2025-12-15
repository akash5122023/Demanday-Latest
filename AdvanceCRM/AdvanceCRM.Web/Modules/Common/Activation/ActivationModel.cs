using System.Net.NetworkInformation;

namespace AdvanceCRM.Common.Activation
{
    public class ActivationModel
    {
        public NetworkInterface[] nics { get; set; }

        public string modules { get; set; }
        public string users { get; set; }
        public string daterange { get; set; }
        public string requestkey { get; set; }
    }
}