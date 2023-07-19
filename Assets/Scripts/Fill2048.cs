using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill2048 : MonoBehaviour
{
    public int value;
    [SerializeField] Text valueDisplay;
    [SerializeField] float speed;

    //Image myImage;

    bool hasCombined;
    public void FillValueUpdate(int valueIn)
    {
        value  = valueIn;
        valueDisplay.text = value.ToString();

        /*int colorIndex = GetColorIndex(value);
        Debug.Log(colorIndex + " color index");
        myImage = GetComponent<Image>();
        myImage.color = GameController2048.instance.fillColors[colorIndex];*/
    }
    /*
    int GetColorIndex(int valueIn)
    {
        int index = 0;
        while(valueIn != 1)
        {
            index++;
            valueIn /= 2;
        }
        index--;
        //Debug.Log(index);
        return index;
    }*/

    private void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, speed * Time.deltaTime);
            hasCombined = false;
        } else if(hasCombined == false)
        {
            if(transform.parent.GetChild(0) != this.transform)
            {
                Destroy(transform.parent.GetChild(0).gameObject);
            }
            hasCombined = true;
        }
    }

    public void Double()
    {
        value *= 2;
        GameController2048.instance.ScoreUpdate(value);
        valueDisplay.text = value.ToString();
        
        /*int colorIndex = GetColorIndex(value);
        Debug.Log(colorIndex + " color index");
        myImage.color = GameController2048.instance.fillColors[colorIndex];*/

    }
}
