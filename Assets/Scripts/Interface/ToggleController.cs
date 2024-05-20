using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public static ToggleController instance;
    public Toggle toggle;
    //public bool gameBool;

    private void Start()
    {
        // Устанавливаем начальное значение переключателя на основе булевой переменной
        toggle.isOn = Tutorial.trigerTutorial;

        // Привязываем переключатель к методу, который будет обновлять булевую переменную
        toggle.onValueChanged.AddListener(delegate { ToggleValueChanged(); });
    }
    private void Awake()
    {
        instance = this;
    }


    // Метод, который будет вызван при изменении состояния переключателя
    public void ToggleValueChanged()
    {
        Tutorial.trigerTutorial = toggle.isOn;

        //Debug.Log("Булевая переменная изменилась на: " + Tutorial.trigerTutorial);
    }

    //public void OffToggle()
    //{
    //    toggle.isOn = false;
    //}
}
