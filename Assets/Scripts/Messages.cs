using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    // 後々種類が増えそうなものはenumや定数リストで管理するとよい
    private static readonly List<string> messages = new List<string>() { "a", "b"};
    
    /// <summary>
    /// get random message
    /// </summary>
    /// <returns></returns>
    public static string GetMessage()
    {
        int idx = UnityEngine.Random.Range(0, messages.Count);
        return messages[idx];
    }
}
