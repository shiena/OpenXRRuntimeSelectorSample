using System.Collections;
using System.Linq;
using OpenXRRuntimeJsons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.XR.Management;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;

    private void Start()
    {
        var runtimes = OpenXRRuntimeJson.GetRuntimeJsonPaths();
        foreach (var (runtimeType, jsonPath) in runtimes.Select(d => (d.Key, d.Value)))
        {
            var buttonObj = Instantiate(buttonPrefab, transform, false);
            if (buttonObj.GetComponentInChildren<Text>() is var text)
            {
                text.text = runtimeType.ToString();
                text.resizeTextForBestFit = true;
            }

            if (buttonObj.GetComponentInChildren<Button>() is var button)
            {
                void LoadVR()
                {
                    OpenXRRuntimeJson.SetRuntimeJsonPath(jsonPath);
                    StartCoroutine(StartXR());
                }

                button.onClick.AddListener(LoadVR);
            }
        }
    }

    private IEnumerator StartXR()
    {
        Debug.Log("Initializing XR...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("Initializing XR Failed. Check Editor or Player log for details.");
        }
        else
        {
            Debug.Log("Starting XR...");
            XRGeneralSettings.Instance.Manager.StartSubsystems();
        }
        SceneManager.LoadScene("VR");
    }

}