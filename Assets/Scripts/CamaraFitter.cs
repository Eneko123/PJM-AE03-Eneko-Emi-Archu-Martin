using UnityEngine;

public class CamaraFitter : MonoBehaviour
{
    private Camera cam;
    private int lastW, lastH;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Fit()
    {
        lastW = Screen.width;
        lastH = Screen.height;

        float aspect = (float)Screen.width / Screen.height;

        cam.orthographicSize = Mathf.Max(lastW, lastH);
    }
}
