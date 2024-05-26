using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public static ToggleController instance;
    public Toggle toggle;

    private void Start()
    {
        toggle.isOn = Tutorial.trigerTutorial;

        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });
    }
    private void Awake()
    {
        instance = this;
    }

    public void ToggleValueChanged()
    {
        Tutorial.trigerTutorial = toggle.isOn;
    }
}
