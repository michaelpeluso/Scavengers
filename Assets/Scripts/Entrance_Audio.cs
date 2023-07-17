using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Entrance_Audio : MonoBehaviour
{
    public AudioClip enter;
    public AudioClip leaving;
    private int switcher = 0;
    AudioSource sourceaudio;
    //public Animator thedude;
    //public TextMeshProUGUI text;

    public void Start()
    {
        sourceaudio = GetComponent<AudioSource>();

    }

    /*
     * Plays audio on entering and exiting
     */
    public void OnTriggerEnter(Collider other)
    {
        if (switcher == 0)
        {
            //thedude.SetBool("playWave", true);
            sourceaudio.PlayOneShot(enter, 0.7f);
            switcher = 1;
        }
        else
        {
            sourceaudio.PlayOneShot(leaving, 0.7f);
            switcher = 0;
            //thedude.SetBool("playWave", true);

        }
    }

    /*
    IEnumerator Subtext()
    {
     if (switcher == 0)
     {
         yield return new WaitForSeconds(1);
         text.text = "Welcome!";
         yield return new WaitForSeconds(3);
         text.text = "";
     }

     else
     {
         yield return new WaitForSeconds(1);
         text.text = "Goodbye!";
         yield return new WaitForSeconds(3);
         text.text = "";
     }

    }
     */
}
