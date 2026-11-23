
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(LineRenderer))]
public class SlingShotMouse : MonoBehaviour
{
    [Header("Prefab y lanzamiento")]
    public Rigidbody2D prefabProyectil;
    public Transform puntoLanzamiento;
    public float fuerza = 8f;
    public float distanciaMaxima = 2f;

    [Header("Trayectoria")]
    public int puntosTrayectoria = 30;
    public float tiempoEntrePuntos = 0.08f;

    [Header("Siguiente proyectil")]
    public float tiempoParaSiguiente = 1.5f;

    private Rigidbody2D proyectilActual;
    private Camera camara;
    private LineRenderer linea;
    private Vector2 posicionInicial;
    private bool arrastrando;
    private bool lanzado;
    private bool esperandoSiguiente;

    void Start()
    {
        camara = Camera.main;
        linea = GetComponent<LineRenderer>();

        linea.useWorldSpace = true;
        linea.positionCount = 0;
        linea.startWidth = 0.04f;
        linea.endWidth = 0.02f;

        if (prefabProyectil == null || puntoLanzamiento == null)
        {
            Debug.LogError("Asigna el prefab y el punto de lanzamiento.");
            enabled = false;
            return;
        }

        posicionInicial = puntoLanzamiento.position;
        CrearProyectil();
    }

    void Update()
    {
        if (proyectilActual == null || lanzado || esperandoSiguiente)
            return;

        Vector3 mouse = camara.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mouseMundo = new Vector2(mouse.x, mouse.y);

        if (Input.GetMouseButtonDown(0))
        {
            if (Vector2.Distance(mouseMundo, proyectilActual.position) < 0.7f)
                arrastrando = true;
        }

        if (arrastrando && Input.GetMouseButton(0))
        {
            Vector2 desplazamiento = mouseMundo - posicionInicial;

            if (desplazamiento.magnitude > distanciaMaxima)
                desplazamiento = desplazamiento.normalized * distanciaMaxima;

            proyectilActual.position = posicionInicial + desplazamiento;
            DibujarTrayectoria();
        }

        if (arrastrando && Input.GetMouseButtonUp(0))
        {
            arrastrando = false;
            lanzado = true;
            linea.positionCount = 0;

            Vector2 velocidad =
                (posicionInicial - proyectilActual.position) * fuerza;

            proyectilActual.bodyType = RigidbodyType2D.Dynamic;
            proyectilActual.gravityScale = 1f;
            proyectilActual.linearVelocity = velocidad;

            StartCoroutine(CrearSiguienteProyectil());
        }
    }

    void CrearProyectil()
    {
        proyectilActual = Instantiate(
            prefabProyectil,
            posicionInicial,
            Quaternion.identity
        );

        proyectilActual.bodyType = RigidbodyType2D.Kinematic;
        proyectilActual.gravityScale = 0f;
        proyectilActual.linearVelocity = Vector2.zero;
        proyectilActual.angularVelocity = 0f;
    }

    IEnumerator CrearSiguienteProyectil()
    {
        esperandoSiguiente = true;

        yield return new WaitForSeconds(tiempoParaSiguiente);

        CrearProyectil();

        lanzado = false;
        esperandoSiguiente = false;
    }

    void DibujarTrayectoria()
    {
        Vector2 velocidad =
            (posicionInicial - proyectilActual.position) * fuerza;

        Vector2 gravedad = Physics2D.gravity;

        linea.positionCount = puntosTrayectoria;

        for (int i = 0; i < puntosTrayectoria; i++)
        {
            float t = i * tiempoEntrePuntos;

            Vector2 punto =
                proyectilActual.position
                + velocidad * t
                + 0.5f * gravedad * t * t;

            linea.SetPosition(i, new Vector3(punto.x, punto.y, 0f));
        }
    }
}
