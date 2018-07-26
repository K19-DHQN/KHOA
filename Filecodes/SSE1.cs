public static int sol(int x) 
{
  int y = 0;
  switch (x)
  {
	  case 1: 
	  y += 4;
	  break;                
	  default: 
	  y = x - 100; 
	  break;
  }
  return y;        
}