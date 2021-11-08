  LDARGS          ; void main(int n) {
  CALL (1, "L1")
  STOP

Label "L1"        ; int i;
  INCSP 1
  GETBP
  CSTI 1
  ADD
  CSTI 0
  STI             ; i = 0;
  INCSP -1
  GOTO "L3"

Label "L2"
  GETBP
  CSTI 1
  ADD
  LDI
  PRINTI          ; print i;
  INCSP -1
  GETBP
  CSTI 1
  ADD
  GETBP
  CSTI 1
  ADD
  LDI
  CSTI 1
  ADD
  STI             ; i=i+1;
  INCSP -1
  INCSP 0

Label "L3"
  GETBP
  CSTI 1
  ADD
  LDI
  GETBP
  CSTI 0
  ADD
  LDI
  LT              ; (i < n)
  IFNZRO "L2"
  INCSP -1
  RET 0
