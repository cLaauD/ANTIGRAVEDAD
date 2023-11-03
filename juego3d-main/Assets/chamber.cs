using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  chamber : MonoBehaviour
{
    public GameObject jugador;
    private bool jugadorEnELCubo = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == jugador)
        {
            Rigidbody jugadorRB = jugador.GetComponent<Rigidbody>();
            jugadorRB.useGravity = false;
            jugadorEnELCubo = true;
            Debug.Log("GANASTE!!!!!");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == jugador)
        {
            Rigidbody jugadorRB = jugador.GetComponent<Rigidbody>();
            jugadorRB.useGravity = true;
            jugadorEnELCubo = false;
        }
    }

    private void Update()
    {
        if (jugadorEnELCubo)
        {
            jugador.transform.position = transform.position;
        }
    }
}