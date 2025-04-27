using UnityEngine;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharPerSec;   //�ʴ� �������� ���� ��
    public bool isAnimation;    //���� �ִϸ��̼� ��� ������
    public GameObject EndCursor;

    Text msgText;
    AudioSource audioSource;
    int index;
    float interval;
    string targetMsg;        //�ش� �ؽ�Ʈ


    private void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetMsg(string msg)
    {
        //���� ��ȭ ��ŵ�ϸ� ���� ��ȭ �ߴ�, Effectend�� �ٷΰ���
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
        //���� �ð� �ݺ� ȣ��
        interval = 1.0f / CharPerSec;
        Debug.Log(interval);

        isAnimation = true;
        Invoke("Effecting", interval);
    }

    void Effecting()
    {
        //�ؽ�Ʈ�� ��� ���� 1���ھ� ���
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];

        //���� ���
        //Ư������ ���� ���� ������ ���
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
