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

        return Resources.Load<T>(path); //������Ʈ�ҷ�����
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {

        GameObject original = Load<GameObject>($"Prefabs/{path}"); //prefab ã��(�޸𸮿� ���)

        if (original == null)//ã�� �� �� ���
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }

        // pooling�� ������Ʈ�� �ִ��� ã�ƺ���
        if (original.GetComponent<Poolable>() != null)
            return Managers.Pool.Pop(original, parent).gameObject;


        GameObject go = Object.Instantiate(original, parent); //prefab ����
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

        // 3. ���� pooling�� �ʿ��� ��� poolmanager���� ��Ź

        Poolable poolalbe = go.GetComponent<Poolable>();
        if(poolalbe != null)
        {
            Managers.Pool.Push(poolalbe);
            return;
        }

        Object.Destroy(go);
    }
}