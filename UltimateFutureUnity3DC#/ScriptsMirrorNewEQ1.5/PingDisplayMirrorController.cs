using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PingDisplayMirrorController : NetworkPingDisplay
{
    [Header("Show Ping Bar")]
    [SerializeField] private bool showPingBar = true;
    [SerializeField] private float timeDelayCheckPingSec = 1f;

    [Header("Object Ping Bar")]
    [SerializeField] private List<Image> bar = new List<Image>();
    [SerializeField] private Text pingDisplay = null;

    [Header("Color Object Ping Bar")]
    [SerializeField] private Color red= new Color(0, 0, 0, 0);
    [SerializeField] private Color green= new Color(0, 0, 0, 0);
    [SerializeField] private Color redOrange= new Color(0, 0, 0, 0);
    [SerializeField] private Color black= new Color(0, 0, 0, 0);

    private void Start()
    {
        if (!showPingBar) { return; }
        StartCoroutine(CheckPingDelay());
    }


    private IEnumerator CheckPingDelay()
    {
        ExeciuteCalculationLatence();
        yield return new WaitForSeconds(timeDelayCheckPingSec);
        ExeciuteCalculationLatence();
        StartCoroutine(CheckPingDelay());
    }
    private void ExeciuteCalculationLatence()
    {
        pingDisplay.text = actualPingFormat;
        ChangeColor();
    }
    private void ChangeColor()
    {
        if (actualPing <= 70)
        {
            //4
            bar[0].color = SelectColor("green");
            bar[1].color = SelectColor("green");
            bar[2].color = SelectColor("green");
            bar[3].color = SelectColor("green");
        }
        else if (actualPing <= 100)
        {
            //3
            bar[0].color = SelectColor("green");
            bar[1].color = SelectColor("green");
            bar[2].color = SelectColor("green");
            bar[3].color = SelectColor("black");
        }
        else if (actualPing <= 200)
        {
            //2
            bar[0].color = SelectColor("red");
            bar[1].color = SelectColor("red-orange");
            bar[2].color = SelectColor("black");
            bar[3].color = SelectColor("black");
        }
        else if (actualPing <= 300)
        {
            //1
            bar[0].color = SelectColor("red");
            bar[1].color = SelectColor("black");
            bar[2].color = SelectColor("black");
            bar[3].color = SelectColor("black");
        }
        else
        {
            //1
            bar[0].color = SelectColor("red");
            bar[1].color = SelectColor("black");
            bar[2].color = SelectColor("black");
            bar[3].color = SelectColor("black");
        }

    }
    private Color SelectColor(string color)
    {
        if (color == "red") { return red; }
        else if (color == "green") { return green; }
        else if (color == "red-orange") { return redOrange; }
        else if (color == "black") { return black; }
        return green;
    }
}
