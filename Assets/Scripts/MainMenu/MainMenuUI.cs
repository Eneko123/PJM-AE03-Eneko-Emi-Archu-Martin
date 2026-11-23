using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Button playMG1;
    public Button playMG2;
    public Button playMG3;
    public Button ranking;
    public Button exit;

    public GameManager gm;

    void Start()
    {
        gm = GameManager.instance;
        playMG1.onClick.AddListener(() => gm.ChangeScene("MiniGame1Base"));
        playMG2.onClick.AddListener(() => gm.ChangeScene("MiniGame2GPS"));
        playMG3.onClick.AddListener(() => gm.ChangeScene("MiniGame3AR"));
        playMG3.onClick.AddListener(() => gm.ChangeScene("Ranking"));
        exit.onClick.AddListener(gm.Quit);
    }
}
