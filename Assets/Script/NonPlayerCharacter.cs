using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    private float DISPLAY_TIME = 3.0f;
    private float displayTime;
    public GameObject dialogue;
    // Start is called before the first frame update
    void Start()
    {
        dialogue.SetActive(false);
        displayTime = -1f;
    }

    // Update is called once per frame
    void Update()
    {
        if(displayTime >= 0){
            displayTime -= Time.deltaTime;
            if(displayTime < 0){
                dialogue.SetActive(false);
            }
        }
    }

    public void ShowDialogue(){
        displayTime = DISPLAY_TIME;
        dialogue.SetActive(true);
    }
}
