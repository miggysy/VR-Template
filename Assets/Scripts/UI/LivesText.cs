using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LivesText : MonoBehaviour
{
    private TextMeshProUGUI livesText;
    private void Awake() => livesText = GetComponent<TextMeshProUGUI>();
    private void SetLivesText(int lives) => livesText.text = lives.ToString();
    private void OnEnable() => LivesManager.onCurrentLivesChanged += SetLivesText;
    private void OnDisable() => LivesManager.onCurrentLivesChanged -= SetLivesText;
}
