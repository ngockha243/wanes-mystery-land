using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private EnergyController controler;
    [SerializeField] private GameObject obj;
    [SerializeField] private GameObject[] portal;
    // Start is called before the first frame update
    void Start()
    {
        controler = obj.GetComponent<EnergyController>();
        for(int i = 0; i < portal.Length; i++)
        {
            portal[i].SetActive(false);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(controler.IsEnergyOn)
        {
            for(int i = 0; i < portal.Length; i++)
            {
                portal[i].SetActive(true);
            }
        }
        else
        {
            for(int i = 0; i < portal.Length; i++)
            {
                portal[i].SetActive(false);
            }
        }
    }
}
