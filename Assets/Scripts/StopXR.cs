using UnityEngine;

public class StopXR : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        ManualXRControl.StopXR();
    }
}