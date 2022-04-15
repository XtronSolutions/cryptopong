using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DebugViewController : MonoBehaviour
{
    [SerializeField] private GameObject ViewObject;
    [SerializeField] private Slider IntelligenceSlider;

    [Space(10)]
    [SerializeField] private Slider ImpactSlider;
    [SerializeField] private TMP_Text ImpactCurrentText;

    // Start is called before the first frame update
    void Start()
    {
        ImpactSlider.onValueChanged.AddListener(OnImpactMultiplierValueChanged);
        IntelligenceSlider.onValueChanged.AddListener(OnIntelligenceValueChanged);

        Events.DoChangeImpactMultiplier(ImpactSlider.value);
        Events.DoChangeIntelligence(Mathf.RoundToInt(IntelligenceSlider.value));
    }

    public void ActivateView() => ViewObject.SetActive(!ViewObject.activeSelf);

    private void OnImpactMultiplierValueChanged(float value)
    {
        ImpactCurrentText.text = value.ToString("0.00");
        Events.DoChangeImpactMultiplier(value);
    }
    private void OnIntelligenceValueChanged(float value) => Events.DoChangeIntelligence(Mathf.RoundToInt(value));
}
