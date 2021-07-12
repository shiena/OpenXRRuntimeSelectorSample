using UnityEngine;
using UnityEngine.XR.Management;

public class ManualXRControl : MonoBehaviour
{
    private void OnApplicationQuit()
    {
        Debug.Log("Stopping XR...");

        XRGeneralSettings.Instance.Manager.StopSubsystems();
        XRGeneralSettings.Instance.Manager.DeinitializeLoader();
        Debug.Log("XR stopped completely.");
    }
}