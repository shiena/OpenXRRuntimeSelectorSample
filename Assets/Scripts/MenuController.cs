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
        message.text = "";
        var runtimes = OpenXRRuntimeJson.GetRuntimeJsonPaths();
        foreach (var (runtimeType, jsonPath) in runtimes.Select(d => (d.Key, d.Value)))
        {
            void LoadXR()
            {
                StartCoroutine(LoadXRCoroutine(jsonPath));
            }

            var buttonObj = Instantiate(buttonPrefab, buttonsParent.transform, false);
            buttonObj.SetLabel(runtimeType.ToString());
            buttonObj.AddListener(LoadXR);
        }
    }

    private IEnumerator LoadXRCoroutine(string jsonPath)
    {
        OpenXRRuntimeJson.SetRuntimeJsonPath(jsonPath);
        var routine = ManualXRControl.StartXRCoroutine();
        yield return StartCoroutine(routine);
        if (routine.Current is bool ret && ret)
        {
            SceneManager.LoadScene("VR");
        }
        else
        {
            message.text = "Initializing XR Failed";
            yield return new WaitForSeconds(3f);
            message.text = "";
        }
    }
}