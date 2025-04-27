using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSec;   //초당 보여지는 글자 수
    public bool isAnimation;    //현재 애니메이션 재생 중인지
    public GameObject EndCursor;

    Text msgText;
    AudioSource audioSource;
    int index;
    float interval;
    string targetMsg;        //해당 텍스트


    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        //만약 대화 스킵하면 현재 대화 중단, Effectend로 바로가기
        if (isAnimation)
        {
            msgText.text = targetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            targetMsg = msg;
            EffectStart();
        }
    }

    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);
        //지연 시간 반복 호출
        interval = 1.0f / CharPerSec;
        Debug.Log(interval);

        isAnimation = true;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        //텍스트의 모든 글자 1글자씩 출력
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];

        //사운드 재생
        //특수문자 제외 글자 나오면 재생
        if (targetMsg[index] != ' ' || targetMsg[index] != '.')
            audioSource.Play();

        index++;

        Invoke("Effecting", interval);
    }

    void EffectEnd()
    {
        isAnimation = false;
        EndCursor.SetActive(true);
    }
}
