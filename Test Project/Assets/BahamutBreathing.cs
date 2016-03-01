using UnityEngine;
using System.Collections;

public class BahamutBreathing : MonoBehaviour {

    public AudioClip footSound1;
    public AudioClip footSound2;
    public AudioClip footSound3;
    public AudioClip footSound4;
    public AudioClip footSound5;

    public float soundVolume = 0.8f;

    private int randTemp;
    private int previousTemp = 0;


    void RandomNumber()
    {
        randTemp = Random.Range(2, 5);
        if (randTemp == previousTemp)
        {
            RandomNumber();
        }
    }

    // Use this for initialization
    void Start () {
        Invoke("Breathe", 2);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void Breathe()
    {
        RandomNumber();//generates a random number for randTemp to determine a footsound, checks to make sure that the number is not repeated

        if (randTemp == 1)
        {
            GetComponent<AudioSource>().PlayOneShot(footSound1, soundVolume);
        }
        else if (randTemp == 2)
        {
            GetComponent<AudioSource>().PlayOneShot(footSound2, soundVolume);
        }
        else if (randTemp == 3)
        {
            GetComponent<AudioSource>().PlayOneShot(footSound3, soundVolume);
        }
        else if (randTemp == 4)
        {
            GetComponent<AudioSource>().PlayOneShot(footSound4, soundVolume);
        }
        else if (randTemp == 5)
        {
            GetComponent<AudioSource>().PlayOneShot(footSound5, soundVolume);
        }

        previousTemp = randTemp;
        Invoke("Breathe", randTemp);
    }
}
