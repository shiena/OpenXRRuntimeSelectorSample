using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartXRButton : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private Button button;

    public void SetLabel(string label)
    {
        text.text = label;
        text.resizeTextForBestFit = true;
    }

    public void AddListener(UnityAction call)
    {
        button.onClick.AddListener(call);
    }

    private void Reset()
    {
        if (GetComponentInChildren<Text>() is var t)
        {
            text = t;
        }

        if (GetComponentInChildren<Button>() is var b)
        {
            button = b;
        }
    }
}