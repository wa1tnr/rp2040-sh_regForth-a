: stimex 384 \ 900
  #, for
      msec
  next
; \ 900 milliseconds

: n867
  6 #, cmd! start

  begin

  ." STACK: " .s cr

  gl_ lv3! stimex
  gl_ lv2! stimex
  gl_ lv1! stimex
  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv3! stimex
  gl_ lv2! stimex
  gl_ lv1! stimex
  gl8 lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv3! stimex
  gl_ lv2! stimex
  gl8 lv1! stimex
  gl6 lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv3! stimex
  gl8 lv2! stimex
  gl6 lv1! stimex
  gl7 lv0! stimex stimex stimex stimex stimex stimex

  gl8 lv3! stimex
  gl6 lv2! stimex
  gl7 lv1! stimex
  gl5 lv0! stimex stimex stimex stimex stimex stimex

  gl6 lv3! stimex
  gl7 lv2! stimex
  gl5 lv1! stimex
  gl3 lv0! stimex stimex stimex stimex stimex stimex

  gl7 lv3! stimex
  gl5 lv2! stimex
  gl3 lv1! stimex
  gl0 lv0! stimex stimex stimex stimex stimex stimex

  gl5 lv3! stimex
  gl3 lv2! stimex
  gl0 lv1! stimex
  gl9 lv0! stimex stimex stimex stimex stimex stimex

  gl3 lv3! stimex
  gl0 lv2! stimex
  gl9 lv1! stimex
  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl0 lv3! stimex
  gl9 lv2! stimex
  gl_ lv1! stimex
  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl9 lv3! stimex
  gl_ lv2! stimex
  gl_ lv1! stimex
  gl_ lv0! stimex stimex stimex stimex stimex stimex

  gl_ lv3! stimex
  gl_ lv2! stimex
  gl_ lv1! stimex
  gl_ lv0! stimex stimex stimex stimex stimex stimex

  again

;

: run n867 ;

\ end.
