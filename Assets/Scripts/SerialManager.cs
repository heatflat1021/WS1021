using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class SerialManager : SingletonMonoBehaviour<SerialManager>
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
    public float progress = 0;
    public bool isNewProgress = false;
    

    override protected void Awake()
    {
        base.Awake();

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
            Debug.LogWarning("デバッグモードに切り替えます");
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
            // 前回のnextSendMessageに応じて適当な値を返してあげる
            if (nextSendMessage.Equals("a"))
            {
                Debug.Log("x");
                progress = 0.2f;
                isNewProgress = true;
                nextSendMessage = "";
            }
            else if (nextSendMessage.Equals("b"))
            {
                progress = 0.8f;
                isNewProgress = true;
                nextSendMessage = "";
            }
        }

        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen)
        {
            if (isDebugMode)
            {
                // 前回のnextSendMessageに応じて適当な値を返してあげる
                if (nextSendMessage.Equals("a"))
                {
                    progress = 0.2f;
                    isNewProgress = true;
                }
                else if(nextSendMessage.Equals("b"))
                {
                    progress = 0.8f;
                    isNewProgress = true;
                }
            }
            else
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
