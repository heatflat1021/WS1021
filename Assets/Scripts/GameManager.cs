using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // �v���O�������̒萔�́Aconst��readonly�Ő錾����B
    private const int SEND_MESSAGE_FRAME = 50;

    // public���Aprivate [SerializeField] ���悢
    [SerializeField]
    private GameObject serialManagerObj;
    [SerializeField]
    private GameObject uiManagerObj;

    private SerialManager serialManager;
    private UIManager uiManager;

    // ����Number�Ƃ������O�͉����w���Ă��邩������ɂ����B
    // amount��counter�ȂǁA��蕪����₷���������悢�B
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
