using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private Text _fpsText;
    [SerializeField] private float _hudRefreshRate = 1f;
    [SerializeField] int targetFramerate = 0;
    [SerializeField] GameObject[] levels;
    [SerializeField] Elevator elevator;
    private float _timer;


    void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFramerate;
        Physics.gravity = new Vector3(0, -29.43f, 0);
    }

    private void Update()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            _fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }


        ElevatorControl();
        LoadLevels();
    }



    void LoadLevels()
    {

        for(int i = 0; i < levels.Length; i++)
        {
            if (elevator.levelActive[i] == true)
                levels[i].SetActive(true);
            else
                levels[i].SetActive(false);
        }
    }

    void ElevatorControl()
    {

    }
}
