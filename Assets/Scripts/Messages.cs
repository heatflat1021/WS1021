using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Messages : MonoBehaviour
{
    // ��X��ނ����������Ȃ��̂�enum��萔���X�g�ŊǗ�����Ƃ悢
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
