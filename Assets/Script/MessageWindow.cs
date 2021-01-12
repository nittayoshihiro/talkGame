using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageWindow : MonoBehaviour
{
    [SerializeField] GameObject m_character;
    [SerializeField] Sprite[] m_characterSprite;
    [SerializeField] Text m_messageText;
    [SerializeField] Image m_messageWindow;
    [SerializeField] float m_waitTime = 0.1f;
    [SerializeField] Text m_speedText;
    [SerializeField] Image m_fadeImage;
    TalkStatus m_talkStatus = TalkStatus.StandardSpeed;
    private bool m_isSkipRequested;
    private bool m_characterCor = true;
    string[] m_testMessage;
    int m_talkIndex = 0;
    int m_characterIndex;
    Image m_characterImage;
    Coroutine m_startCor;
    Coroutine m_charaCor;
    TalkDate m_talkDate;

    // Start is called before the first frame update
    void Start()
    {
        m_testMessage = new string[] { "私のターン！！", "draw！！", "私は大砲よ", "私の勝ちだ" };
        m_startCor = StartCoroutine(Write(m_testMessage[m_talkIndex]));
        m_speedText.text = "×１";
        m_characterImage = m_character.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClick()
    {
        Debug.Log("click");
        if (m_startCor == null)
        {
            //開発用リピーター
            if (m_talkIndex < m_testMessage.Length - 1)
            {
                m_talkIndex++;
                if (1 < m_talkIndex)
                {
                    m_characterIndex = 1;
                    CharacterChange(m_characterImage, m_characterSprite[m_characterIndex], false);
                }
            }
            else
            {
                m_talkIndex = 0;
                m_characterIndex = 0;
                CharacterChange(m_characterImage, m_characterSprite[m_characterIndex], true);
            }

            
            m_startCor = StartCoroutine(Write(m_testMessage[m_talkIndex]));
        }
        else
        {
            m_isSkipRequested = true;
            m_messageText.text = m_testMessage[m_talkIndex];
        }
    }

    void CharacterChange(Image image, Sprite sprite,bool fader)
    {
        Debug.Log(m_characterCor);
        if (m_charaCor == null)
        {
           m_charaCor =StartCoroutine(CharacterFade(image, sprite,fader));
            m_characterCor = false;
        }
    }

    IEnumerator CharacterFade(Image image, Sprite sprite,bool fader)
    {
        if (fader)
        {
            Debug.Log("characterFade");
            for (byte i = 255; 0 < i; i--)
            {
                image.color = new Color32(i, i, i, i);
                yield return null;
            }
            image.sprite = sprite;
            for (byte i = 0; i < 255; i++)
            {
                image.color = new Color32(i, i, i, i);
                yield return null;
            }
        }
        else
        {
            image.sprite = sprite;
        }
        
        m_characterCor = true;
        m_charaCor = null;
    }

    /// <summary>
    /// メッセージを表示
    /// </summary>
    /// <param name="message"></param>
    /// <returns></returns>
    IEnumerator Write(string message)
    {
        m_messageText.text = "";
        string str = message;
        for (int i = 0; i < str.Length && !m_isSkipRequested; i++)
        {
            m_messageText.text += str[i];
            yield return new WaitForSeconds(m_waitTime);
        }
        //m_messageText.text = message;
        m_isSkipRequested = false;
        m_startCor = null;
    }

    /// <summary>トークスピード</summary>
    public void SpeedClick()
    {
        Debug.Log("speedClick");
        m_talkStatus++;
        switch (m_talkStatus)
        {
            case TalkStatus.StandardSpeed:
                break;
            case TalkStatus.DoubleSpeed:
                m_speedText.text = "×２";
                m_waitTime /= 2;
                break;
            case TalkStatus.HalfSpeed:
                m_speedText.text = "×0.5";
                m_waitTime *= 4;
                break;
            case TalkStatus.Last:
                m_speedText.text = "×１";
                m_waitTime /= 2;
                m_talkStatus = TalkStatus.StandardSpeed;
                break;
        }
    }


    /// <summary>トークスピードステータス</summary>
    enum TalkStatus
    {
        StandardSpeed,
        DoubleSpeed,
        HalfSpeed,
        Last
    }

}
