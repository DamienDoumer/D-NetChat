using System;
using System.Media;

namespace DoumeraNetChat
{
    class DoumeraSoundPlayer
    {
        private SoundPlayer player;

        public DoumeraSoundPlayer()
        {
            player = new SoundPlayer();
        }

        public void setSoundPath(string path)
        {
            player.SoundLocation = path;
        }

        public String getSoundPath()
        {
            return player.SoundLocation;
        }
        public void Play()
        {
            try
            {
                player.Load();
                player.Play();
            }
            catch (Exception e)
            {
                
            }
        }

        public void OnTransferTerminated(object sender, EventArgs e)
        {
            Play();
        }

        public void Stop()
        {
            player.Stop();
        }
    }
}
