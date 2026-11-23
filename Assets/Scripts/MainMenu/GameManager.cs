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
    public int bestOleadaMax = 0;
    public float bestTimerMG1 = 0;

    [Header("Estadisticas del minijuego 2 (GPS)")]
    [Tooltip("Preguntas bien respondidas")]
    public int correctQuestinos = 0;
    public float timerMG2 = 0;
    public int bestCorrectQuestinos = 0;
    public float bestTimerMG2 = 0;

    [Header("Estadisticas del minijuego 3 (AR)")]
    [Tooltip("Cantidad de enemigos seguidos derrotados")]
    public int enemysFrequentlyDefeated = 0;
    public float timerMG3 = 0;
    public int bestEnemysFrequentlyDefeated = 0;
    public float bestTimerMG3 = 0;

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

    public RecordResult CheckAndSaveRecords()
    {
        RecordResult result = new RecordResult();

        if (oleadaMax > bestOleadaMax) { bestOleadaMax = oleadaMax; PlayerPrefs.SetInt("Record_Oleada", oleadaMax); result.newBestOleada = true; }
        if (timerMG1 > bestTimerMG1) { bestTimerMG1 = timerMG1; PlayerPrefs.SetFloat("Record_TimerMG1", timerMG1); result.newBestTimer1 = true; }

        if (correctQuestinos > bestCorrectQuestinos) { bestCorrectQuestinos = correctQuestinos; PlayerPrefs.SetInt("Record_Questions", correctQuestinos); result.newBestQuestions = true; }
        if (timerMG2 > bestTimerMG2) { bestTimerMG2 = timerMG2; PlayerPrefs.SetFloat("Record_TimerMG2", timerMG2); result.newBestTimer2 = true; }

        if (enemysFrequentlyDefeated > bestEnemysFrequentlyDefeated) { bestEnemysFrequentlyDefeated = enemysFrequentlyDefeated; PlayerPrefs.SetInt("Record_EnemyDefeated", enemysFrequentlyDefeated); result.newBestEnemysDefeated = true; }
        if (timerMG3 > bestTimerMG3) { bestTimerMG3 = timerMG3; PlayerPrefs.SetFloat("Record_TimerMG3", timerMG3); result.newBestTimer3 = true; }

        PlayerPrefs.Save();
        return result;
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

    public struct RecordResult
    {
        public bool newBestOleada;
        public bool newBestTimer1;
        public bool newBestQuestions;
        public bool newBestTimer2;
        public bool newBestEnemysDefeated;
        public bool newBestTimer3;
    }
}
