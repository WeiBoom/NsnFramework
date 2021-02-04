using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace NeverSayNever.Example
{
    public class CubeRotation : MonoBehaviour
    {
        public float speed = 1;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(Vector3.up * Time.deltaTime * speed,Space.World);
        }
    }
}
