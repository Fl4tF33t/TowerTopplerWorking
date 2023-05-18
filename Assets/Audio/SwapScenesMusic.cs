using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwapScenes : MonoBehaviour
{
    void Update()
    {
      

        if (SceneManager.GetActiveScene().name == "Starter_Scene")
            BGmusic.instance.GetComponent<AudioSource>().Play();
        //BGmusic.instance.GetComponent<AudioSource>().Play();

    }
}