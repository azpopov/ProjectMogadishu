using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {



    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get { return instance; }
    }

    public AudioSource efxSource;                  
    public AudioSource musicSource;
    public AudioSource efxSource2;
    public AudioClip openParch, closeParch, pickUP, shipyardSFX;

    public float lowPitchRange = .95f;              //The lowest a sound effect will be randomly pitched.
    public float highPitchRange = 1.05f;            //The highest a sound effect will be randomly pitched.
    void Awake() {
     if (instance == null) {
         instance = this;
     } else {
         Destroy(gameObject);
     }
     DontDestroyOnLoad(this.gameObject);
 }
     //Used to play single sound clips.
        public void PlaySingle(AudioClip clip)
        {
            //Set the clip of our efxSource audio source to the clip passed in as a parameter.
            efxSource.clip = clip;
            
            //Play the clip.
            efxSource.Play ();
        }
        
        
        //RandomizeSfx chooses randomly between various audio clips and slightly changes their pitch.
        public void RandomizeSfx (params AudioClip[] clips)
        {
            AudioSource _efxSource = efxSource;
            if (efxSource.isPlaying)
                _efxSource = efxSource2;
            //Generate a random number between 0 and the length of our array of clips passed in.
            int randomIndex = Random.Range(0, clips.Length);
            
            //Choose a random pitch to play back our clip at between our high and low pitch ranges.
            float randomPitch = Random.Range(lowPitchRange, highPitchRange);
            
            //Set the pitch of the audio source to the randomly chosen pitch.
            _efxSource.pitch = randomPitch;
            
            //Set the clip to the clip at our randomly chosen index.
            _efxSource.clip = clips[randomIndex];
            
            //Play the clip.
            _efxSource.Play();
        }

}
