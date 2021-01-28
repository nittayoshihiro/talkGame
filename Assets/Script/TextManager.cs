using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    [SerializeField] TextReader m_textReader;
    [SerializeField] string[] m_textNames;
    private int textIndex = 0;
    private string[] com;
    // Start is called before the first frame update
    void Start()
    {
         com = m_textReader.ReaderText(m_textNames,0);
         m_textReader.ComAnalysis(com,textIndex);
    }

    public void OnClick()
    {
        if (m_textReader.CorNow)
        {
            textIndex++;    
        }
        Debug.Log($"OnClick{textIndex}");
        if (textIndex <= com.Length - 1)
        {
            m_textReader.ComAnalysis(com, textIndex);
        }

    }
}
