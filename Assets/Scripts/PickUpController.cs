using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour
{
    public int pickupColorIndex;

    void Start()
    {
        // Khởi tạo vị trí ban đầu của đối tượng Pick Up
        Vector3 spawnPosition = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
        transform.position = spawnPosition;
    }

    public IEnumerator RespawnPickUp()
    {
        gameObject.SetActive(false); // Vô hiệu hóa đối tượng Pick Up

        yield return new WaitForSeconds(1f);

        // Tạo vị trí mới và kích hoạt lại đối tượng Pick Up
        Vector3 spawnPosition = new Vector3(Random.Range(-12, 12), 0.5f, Random.Range(-12, 12));
        transform.position = spawnPosition;
        gameObject.SetActive(true);
    }
}
