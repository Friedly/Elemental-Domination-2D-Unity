using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class healthbar : MonoBehaviour 
{
    [readonlyAttribute]
    public health healthObject;

    private Slider _slider;

    void Start()
    {
        _slider = gameObject.GetComponent<Slider>();
        reset();
    }

	void Update () 
    {
        if (_slider)
        {
            if (healthObject != null)
                _slider.value = healthObject.currentHealth;
        }
        else
        {
            Start();
        }
	}

    public void reset()
    {
        if (_slider)
        {
            _slider.maxValue = healthObject.maximumHealth;
            _slider.minValue = 0.0f;
            _slider.value = healthObject.currentHealth;
        }
    }
}
