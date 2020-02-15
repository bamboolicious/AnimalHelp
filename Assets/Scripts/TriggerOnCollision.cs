using UnityEngine;
using UnityEngine.Events;

public class TriggerOnCollision : MonoBehaviour
{
    [Tooltip("True : Check for trigger ,False : Check for collision")]
    [SerializeField] private bool checkOnTrigger;
    [Tooltip("Insert the string of the tag you want to check against here")]
    [SerializeField] private string triggerLayer;
    [Tooltip("function(s) to call when destroying/deactivating")]
    [SerializeField] private UnityEvent calledOnEnter, calledOnExit;

    // void TriggerEnter(Collider2D other)
    // {
    //     if (other.CompareTag(triggerLayer))
    //     {
    //         // //print("Trigger with " + other.gameObject + " in : " + destroyLayer);
    //         // if (checkOnTrigger)
    //         // {
    //         //     if (destroy)
    //         //     {
    //         //         //print("Destroyed");
    //         //         Destroy(this.gameObject);
    //         //     }
    //         //     else
    //         //     {
    //         //         //print("Deactivated");
    //         //         this.gameObject.SetActive(false);
    //         //     }
    //         // }
    //     }
    // }
    // void CollisionEnter(Collision2D other)
    // {
    //     // //print("ENTERED COLLISION");
    //     // if (other.gameObject.CompareTag(triggerLayer))
    //     // {
    //     //     //print("Collision with " + other.gameObject + " in : " + destroyLayer);
    //     //     if (!checkOnTrigger)
    //     //     {
    //     //         if (destroy)
    //     //         {
    //     //             //print("Destroyed");
    //     //             Destroy(this.gameObject);
    //     //         }
    //     //         else
    //     //         {
    //     //             //print("Deactivated");
    //     //             this.gameObject.SetActive(false);
    //     //         }
    //     //     }
    //     // }
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(triggerLayer))
        {
            calledOnEnter.Invoke();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(triggerLayer))
        {
            calledOnExit.Invoke();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(triggerLayer))
        {
            calledOnEnter.Invoke();
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(triggerLayer))
        {
            calledOnExit.Invoke();
        }
    }

}