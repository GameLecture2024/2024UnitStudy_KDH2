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
        npcNameText.text = sampleTexts[0].npcName;          // UI�� NPC �̸��� ������ NPC�̸��� ���
        textComponent.text = sampleTexts[0].sentences[0];    // UI�� ù ��° �׸��� ù�� ° ��縦 ���
        npcIcon.sprite = Resources.Load<Sprite>($"Album/{sampleTexts[0].ImageName}");

        SampleText sampleText = sampleTexts[0];             // ù��° �����͸� �����ͼ� sampleText�� ����

        foreach (string sentence in sampleText.sentences)   // sampleText�� ����� ����鿡 �����Ͽ� ������ sentence�� 
        {
            stringQueue.Enqueue(sentence);                  // stringQueue��� �ڷᱸ���� �����Ѵ�.
        }
    }

    public void DisplayNextSentence()
    {
        Debug.Log("���� StringQueue ���� �ֱ�");

        if(stringQueue.Count == 0)
        {
            textParent.SetActive(false);
            // ������ ������ �� ����Ʈ�� �ְڴ�. GameObject.SetActvie(True)
            return;
        }

        string sentence = stringQueue.Dequeue();         // ������ ����ϴ� ��ҿ� �츮�� �����ص� �ڷᱸ������ �ϳ��� ������ �����´�.

        StopAllCoroutines();                            // �ڷ�ƾ�� �ߺ��ؼ� ������ ����ϴ� ������ �������ش�.
        StartCoroutine(TypeSentence(sentence));         // �츮�� �ҷ��� ������ �ѱ��ھ� ȣ���ϴ� �ڷ�ƾ(TypeSentece)�� ȣ���Ѵ�.
    }

    IEnumerator TypeSentence(string sentence)       // �ڷ�ƾ. ������ �� ���ھ� ����ϴ� �ڷ�ƾ�Դϴ�.
    {
        textComponent.text = "";                         // ����� text�� ""(����)���� ����ΰ�
        foreach (char letter in sentence.ToCharArray())  // sentence : ��ü ���� , letter : ���忡 �ִ� �� ���ڵ�
        {
            textComponent.text += letter;

            yield return new WaitForSeconds(typeSpeed);       // ������ ���ڰ� ��µ� ���� �ð� �־� �ָ� �˴ϴ�.
        }
    }

}
