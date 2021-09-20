using System.Collections;
using System.Linq;
using OpenXRRuntimeJsons;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Transform buttonsParent;
    [SerializeField] private StartXRButton buttonPrefab;
    [SerializeField] private Text message;

    private void Start()
    {
        message.HideErrorMessage();
        var runtimes = OpenXRRuntimeJson.GetRuntimeJsonPaths();
        foreach (var (runtimeType, jsonPath) in runtimes.Select(d => (d.Key, d.Value)))
        {
            void LoadXR()
            {
                StartCoroutine(LoadXRCoroutine(runtimeType));
            }

            var buttonObj = Instantiate(buttonPrefab, buttonsParent.transform, false);
            buttonObj.SetLabel(runtimeType.ToString());
            buttonObj.AddListener(LoadXR);
            Debug.Log($"Runtime Type: {runtimes}, JSON Path: {jsonPath}");
        }
    }

    private IEnumerator LoadXRCoroutine(OpenXRRuntimeType runtimeType)
    {
        OpenXRRuntimeJson.SetRuntimeJsonPath(runtimeType);
        var routine = ManualXRControl.StartXRCoroutine();
        yield return StartCoroutine(routine);
        if (routine.Current is bool ret && ret)
        {
            SceneManager.LoadScene("VR");
        }
        else
        {
            message.ShowErrorMessage("Initializing XR Failed");
            yield return new WaitForSeconds(3f);
            message.HideErrorMessage();
        }
    }
}

static class TextExtensions
{
    public static void ShowErrorMessage(this Text text, string message)
    {
        text.raycastTarget = true;
        text.text = message;
    }

    public static void HideErrorMessage(this Text text)
    {
        text.raycastTarget = false;
        text.text = string.Empty;
    }
}