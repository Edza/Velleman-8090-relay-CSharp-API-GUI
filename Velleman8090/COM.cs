using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Text.RegularExpressions;

namespace Velleman8090
{
    static class COM
    {
        public static string GetPort()
        {
            ManagementPath path = new ManagementPath();
            ManagementClass devs = null;
            path.Server = ".";
            path.NamespacePath = @"root\CIMV2";
            path.RelativePath = @"Win32_PnPentity";
            using (devs = new ManagementClass(new ManagementScope(path), path, new ObjectGetOptions(null, new TimeSpan(0, 0, 0, 2), true)))
            {
                ManagementObjectCollection moc = devs.GetInstances();

                var usbCOMDevices = ((IEnumerable)moc).OfType<ManagementObject>()
                                  .Where(i => i.ToString().Contains("USB") && i.Properties["Caption"].Value.ToString().Contains("COM"))
                                  .ToList();

                if (usbCOMDevices.Count != 1)
                    throw new Exception("There is no USB COM Device connected or there are 2 or more of them!");



                var res = Regex.Match(usbCOMDevices.Single().Properties["Caption"].Value.ToString(), "COM[0-9]");

                if (!res.Success)
                    throw new Exception("Unknwon caption format");

                string port = res.Value;

                return port;
            }
        }
    }
}
