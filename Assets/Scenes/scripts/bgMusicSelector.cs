using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgMusicSelector : MonoBehaviour
{
    [SerializeField] private AudioSource[] bgMusics;

    // Start is called before the first frame update
    void Start()
    {
        var c = bgMusics.Length;

        var m = bgMusics[Random.Range(0, c)];

        m.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
