using System.Collections;
using UnityEngine;

public class AudioPlayer: MonoBehaviour
{
    public static AudioPlayer Play(Vector3 position, AudioClip clip)
    {
        //Spawn an audio player and get al the values from it.
        Transform spawnedAudioPlayerTransform = Instantiate(SoundManager.i.audioSourcePrefab, position, Quaternion.identity).transform;
        AudioPlayer audioPlayer = spawnedAudioPlayerTransform.transform.GetComponent<AudioPlayer>();

        //Play a clip which is sended from user
        audioPlayer.Play(clip);
        return audioPlayer;
    }

    [SerializeField] private AudioSource audioSource;
    private WaitForSeconds timer = new WaitForSeconds(1);
    
    public void Play(AudioClip clip)
    {
        //Play the clip
        audioSource.PlayOneShot(clip, Random.Range(0.5f ,1));
        //Call the timer
        StartCoroutine(DestroyTimer());
    }

    private IEnumerator DestroyTimer()
    {
        yield return timer;
        //destroy after this time
        Destroy(gameObject);
    }
}
