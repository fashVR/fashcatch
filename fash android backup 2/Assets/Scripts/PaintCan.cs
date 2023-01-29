using UnityEngine;
using UnityEngine.UI;

public class PaintCan : MonoBehaviour
{
    private Inventory inventory;
    public GameObject ItemButton1;
    public GameObject ItemButton2;
    public GameObject blackShade;
    public GameObject effect;
    Animator blackShadeAnim;
    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("FashParent").GetComponent<Inventory>();
        blackShadeAnim = blackShade.GetComponent<Animator>();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Fash"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    
                    Color CanColor = gameObject.GetComponent<SpriteRenderer>().color;
                    ItemButton2.GetComponent<SpriteRenderer>().color = CanColor;
                    inventory.isFull[i] = true;
                    Instantiate(effect, transform.position, transform.rotation);                   
                    Instantiate(ItemButton1, inventory.slots[i].transform, false);
                    Instantiate(ItemButton2, inventory.slots[i].transform, false);
                    Instantiate(blackShade, inventory.slots[i].transform, false);
                    gameObject.SetActive(false);
                    break;
                }
            }
        }
    }

    public void destroyEffect()
    {
        Destroy(effect);
    }
}


