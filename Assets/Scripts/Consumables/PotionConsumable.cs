using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PotionType { Health, Strength};
public class PotionConsumable : MonoBehaviour
{
    [SerializeField] PotionType potion = PotionType.Health;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(potion == PotionType.Health)
                InventoryManager.AddHealthPotion();
            if (potion == PotionType.Strength)
                InventoryManager.AddStrengthPotion();
            Destroy(gameObject);
        }
    }
}
