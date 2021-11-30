using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlindMinigame : Minigame
{
    private List<RythmKey> Inputs = null;
    private int currentInputIndex = 0;
    private bool inputActive = false;
    public int lowestInputAmount = 4;
    public int highestInputAmount = 7;

    public TextMeshProUGUI text = null;
    public Image arrow = null;

    [SerializeField]
    private Color _defaultColor = Color.white;
    [SerializeField]
    private Color _wrongColor = Color.red;
    [SerializeField]
    private Color _rightColor = Color.green;

    // Start is called before the first frame update
    void Start()
    {
        Inputs = new List<RythmKey>();

        for (int i = 0; i < lowestInputAmount - 1; i++)
        {
            Inputs.Add(GenerateInput());
        }

        StartCoroutine(NextRound(true));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            HandleRythmKey(RythmKey.Up);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            HandleRythmKey(RythmKey.Down);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            HandleRythmKey(RythmKey.Left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            HandleRythmKey(RythmKey.Right);
        }
    }

    private void HandleRythmKey(RythmKey key)
    {
        if (!inputActive) return;

        arrow.enabled = true;
        switch (Inputs[currentInputIndex])
        {
            case RythmKey.Down:
                arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 0);
                break;
            case RythmKey.Up:
                arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 180);
                break;
            case RythmKey.Left:
                arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 270);
                break;
            case RythmKey.Right:
                arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 90);
                break;
            default:
                break;
        }

        if (key == Inputs[currentInputIndex])
        {
            currentInputIndex++;            
            arrow.color = _rightColor;
            if (currentInputIndex >= Inputs.Count)
            {                
                StartCoroutine(NextRound(true));
            }
        }
        else
        {
            arrow.color = _wrongColor;
            StartCoroutine(NextRound(false));
        }
    }

    public RythmKey GenerateInput()
    {
        return (RythmKey)Random.Range(0, 4);
    }

    IEnumerator NextRound(bool win)
    {
        inputActive = false;        

        if(win)
        {
            yield return new WaitForSeconds(1);

            if (Inputs.Count >= highestInputAmount)
            {
                performance = 1;
                ApplyGains();
            }
            else
            {
                Inputs.Add(GenerateInput());
                ShowInputs();
            }
        }
        else
        {
            yield return new WaitForSeconds(1);
            performance = (float)(Inputs.Count - lowestInputAmount)/ (float)(highestInputAmount - lowestInputAmount);
            ApplyGains();
        }
    }

    public void ShowInputs()
    {
        text.text = "Watch!";
        arrow.enabled = false;
        StartCoroutine(ShowCoroutine());
    }

    IEnumerator ShowCoroutine()
    {
        arrow.color = _defaultColor;
        for (int i = 0; i < Inputs.Count; i++)
        {
            yield return new WaitForSeconds(0.25f);
            arrow.enabled = true;
            switch (Inputs[i])
            {
                case RythmKey.Down:
                    arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 0);
                    break;
                case RythmKey.Up:
                    arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 180);
                    break;
                case RythmKey.Left:
                    arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 270);
                    break;
                case RythmKey.Right:
                    arrow.rectTransform.localEulerAngles = new Vector3(0, 0, 90);
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(0.75f);
            arrow.enabled = false;
        }
        text.text = "Repeat!";
        currentInputIndex = 0;
        inputActive = true;
    }
}
