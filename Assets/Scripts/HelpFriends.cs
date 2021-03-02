using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpFriends : MonoBehaviour
{
    GameObject cage;
    public Text infoTxt;
    bool canOpen = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "cage")
        {
            cage = other.gameObject;
            infoTxt.text = "Appuyez sur E pour ouvrir la cage...";
            canOpen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cage")
        {
            cage = null;
            infoTxt.text = "";
            canOpen = false;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canOpen) 
        {
            PauseScript.amisRestants--;
            iTween.ShakeScale(cage, new Vector3(145, 145, 145), 1f);
            try
            {
                cage.transform.GetChild(0).gameObject.GetComponent<Canvas>().enabled = true;
            } catch(Exception e)
            {
                print(e.Message);
            }
            
            Destroy(cage.GetComponent<MeshRenderer>(), 1.2f);
            Destroy(cage.GetComponent<BoxCollider>(), 1f);
            canOpen = false;
        }
    }
}
