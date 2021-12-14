using System;
using System.Collections.Generic;
using System.Text;

namespace Cyclone.MciWrapper
{

    public enum AudioChannels
    {
        Left,
        Right,
        Both,
    }

    public enum FileType
    {
        MpegOrMpeg,
        WmaMp3AifWav,
        Avi,
        WmvAsf,
        Mid
    }

    public enum PlaybackState
    {
        Open,
        Playing,
        Paused,
        Stopeed,
        Closed,
    }

}
