using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Code Inspiration: The AudioManager singleton used in Space SHMUP
public class SoundManager : MonoBehaviour {
    //Singleton for sound management
    private static SoundManager SM = null;

    //Property to retrieve the singleton
    public static SoundManager GetSM {
        get {
            if (SM == null) SM = new SoundManager();
            return SM;
        }
    }

    [Header("Set In Inspector")]
    //Retrieve the sfx and music
    public AudioSource[] gruntPrefabs;
    public AudioSource firePrefab;
    public AudioSource musicPrefab;

    //Setup the sound manager
    void Awake() {
        if (SM == null) {
            SM = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    //Delete instances of audio upon completion of playing sound
    void Update() {
        GameObject[] deleteMe = GameObject.FindGameObjectsWithTag("SFX");
        foreach (GameObject go in deleteMe) {
            AudioSource sfx = go.GetComponent<AudioSource>();
            if (!sfx.isPlaying) Destroy(go);
        }
    }

    //Play the music and loop it
    public AudioSource PlayMusic() {
        AudioSource mc = Instantiate<AudioSource>(musicPrefab); 
        mc.loop = true;
        mc.volume = 0.6f;
        mc.Play();
        return mc;
    }

    //Play a random sound effect when an outlaw is hit
    public void PlayHitNoise() {
        //Choose one effect out of the three
        int index = Random.Range(0, gruntPrefabs.Length);

        //Give it a random pitch
        float pitch = Random.Range(0.5f, 2f);

        //Play hit noise
        AudioSource gc = Instantiate<AudioSource>(gruntPrefabs[index]);
        gc.pitch = pitch;
        gc.PlayOneShot(gc.clip, 1f);
    }

    //Play the gunfire noise
    public void PlayGunshot() {
        AudioSource fc = Instantiate<AudioSource>(firePrefab);
        fc.PlayOneShot(fc.clip, 0.6f);
    }
}
