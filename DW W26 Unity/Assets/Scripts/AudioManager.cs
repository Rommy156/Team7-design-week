using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioSource pickup;
    public AudioSource aiming;
    public AudioSource crawl;
    public AudioSource dammage;
    public AudioSource shoot;
    void Start()
    {
        
    }

    public AudioClip ammoPickup;
    public void PlaySound(string sound)
    {
        switch (sound)
        {
            case "ammoPickup": //done
                pickup.Play();
                break;

            case "aim": //
                if (!aiming.isPlaying)
                {
                    aiming.Play();
                }
                break;

            case "crawl": //
                if (!crawl.isPlaying)
                {
                    crawl.Play();
                }
                break;

            case "dam": //done
                dammage.Play();
                break;

            case "shoot": //done
                shoot.Play();
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
