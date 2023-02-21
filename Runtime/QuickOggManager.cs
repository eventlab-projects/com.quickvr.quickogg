using System.Collections;
using System.Collections.Generic;

using System.IO;
using System.Threading;

using UnityEngine;

using OggVorbis;

namespace QuickVR.QuickOgg
{

    public struct RawAudioData
    {
        public float[] _samples;
        public int _samplingRate;
        public int _numChannels;

        public RawAudioData(float[] samples, int samplingRate, int numChannels)
        {
            _samples = samples;
            _samplingRate = samplingRate;
            _numChannels = numChannels;
        }

    }

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

        /// <summary>
        /// Converts an audio raw data into an ogg data represented as a byte array. 
        /// </summary>
        /// <param name="audioData">The raw audio data</param>
        /// <param name="quality">The quality of the ogg compression</param>
        /// <returns></returns>
        public static byte[] ToOgg(RawAudioData audioData, float quality = 0.4f)
        {
            return VorbisPlugin.GetOggVorbis(audioData._samples, audioData._samplingRate, audioData._numChannels, quality);
        }

        /// <summary>
        /// Converts an audio raw data into an ogg data represented as a byte array asyncrhonously. 
        /// </summary>
        /// <param name="audioData">The raw audio data</param>
        /// <param name="quality">The quality of the ogg compression</param>
        /// <returns></returns>
        public static CustomAsyncOperation<byte[]> ToOggAsync(RawAudioData audioData, float quality = 0.4f)
        {
            CustomAsyncOperation<byte[]> op = new CustomAsyncOperation<byte[]>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToOgg(audioData, quality);
                }
            );
            thread.Start();

            return op;
        }

        /// <summary>
        /// Converts an AudioClip into a byte array, representing an ogg audio file. 
        /// </summary>
        /// <param name="aClip">The AudioClip to convert. </param>
        /// <param name="quality">The quality of the compression. </param>
        /// <returns></returns>
        public static byte[] ToOgg(AudioClip aClip, float quality = 0.4f)
        {
            return VorbisPlugin.GetOggVorbis(aClip, quality);
        }

        /// <summary>
        /// Converts an AudioClip into a byte array, representing an ogg audio file asyncrhonously. 
        /// </summary>
        /// <param name="aClip">The AudioClip to convert. </param>
        /// <param name="quality">The quality of the compression. </param>
        /// <returns></returns>
        public static CustomAsyncOperation<byte[]> ToOggAsync(AudioClip aClip, float quality = 0.4f)
        {
            CustomAsyncOperation<byte[]> op = new CustomAsyncOperation<byte[]>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToOgg(aClip, quality);
                }
            );
            thread.Start();

            return op;
        }

        /// <summary>
        /// Creates an AudioClip from a byte array in ogg format. 
        /// </summary>
        /// <param name="name">The name of the AudioClip that is going to be created. </param>
        /// <param name="ogg">A byte array representing an Audio file in ogg format. </param>
        /// <returns></returns>
        public static AudioClip ToAudioClip(string name, byte[] ogg)
        {
            return VorbisPlugin.ToAudioClip(ogg, name);
        }

        /// <summary>
        /// Creates an AudioClip from a byte array in ogg format asyncrhounously. 
        /// </summary>
        /// <param name="name">The name of the AudioClip that is going to be created. </param>
        /// <param name="ogg">A byte array representing an Audio file in ogg format. </param>
        /// <returns></returns>
        public static CustomAsyncOperation<AudioClip> ToAudioClipAsync(string name, byte[] ogg)
        {
            CustomAsyncOperation<AudioClip> op = new CustomAsyncOperation<AudioClip>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToAudioClip(name, ogg);
                }
            );
            thread.Start();

            return op;
        }

        public static RawAudioData ToRawAudioData(byte[] ogg)
        {
            VorbisPlugin.ToRawData(ogg, out float[] samples, out int samplingRate, out int numChannels);

            return new RawAudioData(samples, samplingRate, numChannels);
        }

        public static CustomAsyncOperation<RawAudioData> ToRawAudioDataAsync(byte[] ogg)
        {
            CustomAsyncOperation<RawAudioData> op = new CustomAsyncOperation<RawAudioData>();

            Thread thread = new Thread
            (
                () =>
                {
                    op._result = ToRawAudioData(ogg);
                }
            );
            thread.Start();

            return op;
        }

        #endregion

    }

}


