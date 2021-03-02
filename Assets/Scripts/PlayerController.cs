using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController cc;
    // Variables pour le déplacement
    public float moveSpeed;
    public float jumpForce;
    public float gravity;
    // Vecteur direction souhaitée
    public Vector3 moveDir;
    private Animator anim;
    bool isWalking = false;

    private void Start()
    {
        // On renseigne les variables en récupérant les components 
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Calcul de la direction
        moveDir = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDir.y, Input.GetAxis("Vertical") * moveSpeed);
        
        // Check de la touche espace && est au sol
        if(Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            // On saute
            moveDir.y = jumpForce;
        }
        
        // On applique la gravité
        moveDir.y -= gravity * Time.deltaTime;

        // Si on se déplace (si mouvement != 0)
        if(moveDir.x != 0 || moveDir.z != 0)
        {
            isWalking = true; // Le perso marche
            // On tourne le personnage dans la bonne dir
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(new Vector3(moveDir.x, 0, moveDir.z)), 0.15f);
        }
        else
        {
            isWalking = false; // On s'arrête de marcher
        }

        // New Bool = IsWalking
        anim.SetBool("New Bool", isWalking); // On indique à l'animator si on marche

        // On applique le déplacement
        cc.Move(moveDir * Time.deltaTime);
    }
}
