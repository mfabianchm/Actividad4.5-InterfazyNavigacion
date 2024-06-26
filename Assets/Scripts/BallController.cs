using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallControler : MonoBehaviour
{
    // RigidBody
    public Rigidbody rb;
    // Impulso bola
    public float impulseForce = 3f;

    private bool ignoreNextCollision;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {

        // Evitar multiples coliciones
        if (ignoreNextCollision)
        {
            return;
        }

        // Choco con el queso rojo
        DeathPart deathPart = collision.transform.GetComponent<DeathPart>();
        if (deathPart)
        {
            GameManager.singleton.RestartLevel();
        }

        // Evitar errores al colicionar
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * impulseForce, ForceMode.Impulse);

        // Ya coliciono
        ignoreNextCollision = true;
        // Llamar cada 2 segundos para cambiar valor de colicion
        Invoke("AllowNextCollision", 0.2f);
    }

    private void AllowNextCollision()
    {
        ignoreNextCollision = false;
    }

    public void ResetBall(){
        transform.position = startPosition;
    }
}
