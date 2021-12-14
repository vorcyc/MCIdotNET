using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;

namespace Cyclone.MciWrapper
{

    public class MciCommon
    {

        [DllImport("winmm.dll")]
        internal static extern Int32 mciSendString(
            string command,
            StringBuilder buffer,
            int bufferSize,
            IntPtr hwndCallback);


        private string _file;
        private string _ext;
        private string _shortPath;
        private FileType _fileType;


        public const short VolumeMaxium = 1000;
        public const short VolumeMinium = 0;

        private short _volume = 1000;

        private bool _mute = false;


        private short _rate = 1000;

        public AudioChannels Channel { get; set; }


        public PlaybackState State { get; private set; }

        /// <summary>
        /// 别名
        /// </summary>
        protected string Alias = "Cyclone.MciControl";


        public MciCommon()
        {
            Channel = AudioChannels.Both;
        }



        public virtual void SetFile(string filename)
        {
            _file = filename;
            _shortPath = IOHelper.GetShortPathName(_file);
            _ext = Path.GetExtension(filename).ToLower().Substring(1);

            if (_ext == "mp3" || _ext == "wma" || _ext == "aif" || _ext == "wav")
                _fileType = FileType.WmaMp3AifWav;
            if (_ext == "mid")
                _fileType = FileType.Mid;
        }


        public virtual void Open()
        {
            if (_fileType == FileType.Mid || _fileType == FileType.WmaMp3AifWav)
                mciSendString("Open " + _shortPath + " alias " + Alias, null, 0, IntPtr.Zero);

            State = PlaybackState.Open;
        }

        public void Play()
        {
            var queryString = string.Format("play {0} notify", Alias);
            mciSendString(queryString, null, 0, IntPtr.Zero);
            State = PlaybackState.Playing;
        }

        public void Pause()
        {
            mciSendString("pause " + Alias, null, 0, IntPtr.Zero);
            State = PlaybackState.Paused;
        }



        public void Close()
        {
            mciSendString("close " + Alias, null, 0, IntPtr.Zero);
        }

        public string Filename
        { get { return _file; } }



        #region properties

        public short Volume
        {
            get { return _volume; }
            set
            {
                _volume = MathHelper.FixRange<short>(value, VolumeMinium, VolumeMaxium);
                mciSendString("setaudio " + Alias + " volume to " + _volume, null, 0, IntPtr.Zero);
            }

        }

        public bool Mute
        {
            get { return _mute; }
            set
            {
                _mute = value;
                if (value)
                    mciSendString("set " + Alias + " audio all off", null, 0, IntPtr.Zero);
                else
                    mciSendString("set " + Alias + " audio all on", null, 0, IntPtr.Zero);
            }
        }

        public short Rate
        {
            get { return _rate; }
            set
            {
                _rate = MathHelper.FixRange<short>(value, 0, 2500);
                mciSendString("set " + Alias + " speed " + _rate, null, 0, IntPtr.Zero);
                //针对mdi
                mciSendString("set " + Alias + " tempo " + _rate, null, 0, IntPtr.Zero);
            }
        }


        public int Position
        {
            get
            {
                var sb = new StringBuilder(255);
                mciSendString("Status " + Alias + " position", sb, 255, IntPtr.Zero);
                return int.Parse(sb.ToString());
            }
            set
            {
                mciSendString("seek " + Alias + " to " + value, null, 0, IntPtr.Zero);
            }
        }


        public int Duration
        {
            get
            {
                var sb = new StringBuilder(255);
                var queryString = string.Format("status {0} length", Alias);

                mciSendString(queryString, sb, 255, IntPtr.Zero);
                return int.Parse(sb.ToString());
            }
        }
        #endregion


    }
}
