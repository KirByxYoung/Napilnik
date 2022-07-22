class Player { }
class Gun { }
class Camera { }

class Squad
{
    public IReadOnlyCollection<Unit> UnitsToGet { get; private set; }
}