using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Craft
{
    public string craftname;
    public GameObject previewCraft; //미리보기 프리펩
    public GameObject BuildCraft; // 실제 지어질 프리펩
    public int HasTurretCount  = 0;
}
public class BuildManager : MonoBehaviour
{
    [SerializeField] private Craft[] craft = null;

    [SerializeField] private Camera _camera = null;

    [SerializeField] private GameObject BuildSlot = null;
    private bool OpenBuildSlot = false;
    [SerializeField] private Text[] ObstacleCount = null;

    [SerializeField] private GameObject Shop=null;
    [SerializeField] private GameObject[] ShopInUi = null;
    


    private GameObject PreviewPrefab = null;    //Craft를 담을 변수와 미리보기에 사용할 변수 선언
    private GameObject InsPrefab = null;
    private int BuildNum = 0;
    
    private bool isActivatePreview = false;
    Vector3 MousePos;
    //
    private RaycastHit hitinfo;
    Vector3 _location;





    private void Awake()
    {
        BuildSlot.SetActive(false);
        Shop.SetActive(false);
    }
    private void Update()
    {


        MousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        
        if (isActivatePreview)
        {
            if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition),out hitinfo))
            {
                if (hitinfo.transform != null)
                {
                    _location = hitinfo.point;
                    _location.y = 0;
                    PreviewPrefab.transform.position = _location;
                }
            }
        }

        if(PreviewPrefab != null&&PreviewPrefab.GetComponent<CraftPreview>().isBuildable())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(InsPrefab,_location ,Quaternion.identity);
                Destroy(PreviewPrefab);
                isActivatePreview = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape)&&PreviewPrefab != null)
        {
            Destroy(PreviewPrefab);
            isActivatePreview = false;
            TurretHasCountUp(BuildNum);
        }

        HaveCount();

    }

    public void HaveCount()
    {

        for (int i = 0; i < craft.Length; i++)
        {
            ObstacleCount[i].text = craft[i].HasTurretCount.ToString("D2") + "개";
        }
    }

    public void TurretHasCountUp(int SlotNumber)
    {
        craft[SlotNumber].HasTurretCount++;
    }

    public void SlotClick(int _SlotNumber)
    {
        if (craft[_SlotNumber].HasTurretCount <= 0) return;
        PreviewPrefab = Instantiate(craft[_SlotNumber].previewCraft, MousePos, Quaternion.identity);
        InsPrefab = craft[_SlotNumber].BuildCraft;
        craft[_SlotNumber].HasTurretCount--;
        isActivatePreview = true;
        Debug.Log(isActivatePreview);
        BuildNum = _SlotNumber;
    }

    public void BuildSlotOpen()
    {
        if (OpenBuildSlot)
        {
            BuildSlot.SetActive(false);
            OpenBuildSlot = false;
        }
        else if (!OpenBuildSlot)
        {
            BuildSlot.SetActive(true);
            OpenBuildSlot = true;
        }
        }

    public void ShopOpen()
    {
        Shop.SetActive(true);
        ShopInUi[0].SetActive(true);
        ShopInUi[1].SetActive(false);
        ShopInUi[2].SetActive(false);
    }

    public void ShopClose()
    {
        Shop.SetActive(false);
    }

    public void WeaponOpen()
    {
        ShopInUi[0].SetActive(true);
        ShopInUi[1].SetActive(false);
        ShopInUi[2].SetActive(false);
    }
    public void CharactorOpen()
    {
        ShopInUi[0].SetActive(false);
        ShopInUi[1].SetActive(true);
        ShopInUi[2].SetActive(false);
    }
    public void BuildItemOpen()
    {
        ShopInUi[0].SetActive(false);
        ShopInUi[1].SetActive(false);
        ShopInUi[2].SetActive(true);
    }



    


}
