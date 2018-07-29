using System;
public class Program {
    public static int Puzzle(int x) {
        int y = 0;
        switch (x)
        {
            case 1: y += 4; break;
            case 2: y += 8; break;
            default: y = x - 100; break;
        }
        return y;
    }
}

