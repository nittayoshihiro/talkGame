using UnityEngine;
using UnityEngine.UI;

public class TalkDate
{
    /// <summary>
    /// トークを進める
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="comment"></param>
    void TalkCange(GameObject gameObject, string comment)
    {
        Text changeText = gameObject.GetComponentInChildren<Text>();
        changeText.text = comment;
    }

    /// <summary>
    /// キャラクター変更
    /// </summary>
    /// <param name="nameObject"></param>
    /// <param name="characterName"></param>
    /// <param name="characterObject"></param>
    /// <param name="characterSprite"></param>
    void CharacterChange(GameObject nameObject,string characterName,GameObject characterObject,Sprite characterSprite)
    {
        SpriteChange(characterObject,characterSprite);
        Text nameText = nameObject.GetComponentInChildren<Text>();
        nameText.text = characterName;
    }

    /// <summary>
    /// ゲームオブジェクトのSpriteを変更する
    /// </summary>
    /// <param name="gameObject"></param>
    /// <param name="changeSprite"></param>
    void SpriteChange(GameObject gameObject, Sprite changeSprite)
    {
       Image changeImage =  gameObject.GetComponentInChildren<Image>();
        changeImage.sprite = changeSprite;
    }

}
