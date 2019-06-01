using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartLevel : MonoBehaviour
{
    public void Restart()
    {
        Debug.Log("Should load");
        SceneManager.LoadScene(0);
             
        
       
    }
}
