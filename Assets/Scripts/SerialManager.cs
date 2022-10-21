using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

// �����̃|�[�g���g�������ꍇ������̂ŃV���O���g���ł���K�v�͂Ȃ��B
// �����肷�ׂĂ�SerialManager�ŃV���A���|�[�g�̎w�肪����Ă��Ȃ������ׂ�R�[�h�̕����ق����B
// �i�����������Ƃ̂�͌Ăяo�����̋L�q��������B���߂�Ȃ����B�j
public class SerialManager : MonoBehaviour
{
    public bool isDebugMode;
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived = delegate { };

    public string portName = "COM3";// /dev/tty.usbserial-DJ00LPB1
    public int baudRate = 9600;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    private string nextSendMessage = "";
    
    // �ϐ���private�ɂ��ăA�N�Z�b�T��ݒ肷��
    private float progress = 0;
    public float Progress
    {
        get
        {
            isNewProgress = false;
            return progress;
        }
    }

    // �ϐ���private�ɂ��ăA�N�Z�b�T��ݒ肷��
    private bool isNewProgress = false;
    public bool IsNewProgress
    {
        get { return isNewProgress; }
    }
    

    void Awake()
    {
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_)
        {
            OnDataReceived(message_);
        }

        if (isDebugMode)
        {
            Read();
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        try
        {
            serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            //serialPort_.ReadTimeout = 100;
            serialPort_.NewLine = "\n";
            serialPort_.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogWarning(e);
            Debug.LogWarning("�f�o�b�O���[�h�ɐ؂�ւ��܂�");
            isDebugMode = true;
        }
    }

    public void Close()
    {
        isRunning_ = false;

        if (thread_ != null && serialPort_ != null)
        {
            serialPort_.Close();
            thread_.Join();
            //serialPort_.Dispose();
        }

        if (thread_ != null && thread_.IsAlive)
        {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen)
        {
            serialPort_.Close();
            serialPort_.Dispose();
        }
    }

    private void Read()
    {
        if (isDebugMode)
        {
            // �O���nextSendMessage�ɉ����ēK���Ȓl��Ԃ��Ă�����
            if (nextSendMessage.Equals("a"))
            {
                progress = 0.1f;
                isNewProgress = true;
                nextSendMessage = "";
            }
            else if (nextSendMessage.Equals("b"))
            {
                progress = 0.2f;
                isNewProgress = true;
                nextSendMessage = "";
            }
        }
        else
        {
            while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
            {
                try
                {
                    //if (serialPort_.BytesToRead > 0)
                    //{
                    message_ = serialPort_.ReadLine();
                    isNewMessageReceived_ = true;
                    //}
                }
                catch (System.Exception e)
                {
                    Debug.LogWarning(e.Message);
                }
            }
        }
    }

    public void Write(string message)
    {
        if (isDebugMode)
        {
            nextSendMessage = message;
        }
        else
        {
            try
            {
                serialPort_.Write(message);
            }
            catch (System.Exception e)
            {
                Debug.LogWarning(e.Message);
            }
        }
    }
}
