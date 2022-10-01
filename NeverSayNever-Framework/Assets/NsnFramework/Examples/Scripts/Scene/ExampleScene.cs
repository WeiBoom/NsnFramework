using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NeverSayNever.Example
{
    public class ExampleScene : MonoBehaviour
    {
        private string cubeName = "example_model_cube";
        // Start is called before the first frame update
        void Start()
        {
            IResMdl resMdl = Framework.GetModule<IResMdl>();
            resMdl.LoadModel(cubeName, (obj) =>
            {
                NsnLog.Print("[TEST] load model [{0}] completed ，", cubeName);
                var cubeObj = obj as GameObject;
                var cube = GameObject.Instantiate(cubeObj, new Vector3(0, 0, -5), Quaternion.Euler(45, 0, 0));
                cube.gameObject.name = cubeName;
                CubeRotation cubeRotation = cube.GetComponent<CubeRotation>();
                cubeRotation.speed = 30;
            });
        }
    }
}