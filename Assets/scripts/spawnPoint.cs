using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //// Глобальный вектор "вправо"
        //Vector3 globalRight = Vector3.right; // (1, 0, 0)

        //// Текущий локальный +X объекта в глобальных координатах
        //Vector3 localRight = transform.right;

        //// Вычисляем скалярное произведение
        //float dotProduct = Vector3.Dot(localRight.normalized, globalRight);

        //// Анализируем результат
        //if (dotProduct > 0.5f)
        //{
        //    Debug.Log("Объект смотрит вправо");
        //}
        //else if (dotProduct < -0.5f)
        //{
        //    Debug.Log("Объект смотрит влево");
        //}
        //else
        //{
        //    Debug.Log("Объект смотрит под углом");
        //}
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Нарисовать линию из позиции объекта вдоль transform.right
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2);
    }
}
