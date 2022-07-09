using CSCore.CoreAudioAPI;
using System.Collections.Generic;
using System.Linq;

namespace FishingBotFoffosEdition
{
    internal class AudioManager
    {
        // Gets the default device for the system
        public static MMDevice GetDefaultRenderDevice()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);
            }
        }

        public static List<MMDevice> GetRenderDevices()
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                return enumerator.EnumAudioEndpoints(DataFlow.Render, DeviceState.Active).ToList();
            }
        }
    }
}
