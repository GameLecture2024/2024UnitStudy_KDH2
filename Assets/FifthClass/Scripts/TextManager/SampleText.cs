using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SampleText 
{
    public int sceneID;
    // NPC �̸�
    public string npcName;
    // NPC ������ �̹����� �̸�
    public string ImageName;
    // NPC�� ��ȭ�� ����
    public string[] sentences;
}

public class SampleTextList
{
    public SampleText[] sampleTexts;
}
