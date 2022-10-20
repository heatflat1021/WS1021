using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject serialManagerObj;
    public GameObject uiManagerObj;

    private SerialManager serialManager;
    private UIManager uiManager;

    private int frameNum = 0;

    // Start is called before the first frame update
    void Start()
    {
        serialManager = serialManagerObj.GetComponent<SerialManager>();
        uiManager = uiManagerObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (60 < frameNum++)
        {
            serialManager.Write("a");
            frameNum = 0;
        }

        if (serialManager.isNewProgress)
        {
            uiManager.AddProgress(serialManager.progress);
            serialManager.isNewProgress = false;
        }
    }
}
