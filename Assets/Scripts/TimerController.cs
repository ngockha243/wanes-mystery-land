using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerController : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    private EnergyController energyController;
    [SerializeField] private TextMeshProUGUI timer;
    void Start()
    {
        energyController = obj.GetComponent<EnergyController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(energyController.time < 1)
        {
            timer.text = "";
        }
        else
        {
            timer.text = Mathf.Round(energyController.time).ToString();
        }
    }
}
