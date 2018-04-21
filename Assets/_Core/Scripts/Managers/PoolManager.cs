using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolable
{
    bool Active { get; set; }
}

public class PoolManager : MonoBehaviour
{
    [SerializeField]
    int _sizeOfStartPool;
    [SerializeField]
    Transform _poolParent;
    List<Transform> _pool;
    [SerializeField]
    GameObject _prefab;

    void Start()
    {
        _pool = new List<Transform>();
        if (_poolParent == null)
            _poolParent = transform;
        startPool(_sizeOfStartPool);
    }
    void startPool(int numberOfObjects) //Instansierar startantaler av objekt och lägger dem till poolen
    {
        for (int i = 0; i < numberOfObjects; i++)
        {
            Transform obj;
            obj = Instantiate(_prefab).transform;
            obj.SetParent(_poolParent);
            obj.position = _poolParent.position;
            _pool.Add(obj);
        }
    }
    public GameObject InstantiatePool(Vector3 spawnPosition)
    {
        Transform obj;

        if (_pool.Count > 0) //Hämtar ett objekt från poolen om det finns något där
        {
            obj = _pool[0];
            _pool.RemoveAt(0);
        }
        else // Instansierar en ny
        {
            obj = Instantiate(_prefab).transform;
            obj.SetParent(_poolParent);
        }

        obj.position = spawnPosition;
        obj.GetComponent<IPoolable>().Active = true;
        return obj.gameObject;

    }

    public void DestroyPool(Transform obj) //Stänger av objectet och flyttar det till dess pool.
    {
        obj.position = _poolParent.position;
        _pool.Add(obj);
        obj.GetComponent<IPoolable>().Active = false;
    }
    public string HelloWorld()
    {
        return "Hello World!";
    }


}
