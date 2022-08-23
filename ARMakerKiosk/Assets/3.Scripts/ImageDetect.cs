using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public class MakerInfo
{
    public string imageNmae;//�̹�������
    public GameObject targetObj; //�̹����� ���������� ��Ÿ���� obj

    //AR Tracked Image Manager�� �ν�
   
}
public class ImageDetect : MonoBehaviour
{
    // ���� MakeInfo�� Ŭ���� �� �ڷ������� �ϴ� ������ �迭�� ��� ������
    public MakerInfo[] makeinfos;  //Ŭ������ public���� �ϸ� �ν�����â���� �ȳ�Ÿ���Ƿ� ���� [serializable] ����
    
    ARTrackedImageManager trackedManager; 

    void Start()
    {
        trackedManager = GetComponent<ARTrackedImageManager>(); //ARtrakced~�� int��
        //�Լ� ��� ������ ��������Ʈ=Actrion<>
        //������ȭ(�̹��� �νĿ���)���� �� ȣ��Ǵ� �Լ� ���
        trackedManager.trackedImagesChanged += OntrackedImageChanged;

    
    }
    private void OnDestroy()
    {
        //���� ������ �� �ڵ� ȣ��  > ����ߴ� �Լ��� �� ��
        trackedManager.trackedImagesChanged -= OntrackedImageChanged;
    }

    void OntrackedImageChanged(ARTrackedImagesChangedEventArgs events)
    {
        //�νĵ� �̹����� �츮�ʿ� ����� �̹��� ������ ��
        //events�� �νĵ� �̹����� updated(list��)
        for(int i=0; i < events.updated.Count; i++)
        {
            ARTrackedImage trImage = events.updated[i]; //1�� �̾Ƽ� ��� ����

            for(int j=0; j<makeinfos.Length; j++)
            {

                if (trImage.referenceImage.name == makeinfos[j].imageNmae)
                {
                    if (trImage.trackingState == TrackingState.Tracking)
                    {
                        //���࿡ Ʈ��ŷ �� �� ��� Ʈ��ŷ ã�� ���¶��

                        makeinfos[j].targetObj.SetActive(true);

                        //�̹��� ���󰡱�
                        makeinfos[j].targetObj.transform.position = trImage.transform.position;
                        makeinfos[j].targetObj.transform.up = trImage.transform.up;
                    }
                    else
                    { //Ʈ��ŷ �� ����� ���¶��

                        makeinfos[j].targetObj.SetActive(false);

                    }
                }
            }


        }
    }
  
}
