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
        //// ���������� ������ "������"
        //Vector3 globalRight = Vector3.right; // (1, 0, 0)

        //// ������� ��������� +X ������� � ���������� �����������
        //Vector3 localRight = transform.right;

        //// ��������� ��������� ������������
        //float dotProduct = Vector3.Dot(localRight.normalized, globalRight);

        //// ����������� ���������
        //if (dotProduct > 0.5f)
        //{
        //    Debug.Log("������ ������� ������");
        //}
        //else if (dotProduct < -0.5f)
        //{
        //    Debug.Log("������ ������� �����");
        //}
        //else
        //{
        //    Debug.Log("������ ������� ��� �����");
        //}
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // ���������� ����� �� ������� ������� ����� transform.right
        Gizmos.DrawLine(transform.position, transform.position + transform.right * 2);
    }
}
