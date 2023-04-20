using UnityEngine;

public class MissionBoard : MonoBehaviour
{
    public GameObject[] missions = new GameObject[4];
    public GameObject targetControllerGO;
    public int missionsComplete;
    public bool missionOneComplete = false;
    public bool missionTwoComplete = false;
   
    void Update()
    {
        if (targetControllerGO.GetComponent<TargetLevelController>().levelOneComplete == true && missionOneComplete == false)
        {
            MissionComplete();

            missionOneComplete = true;
        }
        if (targetControllerGO.GetComponent<TargetLevelController>().levelTwoComplete == true && missionTwoComplete == false)
        {
            MissionComplete();

            missionTwoComplete = true;
        }
    }

    public void MissionComplete()
    {
        missionsComplete += 1;

        for (int i = 0; i < missionsComplete; i++)
        {
            if(i == missionsComplete - 1) //if is the new mission
            {
                var mission = missions[i];
                var paperClosed = mission.transform.Find("PaperClosed").gameObject;
                var paperOpen = mission.transform.Find("PaperOpen").gameObject;

                var canvas = mission.transform.Find("Canvas").gameObject;
                var title = canvas.transform.Find("Title").gameObject;
                var missionDescription = canvas.transform.Find("MissionDescription").gameObject;
                var checkMark = canvas.transform.Find("Check").gameObject;

                paperClosed.SetActive(false);

                paperOpen.SetActive(true);
                title.SetActive(true);
                missionDescription.SetActive(true);
            }

            else //is a completed mission
            {
                var mission = missions[i];
                var paperClosed = mission.transform.Find("PaperClosed").gameObject;
                var paperOpen = mission.transform.Find("PaperOpen").gameObject;

                var canvas = mission.transform.Find("Canvas").gameObject;
                var title = canvas.transform.Find("Title").gameObject;
                var missionDescription = canvas.transform.Find("MissionDescription").gameObject;
                var checkMark = canvas.transform.Find("Check").gameObject;

                paperClosed.SetActive(false);

                paperOpen.SetActive(true);
                title.SetActive(true);
                missionDescription.SetActive(true);
                checkMark.SetActive(true);
            }
        }
    }
}
