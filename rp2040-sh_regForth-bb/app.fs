: stimex 384 \ 900
  #, for
      msec
  next
; \ 900 milliseconds

: n867
  6 #, cmd! start

  begin

  ." STACK: " .s cr

  gl_ lv2! stimex \ blank 2 write 3
  gl_ lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl_ lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl_ lv1! stimex

  \ write zero for the stanza - no blank needed

  gl_ lv0! stimex stimex stimex stimex stimex stimex




  gl_ lv2! stimex \ blank 2 write 3
  gl_ lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl_ lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl_ lv1! stimex

  \ write zero for the stanza - no blank needed

  gl8 lv0! stimex stimex stimex stimex stimex stimex




  gl_ lv2! stimex \ blank 2 write 3
  gl_ lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl_ lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl8 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl6 lv0! stimex stimex stimex stimex stimex stimex




  gl_ lv2! stimex \ blank 2 write 3
  gl_ lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl8 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl6 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl7 lv0! stimex stimex stimex stimex stimex stimex



  gl_ lv2! stimex \ blank 2 write 3
  gl8 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl6 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl7 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl5 lv0! stimex stimex stimex stimex stimex stimex


  gl_ lv2! stimex \ blank 2 write 3
  gl6 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl7 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl5 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl3 lv0! stimex stimex stimex stimex stimex stimex


  gl_ lv2! stimex \ blank 2 write 3
  gl7 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl5 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl3 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl0 lv0! stimex stimex stimex stimex stimex stimex


  gl_ lv2! stimex \ blank 2 write 3
  gl5 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl3 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl0 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl9 lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv2! stimex \ blank 2 write 3
  gl3 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl0 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl9 lv1! stimex

  \ write zero for the stanza - no blank needed

  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv2! stimex \ blank 2 write 3
  gl0 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl9 lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl_ lv1! stimex

  \ write zero for the stanza - no blank needed

  gl_ lv0! stimex stimex stimex stimex stimex stimex


  gl_ lv2! stimex \ blank 2 write 3
  gl9 lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl_ lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl_ lv1! stimex

  \ write zero for the stanza - no blank needed

  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv2! stimex \ blank 2 write 3
  gl_ lv3! stimex

  gl_ lv1! stimex \ blank 1 write 2
  gl_ lv2! stimex

  gl_ lv0! stimex \ blank 0 write 1
  gl_ lv1! stimex

  \ write zero for the stanza - no blank needed

  gl_ lv0! stimex stimex stimex stimex stimex stimex

  again

;

: run n867 ;

\ end.
