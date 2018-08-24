int Code1(int x) {
  int y = 0;
  switch (x) {
    case 1: y += 4; break;    
    default: y = x * x;
  }
  return y;
}