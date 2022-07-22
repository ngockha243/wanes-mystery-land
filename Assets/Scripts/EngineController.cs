using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<ParticleSystem>().startColor = new Color(1, 0, 1);
        Debug.Log(particle);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
