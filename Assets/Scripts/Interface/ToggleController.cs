using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public static ToggleController instance;
    public Toggle toggle;

    void Start()
    {
        toggle.isOn = Tutorial.trigerTutorial;

        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });
    }

    void Awake()
    {
        instance = this;
    }

    void ToggleValueChanged()
    {
        Tutorial.trigerTutorial = toggle.isOn;
    }
}
