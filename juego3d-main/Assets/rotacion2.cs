using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotacion2 : MonoBehaviour
{
    public float velocidadRotacion = 10.0f; // La velocidad de rotación

    void Update()
    {
        // Rota la cápsula sobre el eje Y
        transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
    }
}