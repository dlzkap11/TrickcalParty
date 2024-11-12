using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {

        if(typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index >= 0)
                name = name.Substring(index + 1);

            GameObject go = Managers.Pool.GetOriginal(name);
            if (go != null)
                return go as T;
        }

        return Resources.Load<T>(path); //오브젝트불러오기
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject original = Load<GameObject>($"Prefabs/{path}"); //prefab 찾기(메모리에 기억)

        if (original == null)//찾지 못 할 경우
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // pooling된 오브젝트가 있는지 찾아보기
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        GameObject go = Object.Instantiate(original, parent); //prefab 생성
        go.name = original.name;

        /*
        int index = go.name.IndexOf("(Clone)");
        if(index > 0)
            go.name = go.name.Substring(0, index);
        */

        return go;
    }

    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        // 3. 만약 pooling이 필요한 경우 poolmanager에게 위탁

        Poolable poolalbe = go.GetComponent<Poolable>();
        if(poolalbe != null)
        {
            Managers.Pool.Push(poolalbe);
            return;
        }

        Object.Destroy(go);
    }
}