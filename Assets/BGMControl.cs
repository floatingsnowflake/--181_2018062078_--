using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BGMControl : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider BGMSlider;
    public AudioSource BGMSource;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BGMSource.volume = BGMSlider.value;
    }
    
}
