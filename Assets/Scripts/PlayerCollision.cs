using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour
{
    public GameObject pickupEffect;
    public GameObject mobEffect;
    public GameObject loot;
    bool canInstantiate = true;
    bool isInvincible = false;
    public GameObject cam1;
    public GameObject cam2;
    public GameObject cam3;
    public AudioClip hitSound;
    AudioSource audioSource;
    public SkinnedMeshRenderer rend;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "coin") // on a touché une pièce
        {
            audioSource.PlayOneShot(hitSound);
            GameObject go = Instantiate(pickupEffect, other.transform.position, Quaternion.identity);
            Destroy(go, 0.5f);
            PlayerInfos.pi.GetCoin();
            Destroy(other.gameObject);
        }

        if(other.gameObject.name == "Fin")
        { // On a touché la fin du niveau ...
            print("Score final = " + PlayerInfos.pi.GetScore());

        }

        // Gestion des caméras
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(true);
        }
        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(true);
        }
        if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cam1")
        {
            cam1.SetActive(false);
        }
        if (other.gameObject.tag == "cam2")
        {
            cam2.SetActive(false);
        }
        if (other.gameObject.tag == "cam3")
        {
            cam3.SetActive(false);
        }
    }

    // private void OnCollisionEnter(Collision collision)
    private void OnControllerColliderHit(ControllerColliderHit collision)
    {
        // Si le monstre me touche
        if (collision.gameObject.tag == "hurt" && !isInvincible)
        { 
            isInvincible = true;
            PlayerInfos.pi.SetHealth(-1);
            iTween.PunchPosition(gameObject, Vector3.back * 2, .5f);
            // iTween.PunchScale(gameObject, new Vector3(1.2f, 1.2f, 1.2f), .3f);
            StartCoroutine("ResetInvincible");
        }

        // Si je saute sur le monstre
        if (collision.gameObject.tag == "mob" && canInstantiate)
        {
            collision.gameObject.transform.parent.gameObject.GetComponent<Collider>().enabled = false;
            canInstantiate = false;
            audioSource.PlayOneShot(hitSound);
            iTween.PunchScale(collision.gameObject.transform.parent.gameObject, new Vector3(50,50,50), .6f);
            // Je saute sur le monstre
            GameObject go = Instantiate(mobEffect, collision.transform.position, Quaternion.identity);
            Instantiate(loot, collision.transform.position + Vector3.forward, Quaternion.identity * Quaternion.Euler(90,0,0));
            Destroy(go, 0.6f);
            Destroy(collision.gameObject.transform.parent.gameObject, 0.5f);
            StartCoroutine("ResetInstantiate");
        }

        if (collision.gameObject.tag == "fall")
        {
            // TODO: Ajouter effet de particules
            // Respawn ...
            StartCoroutine("RestartScene");
        }
    }

    IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(0.8f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // On réinitialise canInstantiate après 0.8 sec
    IEnumerator ResetInstantiate()
    {
        yield return new WaitForSeconds(0.8f);
        canInstantiate = true;
    }

    IEnumerator ResetInvincible()
    {
        for(int i=0 ; i<10 ; i++)
        {
            yield return new WaitForSeconds(.2f);
            rend.enabled = !rend.enabled;
        }
        yield return new WaitForSeconds(.2f);
        rend.enabled = true;
        isInvincible = false;
    }
}
