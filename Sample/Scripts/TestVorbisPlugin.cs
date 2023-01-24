using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using OggVorbis;
using System.IO;

namespace EventLab.Dependencies.Vorbis.Samples
{

    public class TestVorbisPlugin : MonoBehaviour
    {

        #region PUBLIC ATTRIBUTES

        public Button _buttonSave = null;
        public Button _buttonLoad = null;

        public AudioClip _clipSource = null;

        public string _filePath = "TestVorbisPlugin.ogg";

        public AudioSource _audioSourceLoad = null;

        #endregion

        #region CREATION AND DESTRUCTION

        protected virtual void OnEnable()
        {
            _buttonSave.onClick.AddListener(TestSave);
            _buttonLoad.onClick.AddListener(TestLoad);
        }

        protected virtual void OnDisable()
        {
            _buttonSave.onClick.RemoveListener(TestSave);
            _buttonLoad.onClick.RemoveListener(TestLoad);
        }

        protected virtual void TestSave()
        {
            VorbisPlugin.Save(_filePath, _clipSource, 0.4f);
        }

        protected virtual void TestLoad()
        {
            if (File.Exists(_filePath))
            {
                AudioClip aClip = VorbisPlugin.Load(_filePath);
                _audioSourceLoad.clip = aClip;
                _audioSourceLoad.Play();
            }
        }

        #endregion

    }

}


