using System;

class Weapon
{
    private int _damage;
    private int _bullets;

    public Weapon(int damage, int bullets)
    {
        if (bullets > 0)
            _bullets = bullets;
        else
            throw new InvalidOperationException();

        if (damage > 0)
            _damage = damage;
        else
            throw new InvalidOperationException();
    }

    public int Fire()
    {
        if (_bullets <= 0)
            throw new InvalidOperationException();

        _bullets -= 1;

        return _damage;
    }
}

class Player
{
    private int _health;

    public Player(int health)
    {
        if (health > 0)
            _health = health;
        else
            throw new InvalidOperationException();
    }

    public bool IsAlive { get; private set; }

    public void TakeDamage(int damage)
    {
        if (IsAlive == false)
            throw new InvalidOperationException();

        _health -= damage;

        if (_health <= 0)
            IsAlive = false;
    }
}

class Bot
{
    private Weapon _weapon;

    public void OnSeePlayer(Player player)
    {
        player.TakeDamage(_weapon.Fire());
    }
}