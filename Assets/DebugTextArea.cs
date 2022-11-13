using UnityEngine;
using TMPro;

public class DebugTextArea : MonoBehaviour
{
    // This is a singleton
    public static DebugTextArea Instance { get; private set; }

    private TMP_Text _text;

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        Instance = this;
    }

    public void Log(string text)
    {
        _text.SetText(text);
        Debug.Log(text);
    }
}
