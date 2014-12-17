using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace DungeonCrawl
{
    class MainData
    {
        public static bool developerMode = false;
        public static bool xboxMode = true;
        public static bool rumbleOn = true;
        public static bool fullScreen = true;

        //AUDIO
        public static AudioEngine audioEngine;
        public static WaveBank waveBank;
        public static SoundBank soundBank;

        public static AudioCategory soundEffects;
        // public static AudioCategory music;
    }
}
