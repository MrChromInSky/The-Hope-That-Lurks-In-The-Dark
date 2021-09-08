using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpeedMeter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI speedText;

    Player_Movement_Default pmd;

    private void Awake()
    {
        pmd = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Movement_Default>();
    }

    private void Update()
    {
        speedText.SetText("Speed: " + pmd.DEBUG_Player_Speed.ToString("00.0"));
    }
}
