using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimator : MonoBehaviour
{
    public Image stator;
    public Image rotor;
    public float colorSpeed;
    public float rotorSpeed;
    [Range(0, 1)]
    public float saturation;
    [Range(0, 1)]
    public float value;
    [Range(-1, 1)]
    public float rotorSaturationMod;

    private float hue;

    void Start()
    {
        hue = Random.Range(0f, 1f);
    }

    void Update()
    {
        hue += Time.deltaTime * colorSpeed;
        if (hue > 1)
            hue = 0;
        else if (hue < 0)
            hue = 1;

        rotor.rectTransform.eulerAngles += new Vector3(0, 0, Time.deltaTime * rotorSpeed);

        stator.color = Color.HSVToRGB(hue, saturation, value);
        rotor.color = Color.HSVToRGB(hue, saturation + rotorSaturationMod, value);
    }
}