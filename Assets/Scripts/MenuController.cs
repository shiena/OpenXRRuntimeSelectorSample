using System.Linq;
using OpenXRRuntimeJsons;
using UnityEngine;
using UnityEngine.UI;

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
                button.onClick.AddListener(() =>
                {
                    OpenXRRuntimeJson.SetRuntimeJsonPath(value);
                });
            }
        }
    }
}