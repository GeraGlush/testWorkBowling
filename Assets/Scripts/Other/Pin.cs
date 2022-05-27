using System.Collections;
using UnityEngine;

public class Pin : MonoBehaviour
{
   private bool beDestroyded = false;
   private void OnCollisionEnter(Collision other)
   {
      if (other.collider.gameObject.tag == "Player" && !beDestroyded)
      {
         FindObjectOfType<PinManager>().pinDestroy++;
         StartCoroutine(DeledeForTime());
      }
   }

   /// <summary>
   /// Удаление обьекта, чтобы не нагружать программу.
   /// </summary>
   private IEnumerator DeledeForTime()
   {
      beDestroyded = true;
      GetComponent<Rigidbody>().AddForce(Vector3.up * 2, ForceMode.Impulse);
      yield return new WaitForSeconds(10f);
      Destroy(gameObject);
   }
}
