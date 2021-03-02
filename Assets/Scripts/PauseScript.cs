using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    public GameObject menuPause;
    public Text objectifs;
    public static int amisRestants = 3;
    public GameObject miniMap;

    public void SetObjectifText()
    {
        objectifs.text = "Il reste " + amisRestants + " amis à libérer...";
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            // TODO: Ajouter un son de pause
            if(isPaused)
            {
                isPaused = false;
                menuPause.SetActive(isPaused);
                Time.timeScale = 1f;
            }
            else
            {
                SetObjectifText();
                isPaused = true;
                menuPause.SetActive(isPaused);
                Time.timeScale = 0f;
            }
        }

        if(Input.GetKeyUp(KeyCode.M))
        {
            miniMap.active = !miniMap.active;
        }
    }
}
