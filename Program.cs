class Player
{
    public string Name { get; private set; }
    public int Age { get; private set; }

    public void Move()
    {
        //Do move
    }
}

class Movement
{
    private float DirectionX { get; private set; }
    private float DirectionY { get; private set; }
    public float Speed { get; private set; }
}

class Weapon
{
    public int Damage { get; private set; }
    public float Cooldown { get; private set; }

    public void Attack()
    {
        //attack
    }

    public bool IsReloading()
    {
        throw new NotImplementedException();
    }
}