public static void CreateOnMap()
{
    //Создание объекта на карте
}

public static void SetRandomChance()
{
    _chance = Random.Range(0, 100);
}

public static int GetCalculatedSalary(int hoursWorked)
{
    return _hourlyRate * hoursWorked;
}