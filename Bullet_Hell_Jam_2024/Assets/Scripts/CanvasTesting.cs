using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTesting : MonoBehaviour
{
    [SerializeField] CanvasScript canvas;

    [ContextMenu("Score 1000")]
    void ChangeScore1000() { canvas.UpdateScore(1000); }
    [ContextMenu("Score 10")]
    void ChangeScore10() { canvas.UpdateScore(10); }
    [ContextMenu("Time 60")]
    void SetTime1Min() { canvas.UpdateTime(60); }
    [ContextMenu("Time 600")]
    void SetTime10Min() { canvas.UpdateTime(600); }
    [ContextMenu("Time 1")]
    void SetTime1Sec() { canvas.UpdateTime(1); }
    [ContextMenu("Time 59")]
    void SetTime59Sec() { canvas.UpdateTime(59); }
    [ContextMenu("Time 0")]
    void SetTime0Sec() { canvas.UpdateTime(0); }
    [ContextMenu("Time 110.52134")]
    void SetTime110DecimalSec() { canvas.UpdateTime(110.52134f); }
    [ContextMenu("Life 3")]
    void UpdateLife3() { canvas.UpdateLife(3); }
    [ContextMenu("Life 1")]
    void UpdateLife1() { canvas.UpdateLife(1); }
    [ContextMenu("Life 0")]
    void UpdateLife0() { canvas.UpdateLife(0); }
    [ContextMenu("Weapon 1")]
    void UpdateWeapon1Gauge() { canvas.UpdateWeaponGauge(0, 0.1f, 0.2f, 0.3f); }
    [ContextMenu("Weapon 2")]
    void UpdateWeapon2Gauge() { canvas.UpdateWeaponGauge(1, 0.01f, 0.75f, 0.98f); }
    [ContextMenu("Weapon 3")]
    void UpdateWeapon3Gauge() { canvas.UpdateWeaponGauge(2, 0.5f, 0, 1); }
}
