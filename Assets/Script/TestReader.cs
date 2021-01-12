using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestReader : MonoBehaviour
{
    TextReader m_TextReader;
    [SerializeField] string[] m_textnames;
    string[] comStr;//コマンド文字列の配列用
    int comIndex=0;//コマンドインデックス

    // Start is called before the first frame update
    void Start()
    {
       m_TextReader = new TextReader();
       comStr = m_TextReader.ReaderText(m_textnames,m_textnames.Length);
        m_TextReader.ComAnalysis(comStr,comIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        comIndex++;
        m_TextReader.ComAnalysis(comStr, comIndex);
    }
}
