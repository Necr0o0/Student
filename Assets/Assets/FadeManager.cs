using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeManager : MonoBehaviour
{
    public static FadeManager singleton;

    public Image fadeImage;

    private bool isInTransition;
    private float transition;
    private bool isShowing;

    private float duration;

    void Awake()
    {
        singleton = this;
    }

   
    public void Fade(bool showing, float duration)
    {
        
        isShowing = showing;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
        isInTransition = true;

        //turns on image (render optimalization)
        fadeImage.enabled = true;
        Debug.Log("fadeStarted");
        FreezePlayer(true);
    }


    // Update is called once per frame
    void Update()
    {
        if (!isInTransition)
            return;

        //changes progress of fade
        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);


        //fades
        fadeImage.color = Color.Lerp(new Color(0, 0, 0, 0), Color.black, transition);


        //checks state of fade (Out or IN)
        if (transition > 1)
        {
            isInTransition = false;
            Fade(false, duration);
        }
        else if (transition < 0)
        {
            isInTransition = false;
            fadeImage.enabled = false;
            FreezePlayer(false);
        }
    }

        void FreezePlayer(bool freeze)
        {
            if (freeze)
            {
                //GameManager.singleton.player.GetComponent<PlayerMovement2>().rb = null;
                GameManager.singleton.player.GetComponent<PlayerMovement2>().enabled = false;
                GameManager.singleton.player.GetComponentInChildren<CameraMovement>().enabled = false;
                GameManager.singleton.player.GetComponentInChildren<CameraSelector>().enabled = false;
            
            }
            else
            {
                GameManager.singleton.player.GetComponent<PlayerMovement2>().enabled = true;
                GameManager.singleton.player.GetComponentInChildren<CameraMovement>().enabled = true;
                GameManager.singleton.player.GetComponentInChildren<CameraSelector>().enabled = true;
            }
          
        }



}
