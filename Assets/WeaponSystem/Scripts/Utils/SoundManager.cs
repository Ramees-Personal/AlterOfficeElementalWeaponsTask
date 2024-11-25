using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

namespace WeaponSystem.Utils
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance;

        private ObjectPool<AudioSource> _soundPool;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _soundPool =
                new ObjectPool<AudioSource>(createFunc: () =>
                    {
                        var audioSource = new GameObject(name: "AudioSource_" + (transform.childCount + 1))
                            .AddComponent<AudioSource>();
                        audioSource.transform.SetParent(transform);
                        return audioSource;
                    }, source => { source.gameObject.SetActive(true); },
                    source =>
                    {
                        if (source.isPlaying) source.Stop();
                        source.gameObject.SetActive(false);
                    },
                    source => { Destroy(source.gameObject); });
        }

        public AudioSource PlaySound(AudioClip clip, float volume = 1f, float pitch = 1f, bool loop = false)
        {
            var source = _soundPool.Get();
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.Play();
            if (!loop)
            {
                StartCoroutine(ReleaseAfterPlay(source, clip.length));
            }

            return source;
        }

        private IEnumerator ReleaseAfterPlay(AudioSource source, float clipLength)
        {
            yield return new WaitForSeconds(clipLength);
            Release(source);
        }

        public void Release(AudioSource source)
        {
            _soundPool.Release(source);
        }
    }
}