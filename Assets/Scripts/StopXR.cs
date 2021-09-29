using System.Collections;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class StopXR : MonoBehaviour
{
    private IEnumerator Start()
    {
        var routine = ManualXRControl.StartXRCoroutine();
        yield return StartCoroutine(routine);
        if (!(routine.Current is bool ret && ret))
        {
            Debug.LogError("Initializing XR Failed");
        }
    }

    private void OnDestroy()
    {
        ManualXRControl.StopXR();
    }
}