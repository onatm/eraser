﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;

namespace Eraser.Core.Network
{
    public static class NIC
    {
        private static readonly string DNSSERVERSEARCHORDER = "DNSServerSearchOrder";
        private static readonly string SETDNSSERVERSEARCHORDER = "SetDNSServerSearchOrder";

        public static IEnumerable<Adapter> GetAdapterList()
        {
            string query = "SELECT * FROM Win32_NetworkAdapterConfiguration WHERE IPEnabled = 1";

            ManagementObjectSearcher mos = new ManagementObjectSearcher(query);
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc)
            {
                string settingId = mo["SettingID"].ToString();
                string name = NetworkInterface.GetAllNetworkInterfaces().Where(x => x.Id == settingId).FirstOrDefault().Name;
                string[] dns = (string[])mo[DNSSERVERSEARCHORDER];

                yield return new Adapter
                {
                    Id = settingId,
                    Name = name,
                    DNS = dns
                };
            }

            yield break;
        }

        public static void SetDNS(string settingId, string[] dnsArray, bool defined)
        {
            string query = string.Format("SELECT * FROM Win32_NetworkAdapterConfiguration WHERE SettingID = {0}", settingId);

            ManagementObjectSearcher mos = new ManagementObjectSearcher(query);
            ManagementObjectCollection moc = mos.Get();

            foreach (ManagementObject mo in moc)
            {
                ManagementBaseObject newMo = mo.GetMethodParameters(SETDNSSERVERSEARCHORDER);

                foreach (string dns in dnsArray)
                {
                    string[] segments = dns.Split('.');

                    if (segments.Length != 4)
                        throw new IPException("Invalid IP address. It should have 4 octets.");

                    foreach (string segment in segments)
                    {
                        try
                        {
                            byte value = byte.Parse(segment);
                        }
                        catch(Exception ex)
                        {
                            throw new IPException("Invalid IP address. Octet values should be lower than 255.", ex);
                        }
                    }
                }

                if (defined)
                    newMo[DNSSERVERSEARCHORDER] = dnsArray;
                else
                    newMo[DNSSERVERSEARCHORDER] = null;
                
                mo.InvokeMethod(SETDNSSERVERSEARCHORDER, newMo, null);

                break;
            }
        }
    }
}
