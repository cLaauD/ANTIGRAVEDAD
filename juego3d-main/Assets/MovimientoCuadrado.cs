using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoCuadrado : MonoBehaviour
{
    public Transform sphereTransform;
    private Rigidbody rb;
    private bool isInSphere = false;
    public float velocidadMovimiento = 5.0f;
    public float fuerzaSalto = 10f;
    public Color newColor = Color.yellow;
    private Color colorOriginal;
    private int capsulasRecogidas = 0;
    private AudioSource sonidoCapsula;

    private bool enCamara = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        colorOriginal = GetComponent<Renderer>().material.color;
        Debug.Log("Presiona 'G' para cambiar de cámara");
        Debug.Log("RECOLECTA LAS 8 MONEDAS...");
        sonidoCapsula = GetComponent<AudioSource>();
        sonidoCapsula.playOnAwake = false;
    }

    void Update()
    {
        float movimientoHorizontal = Input.GetAxis("Vertical") * velocidadMovimiento * Time.deltaTime;
        transform.Translate(Vector3.left * movimientoHorizontal);

        float movimientoVertical = Input.GetAxis("Horizontal") * velocidadMovimiento * Time.deltaTime;
        transform.Translate(Vector3.forward * movimientoVertical);

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * fuerzaSalto, ForceMode.Impulse);
        }

        float distanceToSphere = Vector3.Distance(transform.position, sphereTransform.position);

        if (distanceToSphere < sphereTransform.localScale.x / 2)
        {
            if (!isInSphere)
            {
                isInSphere = true;
                Debug.Log("Dentro de la burbuja");
                if (!enCamara)
                {
                    StartCoroutine(ReiniciarDespuesDeTiempo(5.0f));
                    enCamara = true;
                }
            }
        }
        else
        {
            if (isInSphere)
            {
                isInSphere = false;
                Debug.Log("Fuera de la burbuja");
                enCamara = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Capsula"))
        {
            Debug.Log("Monedita");
            Destroy(collision.gameObject);
            GetComponent<Renderer>().material.color = newColor;
            StartCoroutine(RevertirColorDespuesDeTiempo());
            capsulasRecogidas++;
            Debug.Log("Cápsula recogida. Total: " + capsulasRecogidas);
            if (sonidoCapsula != null)
            {
                sonidoCapsula.Play();
            }

            if (capsulasRecogidas >= 8)
            {
                Debug.Log("AHORA ENTRA EN LA BURBUJA!!!");

            }
        }
    }

    IEnumerator RevertirColorDespuesDeTiempo()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<Renderer>().material.color = colorOriginal;
    }

    IEnumerator ReiniciarDespuesDeTiempo(float tiempoEspera)
    {
        float tiempoMostrarMensaje = 5.0f;
        yield return new WaitForSeconds(tiempoEspera);
        Debug.Log("GANASTE!!!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}