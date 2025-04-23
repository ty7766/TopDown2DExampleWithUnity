public class QuestData 
{
    public string questName;    //퀘스트 이름
    public int[] npcId;         //npcId 저장


    //생성자
    public QuestData(string questName, int[] npcId)
    {
        this.questName = questName;
        this.npcId = npcId;
    }
}
