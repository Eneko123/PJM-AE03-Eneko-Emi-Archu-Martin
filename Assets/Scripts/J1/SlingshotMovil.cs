
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SlingshotMovil : MonoBehaviour
{
    [Header("Proyectil")]
    public Rigidbody2D proyectil;
    public Transform puntoLanzamiento;

    [Header("Lanzamiento")]
    public float fuerza = 8f;
    public float distanciaMaxima = 2f;

    [Header("Trayectoria")]
    public int puntosTrayectoria = 30;
    public float tiempoTrayectoria = 0.08f;

    private Camera camara;
    private LineRenderer linea;
    private Vector2 posicionInicial;
    private bool arrastrando;
    private bool lanzado;

    void Start()
    {
        camara = Camera.main;
        linea = GetComponent<LineRenderer>();

        linea.positionCount = 0;
        linea.useWorldSpace = true;
        linea.startWidth = 0.04f;
        linea.endWidth = 0.01f;

        if (proyectil == null || puntoLanzamiento == null)
        {
            Debug.LogError("Asigna el proyectil y el punto de lanzamiento.");
            enabled = false;
            return;
        }

        posicionInicial = puntoLanzamiento.position;

        proyectil.position = posicionInicial;
        proyectil.bodyType = RigidbodyType2D.Kinematic;
        proyectil.gravityScale = 0f;
        proyectil.linearVelocity = Vector2.zero;
    }

    void Update()
    {
        if (lanzado || proyectil == null || camara == null)
            return;

        if (Input.touchCount == 0)
            return;

        Touch toque = Input.GetTouch(0);

        Vector3 pantalla = new Vector3(
            toque.position.x,
            toque.position.y,
            -camara.transform.position.z
        );

        Vector2 mundo = camara.ScreenToWorldPoint(pantalla);

        if (toque.phase == TouchPhase.Began)
        {
            if (Vector2.Distance(mundo, proyectil.position) < 0.7f)
                arrastrando = true;
        }

        if (arrastrando &&
            (toque.phase == TouchPhase.Moved ||
             toque.phase == TouchPhase.Stationary))
        {
            Vector2 desplazamiento = mundo - posicionInicial;

            if (desplazamiento.magnitude > distanciaMaxima)
                desplazamiento =
                    desplazamiento.normalized * distanciaMaxima;

            proyectil.position = posicionInicial + desplazamiento;

            DibujarTrayectoria();
        }

        if (arrastrando &&
            (toque.phase == TouchPhase.Ended ||
             toque.phase == TouchPhase.Canceled))
        {
            arrastrando = false;
            linea.positionCount = 0;

            Vector2 velocidad =
                (posicionInicial - proyectil.position) * fuerza;

            proyectil.bodyType = RigidbodyType2D.Dynamic;
            proyectil.gravityScale = 1f;
            proyectil.linearVelocity = velocidad;

            lanzado = true;
        }
    }

    void DibujarTrayectoria()
    {
        Vector2 velocidad =
            (posicionInicial - proyectil.position) * fuerza;

        Vector2 gravedad =
            Physics2D.gravity * proyectil.gravityScale;

        linea.positionCount = puntosTrayectoria;

        for (int i = 0; i < puntosTrayectoria; i++)
        {
            float t = i * tiempoTrayectoria;

            Vector2 punto =
                posicionInicial
                + velocidad * t
                + 0.5f * gravedad * t * t;

            linea.SetPosition(
                i, new Vector3(punto.x, punto.y, 0f)
            );
        }
    }
}
