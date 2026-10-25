using UnityEngine;

public static class ShootableFactory
{
    private static GameObject bottlePrefab = Resources.Load<GameObject>("Prefabs/Bottle"); // Complicado hacerlo async y estático (desde aquí) así que lagspike de carga 
    private static GameObject bombPrefab = Resources.Load<GameObject>("Prefabs/Bomb");
    private static GameObject canPrefab = Resources.Load<GameObject>("Prefabs/Can");



    public static GameObject CreateBomb()
    {
        GameObject bomb = GameObject.Instantiate(bombPrefab);
        return bomb;
    }

    public static GameObject CreateBottle()
    {
        GameObject bottle = GameObject.Instantiate(bottlePrefab);
        return bottle;
    }

    public static GameObject CreateCan()
    {
        GameObject can = GameObject.Instantiate(canPrefab);
        return can;
    }

    public static GameObject CreateRandom()
    {
        switch (Random.value) // La magia de los switches en C# no la conocía yo, muy cómodo
        {
            case > .66f:
                return CreateBomb();
            case > .33f:
                return CreateCan();
            default:
                return CreateBottle();
        }
    }
    public static GameObject CreateSafe()
    {
        switch (Random.value) // La magia de los switches en C# no la conocía yo, muy cómodo
        {
            case > .5f:
                return CreateCan();
            default:
                return CreateBottle();
        }
    }
}
