using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private float _bpm;
    [SerializeField] private float previewedBeats;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.right * ( 60f / _bpm * previewedBeats) 
                                            * Time.deltaTime;
    }
}
