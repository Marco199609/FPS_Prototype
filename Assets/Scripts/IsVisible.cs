using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

public class IsVisible : MonoBehaviour
{
    bool isVisible = true;
    Vector3 direction;
    int maxRange = 200;
    MonoBehaviour[] scripts;


    void Start()
    {
        scripts = gameObject.GetComponents<MonoBehaviour>();        //Gets scripts from gameobject
    }

    void Update()
    {
        StartCoroutine(ActivateOrDeactivateScripts(2f));        //Update coroutine every x seconds
    }



    IEnumerator ActivateOrDeactivateScripts(float waitTime)
    {
        direction = gameObject.transform.position - Camera.main.transform.position;

        if (direction.sqrMagnitude < maxRange * maxRange)
        {
            isVisible = true;
        }
        else
            isVisible = false;

        foreach (MonoBehaviour script in scripts)                   //enables or disables scripts depending on the distance from player
        {
            if (!isVisible && script != this)
                script.enabled = false;
            else
                script.enabled = true;
        }
        yield return new WaitForSeconds(waitTime);
    }




}
