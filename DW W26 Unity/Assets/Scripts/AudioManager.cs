using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource pickup;
    public AudioSource aiming;
    public AudioSource crawl;
    public AudioSource dammage;
    public AudioSource shoot;
    public AudioSource music;
    void Start()
    {
        
    }

    public void StopPlay()
    {
        music.Stop();
    }
    public void PlaySound(string sound)
    {
        Debug.Log(sound);
        switch (sound)
        {
            case "ammoPickup": //done?
                pickup.Play();
                break;

            case "aim": // done
                if (!aiming.isPlaying)
                {
                    aiming.Play();
                }
                break;

            case "crawl": // done
                if (!crawl.isPlaying)
                {
                    crawl.Play();
                }
                break;

            case "dam": //done ?
                dammage.Play();
                break;

            case "shoot": //done
                shoot.Play();
                break;

            case "music": //done
                if (!music.isPlaying)
                {
                    music.Play();
                }
                break;
            default:

                break;

        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
