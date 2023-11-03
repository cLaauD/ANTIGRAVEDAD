using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion : MonoBehaviour
{
    public float velocidadRotacion = 10.0f; // La velocidad de rotación
    public ParticleSystem particulasMoneda;
    void Update()
    {
        // Rota la cápsula sobre el eje Y
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            particulasMoneda.gameObject.SetActive(true);
            particulasMoneda.transform.position = other.transform.position;
            particulasMoneda.Play();
            
        }
    }
}