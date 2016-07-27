using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace DoumeraNetChat.VirtualWifiHotspotCreator
{
    class WifiHotspot
    {
        private bool isConnectionEstablished;
        public Boolean IsConnectionEstablished { get { return isConnectionEstablished; } }

        public delegate void HotspotStartedEventHandler(object sender);
        public event HotspotStartedEventHandler HotspotStarted;
        public delegate void HotspotStoppedEventHandler(object Sender);
        public event HotspotStoppedEventHandler HotspotStopped;
        public delegate void ErrorOccuredEventHandler(Object sender, Exception ex);
        public event ErrorOccuredEventHandler ErrorOccured;
        public delegate void HotspotStartingEventHandler(object sender);
        public event HotspotStartingEventHandler HotspotStarting;
        public delegate void HotspotStoppingEventHandler(object sender);
        public event HotspotStoppingEventHandler HotspotStopping;

        private Process WifiProcess;
        private string hotspotKey;
        public string HotspotSSID { get; set; }
        public string HotspotKey
        {
            get { return hotspotKey; }
            set
            {
               hotspotKey = value; 
            }
        }

        /// <summary>
        /// Constructor Which starts the Process for the creation of the virtual Wifi Hotsppot
        /// </summary>
        public WifiHotspot()
        {
            WifiProcess = new Process();
            WifiProcess.StartInfo.CreateNoWindow = true;
            WifiProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
        }

        /// <summary>
        /// Stops the previous hostednetwork and to start the next and it lauches the next Method
        /// </summary>
        /// <Param >None</Param>
        private void FirstProcess()
        {
            WifiProcess.StartInfo.FileName = "netsh";
            WifiProcess.StartInfo.Arguments = " wlan stop hostednetwork";
            try
            {
                using (Process proc = Process.Start(WifiProcess.StartInfo))
                {
                    proc.WaitForExit();
                    SecondProcess();
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, ex);
                }
            }
        }

        /// <summary>
        /// Creates the hostednetwork and Starts the next Method
        /// </summary>
        /// <Param >None</Param>
        private void SecondProcess()
        {
            WifiProcess.StartInfo.FileName = "netsh";
            WifiProcess.StartInfo.Arguments = string.Format(" wlan set hostednetwork mode = allow ssid = {0} key = {1}", 
                HotspotSSID, hotspotKey);
            try
            {
                using (Process proc = Process.Start(WifiProcess.StartInfo))
                {
                    proc.WaitForExit();
                    ThirdProcess();
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, ex);
                }
            }
        }
        /// <summary>
        /// Starts the HostedNetwork
        /// </summary>
        /// <Param >None</Param>
        private void ThirdProcess()
        {
            WifiProcess.StartInfo.FileName = "netsh";
            WifiProcess.StartInfo.Arguments = " Wlan Start HostedNetwork";

            try
            {
                using (Process proc = Process.Start(WifiProcess.StartInfo))
                {
                    proc.WaitForExit();
                    if (HotspotStarted != null)
                    {
                        HotspotStarted(this);
                        isConnectionEstablished = true;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, ex);
                }
            }
        }
        /// <summary>
        /// Stops the hostednetwork 
        /// </summary>
        /// <Param >None</Param>
        public void StopHostedNetwork()
        {
            if(HotspotStopping!=null)
            {
                HotspotStopping(this);
            }
            WifiProcess.StartInfo.FileName = "netsh";
            WifiProcess.StartInfo.Arguments = " Wlan Stop HostedNetwork";
            try
            {
                using (Process proc = Process.Start(WifiProcess.StartInfo))
                {
                    proc.WaitForExit();
                    if (HotspotStopped != null)
                    {
                        HotspotStopped(this);
                        isConnectionEstablished = false;
                    }
                }
            }
            catch (Exception ex)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, ex);
                }
            }
        }
        /// <summary>
        /// Calls a method which launches The private FirstProcess Method 
        /// and checks wether the ssid and key are valid
        /// </summary>
        /// <Param >None</Param>
        public void StartHostedNetwork()
        {
            if (hotspotKey.Length == 0 || HotspotSSID.Length < 1)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, new Exception("You must enter an SSID and a key for your wifi hotspot"));
                }
            }
            else
            if (hotspotKey.Length < 8)
            {
                if (ErrorOccured != null)
                {
                    ErrorOccured(this, new Exception("Your Key must Have at least 8 characters"));
                }
            }
            else
            {
                if (HotspotStarting != null)
                {
                    HotspotStarting(this);
                }
                FirstProcess();
                
            }
        }
    }
}
