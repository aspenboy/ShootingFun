using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ShootingFun
{
    public class SoundManager
    {
        private SoundEffect laserEffect;
        private SoundEffect explosionEffect;

        public SoundManager(ContentManager content)
        {
            //backgroundMusic = content.Load<Song>(@"sounds\background");
            laserEffect = content.Load<SoundEffect>(@"sounds\laser");
            explosionEffect = content.Load<SoundEffect>(@"sounds\explosion");
        }

        public void PlayBackgroundMusic()
        {
            if (MediaPlayer.GameHasControl)
            {
                //MediaPlayer.Play(backgroundMusic);
                MediaPlayer.IsRepeating = true;
            }
        }

        public void PlayShotSound()
        {
            laserEffect.Play();
        }

        public void PlayExplosionSound()
        {
            explosionEffect.Play();
        }
    }
}
