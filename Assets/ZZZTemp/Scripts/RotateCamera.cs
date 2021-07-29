using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class RotateCamera : MonoBehaviour
{
    [SerializeField]
    Slider mySlider;
    [SerializeField]
    float val;

    private void Update()
    {
        if (val < 0)
        {
            Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, 50 * Time.deltaTime);
        }
        else if (val > 0)
        {
            Camera.main.transform.RotateAround(Vector3.zero, Vector3.up, -50 * Time.deltaTime);
        }
    }
    public void Rotate(float val)
    {
        this.val = val;
        
    }
}
