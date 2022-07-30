using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnergyController : MonoBehaviour
{
    private float time = 120f;
    [SerializeField] private bool IsPlayerNearby = false;
    public bool IsEnergyOn = false;
    [SerializeField] private TextMeshPro EnergyAlertText;
    [SerializeField] private TextMeshProUGUI TurnOnEnergyText;
    [SerializeField] private Material mat1;
    [SerializeField] private Material mat2;
    [SerializeField] private GameObject obj;
    [SerializeField] private Light EngergyLight;

    // Start is called before the first frame update
    void Start()
    {
        EnergyAlertText.text = "";
        TurnOnEnergyText.text = "";
        obj.GetComponent<MeshRenderer>().material = mat1;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayerNearby)
        {
            TurnOnEnergyText.text = "Turn on [F]";
            if(Input.GetKeyUp(KeyCode.F))
            {
                IsEnergyOn = true;
                time = 120f;
            }
        }
        else
        {
            TurnOnEnergyText.text = "";
        }
        if(IsEnergyOn)
        {
            time -= Time.deltaTime;
            EnergyAlertText.text = Mathf.Round(time).ToString();

            obj.GetComponent<MeshRenderer>().material = mat2;
            EngergyLight.enabled = true;
        }
        else{
            obj.GetComponent<MeshRenderer>().material = mat1;
            EngergyLight.enabled = false;
        }
        if(time <= 0)
        {
            EnergyAlertText.text = "";
            IsEnergyOn = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Torch"){
            IsPlayerNearby = true;
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Torch"){
            IsPlayerNearby = false;
        }
    }
}