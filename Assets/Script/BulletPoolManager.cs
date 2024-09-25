using UnityEngine;
using UnityEngine.Pool;

public class BulletPoolManager : MonoBehaviour
{
    public static BulletPoolManager instance;

    public int defaultCapacity = 30;
    public int maxPoolSize = 35;
    public GameObject[] bulletPrefabs; // 여러 종류의 총알 프리팹
    public Transform poolParent; // 풀링된 오브젝트를 부모로 설정할 Transform

    public IObjectPool<GameObject> Pool { get; private set; }

    private void Awake()
    {
        if (!instance)
            instance = this;
        else
            Destroy(gameObject);

        Init();
    }

    private void Init()
    {
        Pool = new ObjectPool<GameObject>(
            CreatePooledItem,
            OnTakeFromPool,
            OnReturnToPool,
            OnDestroyPoolGameObject,
            true, defaultCapacity, maxPoolSize
        );

        // 초기 풀 생성
        for (int i = 0; i < defaultCapacity; i++)
        {
            Pool.Release(CreatePooledItem());
        }
    }

    private GameObject CreatePooledItem()
    {
        // 프리팹 배열에서 랜덤하게 선택
        int index = Random.Range(0, bulletPrefabs.Length);
        GameObject prefab = bulletPrefabs[index];
        GameObject poolGo = Instantiate(prefab, poolParent);
        poolGo.GetComponent<BulletSetting>().Pool = this.Pool;
        poolGo.SetActive(false); // 초기에는 비활성화 상태
        return poolGo;
    }

    private void OnTakeFromPool(GameObject poolGo)
    {
        poolGo.SetActive(true);
    }

    private void OnReturnToPool(GameObject poolGo)
    {
        poolGo.SetActive(false);
    }

    private void OnDestroyPoolGameObject(GameObject poolGo)
    {
        Destroy(poolGo);
    }

    // 특정 이름의 오브젝트 가져오기 (필요 시 구현)
    public GameObject GetGo(string goName)
    {
        foreach (var prefab in bulletPrefabs)
        {
            if (prefab.name == goName)
            {
                GameObject poolGo = Instantiate(prefab, poolParent);
                poolGo.GetComponent<BulletSetting>().Pool = this.Pool;
                return poolGo;
            }
        }
        return null;
    }
}
