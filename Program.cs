class Weapon
{
    private int _bullets;

    public void Shoot()
    {
        if (CanShoot())
            _bullets--;
    }

    private bool CanShoot() => _bullets > 0;
}