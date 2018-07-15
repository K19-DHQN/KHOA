int foo(int v) 
{
   return 2*v;
}
void test_me(int x, int y) {
   int z = foo(y);
   if (z == x)
      if (x > y+10)
         ERROR;
}
