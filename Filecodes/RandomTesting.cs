int myAbs(int x)
{
    if (x > 0)
    {
        return x;
    }
    else
    {
        return x; 
    }
}

void testAbs(int n)
{
    for (int i = 0; i < n; i++)
    {
        int x = getRandomInput();
        int result = myAbs(x);
        assert(result >= 0);
    }
}
