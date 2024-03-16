using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SampleTextManager : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public TextMeshProUGUI npcNameText;
    public Image npcIcon;

    public string iconID;

    public Queue<string> stringQueue;

    public float typeSpeed = 0.3f;

    public GameObject textParent;

    private void Awake()
    {
        stringQueue = new Queue<string>();
    }

    private void Start()
    {
        //npcIcon.sprite = Resources.Load<Sprite>($"Album/{iconID}");     
    }

    public void StartText(SampleText[] sampleTexts)
    {
        textParent.SetActive(true);
        npcNameText.text = sampleTexts[0].npcName;          // UI의 NPC 이름에 저장한 NPC이름을 출력
        textComponent.text = sampleTexts[0].sentences[0];    // UI의 첫 번째 항목의 첫번 째 대사를 출력
        npcIcon.sprite = Resources.Load<Sprite>($"Album/{sampleTexts[0].ImageName}");

        SampleText sampleText = sampleTexts[0];             // 첫번째 데이터를 가져와서 sampleText에 저장

        foreach (string sentence in sampleText.sentences)   // sampleText에 저장된 문장들에 접근하여 각각의 sentence를 
        {
            stringQueue.Enqueue(sentence);                  // stringQueue라는 자료구조에 저장한다.
        }
    }

    public void DisplayNextSentence()
    {
        Debug.Log("다음 StringQueue 내용 넣기");

        if(stringQueue.Count == 0)
        {
            textParent.SetActive(false);
            // 문장이 끝났을 때 이팩트가 넣겠다. GameObject.SetActvie(True)
            return;
        }

        string sentence = stringQueue.Dequeue();         // 문장을 출력하는 장소에 우리가 저장해둔 자료구조에서 하나의 문장을 꺼내온다.

        StopAllCoroutines();                            // 코루틴이 중복해서 문장을 출력하는 현상을 방지해준다.
        StartCoroutine(TypeSentence(sentence));         // 우리가 불러올 문장을 한글자씩 호출하는 코루틴(TypeSentece)을 호출한다.
    }

    IEnumerator TypeSentence(string sentence)       // 코루틴. 문장을 한 글자씩 출력하는 코루틴입니다.
    {
        textComponent.text = "";                         // 출력할 text를 ""(공백)으로 비워두고
        foreach (char letter in sentence.ToCharArray())  // sentence : 전체 문장 , letter : 문장에 있는 한 글자들
        {
            textComponent.text += letter;

            yield return new WaitForSeconds(typeSpeed);       // 실제로 글자가 출력될 버퍼 시간 넣어 주면 됩니다.
        }
    }

}
