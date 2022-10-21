using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // プログラム中の定数は、constかreadonlyで宣言する。
    private const int SEND_MESSAGE_FRAME = 50;

    // publicより、private [SerializeField] がよい
    [SerializeField]
    private GameObject serialManagerObj;
    [SerializeField]
    private GameObject uiManagerObj;

    private SerialManager serialManager;
    private UIManager uiManager;

    // ○○Numberという名前は何を指しているか分かりにくい。
    // amountやcounterなど、より分かりやすい命名がよい。
    private int frameCounter = 0;

    // Start is called before the first frame update
    void Start()
    {
        serialManager = serialManagerObj.GetComponent<SerialManager>();
        uiManager = uiManagerObj.GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SEND_MESSAGE_FRAME < frameCounter++)
        {
            string message = Messages.GetMessage();
            serialManager.Write(message);
            frameCounter = 0;
        }

        if (serialManager.IsNewProgress)
        {
            uiManager.AddProgress(serialManager.Progress);
        }
    }
}
