using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[System.Serializable]
public class MakerInfo
{
    public string imageNmae;//이미지내용
    public GameObject targetObj; //이미지가 보여졌을때 나타나는 obj

    //AR Tracked Image Manager로 인식
   
}
public class ImageDetect : MonoBehaviour
{
    // 위의 MakeInfo의 클래스 내 자료형으로 하는 정보를 배열로 잠시 가져옴
    public MakerInfo[] makeinfos;  //클래스를 public으로 하면 인스펙터창에서 안나타나므로 위에 [serializable] 쓴것
    
    ARTrackedImageManager trackedManager; 

    void Start()
    {
        trackedManager = GetComponent<ARTrackedImageManager>(); //ARtrakced~는 int임
        //함수 담는 변수인 델리게이트=Actrion<>
        //정보변화(이미지 인식여부)있을 때 호출되는 함수 등록
        trackedManager.trackedImagesChanged += OntrackedImageChanged;

    
    }
    private void OnDestroy()
    {
        //뭔가 없어질 때 자동 호출  > 등록했던 함수를 뺄 것
        trackedManager.trackedImagesChanged -= OntrackedImageChanged;
    }

    void OntrackedImageChanged(ARTrackedImagesChangedEventArgs events)
    {
        //인식된 이미지와 우리쪽에 저장된 이미지 정보를 비교
        //events가 인식된 이미지의 updated(list임)
        for(int i=0; i < events.updated.Count; i++)
        {
            ARTrackedImage trImage = events.updated[i]; //1개 뽑아서 잠시 담음

            for(int j=0; j<makeinfos.Length; j++)
            {

                if (trImage.referenceImage.name == makeinfos[j].imageNmae)
                {
                    if (trImage.trackingState == TrackingState.Tracking)
                    {
                        //만약에 트래킹 된 게 계속 트래킹 찾는 상태라면

                        makeinfos[j].targetObj.SetActive(true);

                        //이미지 따라가기
                        makeinfos[j].targetObj.transform.position = trImage.transform.position;
                        makeinfos[j].targetObj.transform.up = trImage.transform.up;
                    }
                    else
                    { //트래킹 외 사라진 상태라면

                        makeinfos[j].targetObj.SetActive(false);

                    }
                }
            }


        }
    }
  
}
