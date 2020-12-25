using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextReader : MonoBehaviour
{
    /// <summary>読み込むテキスト名</summary>
    [SerializeField] string[] m_texts;
    /// <summary>バックグラウンドImage</summary>
    [SerializeField] Image m_imageBackground;
    /// <summary>バックグラウンド</summary>
    [SerializeField] Sprite[] m_spritesBackground;
    /// <summary>キャラクターImage</summary>
    [SerializeField] Image[] m_imageCharacter;
    /// <summary>キャラクター</summary>
    [SerializeField] Sprite[] m_spritesCharacter;
    /// <summary>メッセージテキスト</summary>
    [SerializeField] Text m_messageText;
    /// <summary>トークスピード</summary>
    [SerializeField] float m_talkSpeed;
    //現在のコルーチン
    Coroutine m_nowCor;
    /// <summary>コルーチンストップ</summary>
    bool m_skip = false;
    
    /// <summary>
    /// テキストを読み取り
    /// </summary>
    /// <param name="texts">テキスト名の配列</param>
    /// <param name="indexNum">テキスト名配列のindex</param>
    public string[] ReaderText(string[] texts, int indexNum)
    {
        TextAsset textAsset = Resources.Load(texts[indexNum]) as TextAsset;
        string[] del = {"\n"};
        string[] comStr = textAsset.text.Split(del,System.StringSplitOptions.None); //改行で分割
        //ここでは記号混ざりのstring[]を返す
        return comStr;
    }

    /// <summary>
    /// コマンド解析、命令
    /// </summary>
    /// <param name="comText"></param>
    public void ComAnalysis(string[] comStr,int comIndex)
    {
        string[] strArr =comStr[comIndex].Split('/');
        switch (strArr[0])
        {
            //背景変更コマンド　背景を変更
            case"back":
                //コマンド例/back/1/true/アイウエオ
                BackgroundChange(m_spritesBackground[int.Parse(strArr[1])],bool.Parse(strArr[2]));
                if (2<strArr.Length)
                {
                    TextChange(strArr[3]);
                }
                break;
            //キャラクターコマンド  キャラクターと名前を変更
            case"character":
                //コマンド例/character/1/2/ture/カキクケコ
                CharacterChange(m_spritesCharacter[int.Parse(strArr[1])],int.Parse(strArr[2]),bool.Parse(strArr[3]));
                if (2 < strArr.Length)
                {
                    TextChange(strArr[3]);
                }
                break;
            default:
                TextChange(strArr[0]);
                break;
        }
    }

    //キャラクタースプライト変更
    void CharacterChange(Sprite sprite ,int characterPos,bool fade)
    {
        if (fade)
        {
            StartCoroutine(Fader(sprite,m_imageCharacter[characterPos].color, m_imageCharacter[characterPos]));
        }
        else
        {
            m_imageCharacter[characterPos].sprite = sprite;
        }
    }

    //背景のスプライト変更
    void BackgroundChange(Sprite sprite,bool fade)
    {
        if (fade)
        {
           StartCoroutine(Fader(sprite,m_imageBackground.color,m_imageBackground));
        }
        else
        {
            m_imageBackground.sprite = sprite;
        }
    }

    //フェード
    IEnumerator Fader(Sprite sprite, Color color,Image image)
    {
        while (0 < color.a)
        {
            color.a--;
            yield return null;
        }
        image.sprite = sprite;
        while (color.a < 255)
        {
            color.a++;
            yield return null;
        }
    }

    //テキストの変更
    IEnumerator TextChange(string message)
    {
        m_messageText.text = "";
        string str = message;
        for (int i = 0; i < str.Length && !m_skip; i++)
        {
            m_messageText.text += str[i];
            yield return new WaitForSeconds(m_talkSpeed);
        }
        m_skip = false;
        m_nowCor = null;
    }

}
