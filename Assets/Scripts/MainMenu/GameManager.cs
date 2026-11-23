using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    [Header("Estadisticas del minijuego 1 (2D)")]
    [Tooltip("Oleada maxima alcanzada")]
    public int oleadaMax = 0;
    public float timerMG1 = 0;

    [Header("Estadisticas del minijuego 2 (GPS)")]
    [Tooltip("Preguntas bien respondidas")]
    public int correctQuestinos = 0;
    public float timerMG2 = 0;

    [Header("Estadisticas del minijuego 3 (AR)")]
    [Tooltip("Cantidad de enemigos seguidos derrotados")]
    public int enemysFrequentlyDefeated = 0;
    public float timerMG3 = 0;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }

    private void OnApplicationPause(bool paused)
    {
        Time.timeScale = (paused) ? 0f : 1f;
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void ChangeScene(string esceneName)
    {
        SceneManager.LoadScene(esceneName);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
