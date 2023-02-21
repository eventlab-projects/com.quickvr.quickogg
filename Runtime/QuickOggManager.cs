using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Threading;

using UnityEngine;

using OggVorbis;

namespace QuickVR.QuickOgg
{

    public static class QuickOggManager
    {

        #region GET AND SET

        public static void Save(string filePath, AudioClip aClip, float quality = 0.4f)
        {
            VorbisPlugin.Save(filePath, aClip, quality);
        }

        public static CustomAsyncOperation SaveAsync(string filePath, AudioClip aClip, float quality = 0.4f)
        {
            CustomAsyncOperation op = new CustomAsyncOperation();

            Thread thread = new Thread
            (
                () =>
                {
                    Save(filePath, aClip, quality);
                    op._isDone = true;
                }
            );
            thread.Start();

            return op;
        }

        public static byte[] ToByteArray(AudioClip aClip, float quality = 0.4f)
        {
            return VorbisPlugin.GetOggVorbis(aClip, quality);
        }

        public static CustomAsyncOperation<byte[]> ToByteArrayAsync(AudioClip aClip, float quality = 0.4f)
        {
            CustomAsyncOperation<byte[]> op = new CustomAsyncOperation<byte[]>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToByteArray(aClip, quality);
                }
            );
            thread.Start();

            return op;
        }

        public static AudioClip ToAudioClip(string name, byte[] data)
        {
            return VorbisPlugin.ToAudioClip(data, name);
        }

        public static CustomAsyncOperation<AudioClip> ToAudioClipAsync(string name, byte[] data)
        {
            CustomAsyncOperation<AudioClip> op = new CustomAsyncOperation<AudioClip>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToAudioClip(name, data);
                }
            );
            thread.Start();

            return op;
        }

        #endregion

    }

}


