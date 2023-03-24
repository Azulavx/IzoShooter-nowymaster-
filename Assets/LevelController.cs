using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public GameObject zombiePrefab;
    public float spawnTime = 5.0f; // czas miêdzy spawnami
    public int maxZombies = 3; // maksymalna liczba zombie na scenie
    

    private List<GameObject> zombies; // lista przechowuj¹ca aktualne zombie na scenie

    // Start is called before the first frame update
    void Start()
    {
        zombies = new List<GameObject>();
        InvokeRepeating("SpawnZombie", 0.0f, spawnTime);
    }

    // Funkcja do spawnowania nowego zombie
    void SpawnZombie()
    {
        if (zombies.Count < maxZombies)
        {
            Vector3 randomPos = RandomVector(10, 20); // losowa pozycja do spawnu
            if (Physics.CheckSphere(randomPos, 1.0f)) // sprawdŸ czy pozycja jest wolna
            {
                GameObject zombie = Instantiate(zombiePrefab, randomPos, Quaternion.identity); // spawnuj nowego zombie
                zombies.Add(zombie); // dodaj do listy aktualnych zombie
                zombie.GetComponent<ZombieController>().OnDeath += RemoveZombie; // zarejestruj zdarzenie œmierci zombie
            }
        }
    }

    // Funkcja do usuwania zombie z listy aktualnych po jego œmierci
    void RemoveZombie(GameObject zombie)
    {
        zombies.Remove(zombie);
    }

    // Funkcja zwracaj¹ca losowy wektor w zakresie magnitude od 10 do 20
    Vector3 RandomVector(float min, float max)
    {
        float x = Random.Range(-1.0f, 1.0f);
        float z = Random.Range(-1.0f, 1.0f);
        Vector3 randomVector = new Vector3(x, 0.0f, z).normalized * Random.Range(min, max);
        return randomVector;
    }
}
