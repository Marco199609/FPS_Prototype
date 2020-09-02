using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSControl : MonoBehaviour
{
    private float _hudRefreshRate = 1f;
    GameController gameController;
    private float _timer;

    private void Awake()
    {
        gameController = gameObject.GetComponent<GameController>();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = gameController.targetFramerate;
        Physics.gravity = new Vector3(0, -29.43f, 0);
    }


    private void Update()
    {
        FPS();
    }

    void FPS()
    {
        if (Time.unscaledTime > _timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            gameController._fpsText.text = "FPS: " + fps;
            _timer = Time.unscaledTime + _hudRefreshRate;
        }
    }
}
