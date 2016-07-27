using System;
using System.Collections.Generic;
using System.Linq;

namespace DoumeraNetChat.VirtualWifiHotspotCreator
{
    static class Wifi//this class is used to call the wifi creator at any point in teh assembly
    {
        /// <summary>
        /// Creates a vitual wifi hotspot this instance is static
        /// </summary>
       
        static private WifiHotspot wifi = new WifiHotspot();
        static bool eventt = true;
        /// <summary>
        /// Determines if the StopHotspot event is to be called or not
        /// </summary>
        public static void Event(bool e)
        {
            eventt = e;
        }

        /// <summary>
        /// Sets the hotspot's Name goten from the user of the class
        /// </summary>
        /// <param name="Name"></param>
        public static void SetSSID(string Name)
        {
            wifi.HotspotSSID = Name;
        }
        /// <summary>
        /// Sets the hotspot's Key gottenfrom the user of the class
        /// </summary>
        /// <param name="Key"></param>
        public static void SetKey(string Key)
        {
            wifi.HotspotKey = Key;
        }
        /// <summary>
        /// Starts the hotspot
        /// </summary>
        public static void StartHotspot()
        {
            wifi.StartHostedNetwork();
        }
        /// <summary>
        /// Stops the hotspot
        /// </summary>
        public static void StopHotspot()
        {
            wifi.StopHostedNetwork();
        }
        /// <summary>
        /// this is to get the hotspot key
        /// </summary>
        /// <returns>The wifi hotspots key</returns>
        public static string GetKey()
        {
            return wifi.HotspotKey;
        }
        /// <summary>
        /// This is to get the Hotspots SSID
        /// </summary>
        /// <returns>THe WifiHotspots SSID </returns>
        public static string GetSSID()
        {
            return wifi.HotspotSSID;
        }
        /// <summary>
        /// Handles the event which marks the starting process of the wifi hotspot
        /// </summary>
        /// <param name="HotspotStarting">Takes a delegate coresponding to the hotspotstarting delegate of 
        ///  the WifiHotspot class</param>
        public static void OnHotspotStarting(WifiHotspot.HotspotStartingEventHandler HotspotStarting)
        {
            wifi.HotspotStarting += HotspotStarting;
        }
        /// <summary>
        /// Handles the event which marks the start of the wifi hotspot
        /// </summary>
        /// <param name="HotspotStarting">Takes a delegate coresponding to the hotspotstarted delegate of 
        ///  the WifiHotspot class</param>
        public static void OnHotspotStarted(WifiHotspot.HotspotStartedEventHandler HotspotStarted)
        {
            wifi.HotspotStarted += HotspotStarted;
        }
        /// <summary>
        /// Handles the event which marks the stop of the wifi hotspot
        /// </summary>
        /// <param name="HotspotStarting">Takes a delegate coresponding to the hotspotstoped delegate of 
        ///  the WifiHotspot class</param>
        public static void OnHotspotStopped(WifiHotspot.HotspotStoppedEventHandler HotspotStopped)
        {
            if (eventt)
            {
                wifi.HotspotStopped += HotspotStopped;
            }
        }
        /// <summary>
        /// Handles the event which marks the occurance of an error of the wifi hotspot
        /// </summary>
        /// <param name="HotspotStarting">Takes a delegate coresponding to the hotspotstarting delegate of 
        ///  the WifiHotspot class</param>
        public static void OnErrorOccured(WifiHotspot.ErrorOccuredEventHandler ErrorOccured)
        {
            wifi.ErrorOccured += ErrorOccured;
        }
        public static void OnHotspotStopping(WifiHotspot.HotspotStoppingEventHandler hotspotStopping)
        {
            wifi.HotspotStopping += hotspotStopping;
        }
    }
}
