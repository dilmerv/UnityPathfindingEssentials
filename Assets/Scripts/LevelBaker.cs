using DilmerGames.Core.Singletons;
using UnityEngine.AI;

public class LevelBaker : Singleton<LevelBaker>
{
    private NavMeshSurface surface;

    private void Awake() 
    {
        surface = GetComponent<NavMeshSurface>();
    }

    public void BakeLevel()
    {
        surface.BuildNavMesh();
    }
}
