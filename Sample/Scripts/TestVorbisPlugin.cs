using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using OggVorbis;

namespace EventLab.Dependencies.Vorbis.Samples
{

    public class TestVorbisPlugin : MonoBehaviour
    {

        #region PUBLIC ATTRIBUTES

        public Button _buttonSave = null;
        public Button _buttonLoad = null;

        public AudioClip _clipSource = null;

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
            VorbisPlugin.Save("TestVorbisPlugin.ogg", _clipSource, 0.4f);
        }

        protected virtual void TestLoad()
        {

        }

        #endregion

    }

}


