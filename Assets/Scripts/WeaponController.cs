using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* TODO
 * Equip item when trigger is intiated
 * If equipped disable trigger
 * 
 * Public damage
 * serialized private local position
 * serialized private local rotation
 * 
 * public Drop this item
 * 
 */ 


/// <summary>
/// Control equipping, equip position, equip rotation, damage, dropping this equipped item
/// </summary>
public class WeaponController : MonoBehaviour
{
    public float Damage = 10f;

    [SerializeField] private Vector3 _posEquipped;
    [SerializeField] private Quaternion _rotEquipped;

    [SerializeField] private GameObject ParentToObj;

    private bool _isEquipped = false;

    private void EquipItem()
    {
        transform.SetParent(ParentToObj.transform);
        transform.localPosition = _posEquipped;
        transform.localRotation = _rotEquipped;
    }

    private void UnEquipItem()
    {
        transform.SetParent(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !_isEquipped)
        {
            EquipItem();
        }
    }

}
