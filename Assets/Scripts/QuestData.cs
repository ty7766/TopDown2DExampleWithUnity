public class QuestData 
{
    public string questName;    //����Ʈ �̸�
    public int[] npcId;         //npcId ����


    //������
    public QuestData(string questName, int[] npcId)
    {
        this.questName = questName;
        this.npcId = npcId;
    }
}
