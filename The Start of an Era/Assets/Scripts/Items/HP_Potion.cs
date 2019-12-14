using UnityEngine;

public class HP_Potion : MonoBehaviour, IItemBehaviour
{
    [SerializeField] private int _HP_Restore = default;
    
    [SerializeField] private Player _player = default;
    private SpriteRenderer _sprite;
    private Collider2D _selfColl;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _selfColl = GetComponent<Collider2D>();
    }

    public void ItemUse()
    {
        RestoreHP();
        Destroy(gameObject);
    }

    private void RestoreHP()
    {
        _player.Heal(_HP_Restore);
    }
    public void OnPickUp()
    {
        // Vai a farmacia comprimidos
        _player.InventoryAdd(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (!_player.IsInventoryFull)
            {
                OnPickUp();
                _sprite.enabled = false;
                _selfColl.enabled = false;
            }
        }
    }
}
