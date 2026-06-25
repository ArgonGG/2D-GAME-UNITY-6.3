
using UnityEngine;
using UnityEngine.Tilemaps;

public class CollectibleTilemap : MonoBehaviour
{
    private Tilemap tilemap;
    private AudioSource audioSource;

    [Header("Statystyki")]
    public int iloscZebranych = 0;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Wykryto kolizjê z: " + collision.gameObject.name);

        if (collision.CompareTag("Player"))
        {
            Vector3 playerPos = collision.bounds.center;
            Vector3Int centerCell = tilemap.WorldToCell(playerPos);

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    Vector3Int checkCell = new Vector3Int(centerCell.x + x, centerCell.y + y, centerCell.z);

                    if (tilemap.HasTile(checkCell))
                    {
                        if (audioSource != null && audioSource.clip != null)
                        {
                            audioSource.Play();
                            Debug.Log("Odtwarzam dŸwiêk z AudioSource: " + audioSource.clip.name);
                        }

                        tilemap.SetTile(checkCell, null);
                        iloscZebranych++;
                        Debug.Log("Zebrano bloczek! £¹cznie: " + iloscZebranych);
                        return;
                    }
                }
            }
        }
    }
}