namespace Task4
{
    public static int Sort(int a, int b, int c)
    {
        if (a < b)
            return b;
        else if (a > c)
            return c;
        else
            return a;
    }
}