using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Espera : MonoBehaviour
{
    public Transform cuadrado; // Reference to the cuadrado's transform
    public float velocidad = 10f;
    private bool detenerse = false; // Variable to control whether the object should stop

    void Update()
    {
        if (!detenerse && cuadrado != null) // Check if cuadrado still exists and shouldn't stop
        {
            Vector3 direction = cuadrado.position - transform.position;
            float distance = direction.magnitude;

            if (distance > 0.1f) // Adjust the threshold value as needed
            {
                direction.Normalize();
                transform.rotation = Quaternion.LookRotation(direction);
                transform.Translate(direction * velocidad * Time.deltaTime, Space.World);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("¡Perdiste! Reiniciando juego..."); // Message in the console
            Destroy(collision.gameObject); // Destroy the cuadrado upon collision
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("chamber"))
        {
            detenerse = true; // The object should stop when colliding with "chamber"
        }
    }
}
