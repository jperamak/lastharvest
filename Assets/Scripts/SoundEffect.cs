using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundEffect : MonoBehaviour
    {
        [SerializeField]
        private List<AudioSource> _audioClips = new List<AudioSource> {null}; //initial size 1
        public IEnumerable<AudioSource> AudioClips { get { return _audioClips; } }

        public float minPitch = 1f;
        public float maxPitch = 1f;

        public float minVolume = 1f;
        public float maxVolume = 1f;

        public ClipCyclingMode Mode = ClipCyclingMode.None;

        private int _currentClip;


        public void PlayEffect()
        {
            switch (Mode)
            {
                case ClipCyclingMode.None:
                    PlayEffect(_audioClips.First());
                    break;
                case ClipCyclingMode.InOrder:
                    if (_currentClip >= _audioClips.Count)
                        _currentClip = 0;
                    PlayEffect(_audioClips[_currentClip]);
                    _currentClip++;
                    break;
                case ClipCyclingMode.Random:
                    PlayEffect(_audioClips[Random.Range(0, _audioClips.Count - 1)]);
                    break;
            }
        }

        private void PlayEffect(AudioSource effect)
        {
            effect.Do(c => c.volume = Random.Range(minVolume, maxVolume))
                .Do(c => c.pitch = Random.Range(minPitch, maxPitch))
                .Do(c => c.Play());
        }
    }

    public enum ClipCyclingMode
    {
        None,
        InOrder,
        Random
    }

    public class ReadOnlyAttribute : PropertyAttribute
    {

    }
}
