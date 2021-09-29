using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

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

    private bool OnPress(XRNode node)
    {
        var device = InputDevices.GetDeviceAtXRNode(node);
        if (device.isValid
            && device.TryGetFeatureValue(new InputFeatureUsage<bool>("TriggerButton"), out var isPressed)
            && isPressed)
        {
            SceneManager.LoadScene("Scenes/Menu");
            return true;
        }

        return false;
    }

    private void Update()
    {
        if (!OnPress(XRNode.LeftHand))
        {
            OnPress(XRNode.RightHand);
        }
    }
    private void OnDestroy()
    {
        ManualXRControl.StopXR();
    }
}