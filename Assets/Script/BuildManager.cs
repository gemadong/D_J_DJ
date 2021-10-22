using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Craft
{
    public string craftname;
    public GameObject previewCraft; //미리보기 프리펩
    public GameObject BuildCraft; // 실제 지어질 프리펩
    public int HasTurretCount = 2;
}
public class BuildManager : MonoBehaviour
{
    [SerializeField] private Craft[] craft = null;

    [SerializeField] private Camera _camera = null;
    private GameObject PreviewPrefab = null;
    private GameObject InsPrefab = null;
    private bool isActivatePreview = false;
    private bool OnClickTurret = false;
    Vector3 MousePos;

    //
    private RaycastHit hitinfo;
    private LayerMask _layerMask;
    Vector3 _location;
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
    }

    public void SlotClick(int _SlotNumber)
    {
        if (craft[_SlotNumber].HasTurretCount <= 0) return;
        PreviewPrefab = Instantiate(craft[_SlotNumber].previewCraft, MousePos, Quaternion.identity);
        InsPrefab = craft[_SlotNumber].BuildCraft;
        craft[_SlotNumber].HasTurretCount--;
        isActivatePreview = true;
        Debug.Log(isActivatePreview);
    }


}
