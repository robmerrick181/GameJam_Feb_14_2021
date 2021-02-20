using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Control equipping, equip position, equip rotation, damage, dropping this equipped item
/// </summary>
public class WeaponController : MonoBehaviour
{
    public float Damage = 10f;

    [SerializeField] private Text _damageText;

    private void Start()
    {
        _damageText.text = Damage.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            InventoryManager.Player.ChangeWeaponDamage(Damage);
            Destroy(gameObject);
        }
    }


    //[SerializeField] private Vector3 _posEquipped;
    //[SerializeField] private Quaternion _rotEquipped;

    //[SerializeField] private GameObject ParentToObj;

    //private bool _isEquipped = false;

    //private void MoveToHand()
    //{
    //    transform.SetParent(ParentToObj.transform);
    //    transform.localPosition = _posEquipped;
    //    transform.localRotation = _rotEquipped;
    //    _isEquipped = true;
    //}

    //private void UnEquipItem()
    //{
    //    transform.SetParent(null);
    //    _isEquipped = false;
    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.tag == "Player" && !_isEquipped)
    //    {
    //        MoveToHand();
    //    }
    //}

}
