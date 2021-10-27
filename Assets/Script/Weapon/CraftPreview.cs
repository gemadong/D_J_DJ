using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftPreview : MonoBehaviour
{
    private List<Collider> colliderList = new List<Collider>();

    [SerializeField]
    private int layerGround;
    private const int IGNORE_LAYER = 2;

    [SerializeField] private Material green;
    [SerializeField] private Material red;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        ChangeColor();
    }

    private void ChangeColor()
    {
        if (colliderList.Count > 0) { 
            SetColor(red);
        }
        
        else { SetColor(green); }
    }

    private void SetColor(Material mat)
    {
        foreach(Transform thistransform in transform)
        {
            var newMaterials = new Material[thistransform.GetComponent<Renderer>().materials.Length];
            for(int i = 0; i < newMaterials.Length; i++)
            {
                newMaterials[i] = mat;
            }
            thistransform.GetComponent<Renderer>().materials = newMaterials;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Add(other);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer != layerGround && other.gameObject.layer != IGNORE_LAYER)
        {
            colliderList.Remove(other);
        }
    }

    public bool isBuildable()
    {
        return colliderList.Count == 0;
    }
}
