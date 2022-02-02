: stimex 384 \ 900
  #, for
      msec
  next
; \ 900 milliseconds

: n867
  6 #, cmd! start

  begin

  ." STACK: " .s cr

  gl_ lv2!  \ __blank 2 write 3
  gl_ lv3!

  gl_ lv1!  \ __blank 1 write 2
  gl_ lv2!

  gl_ lv0!  \ __blank 0 write 1
  gl_ lv1!

  \ stanza

  gl_ lv0! stimex stimex stimex stimex




  gl_ lv2!
  gl_ lv3!

  gl_ lv1!
  gl_ lv2!

  gl_ lv0!
  gl_ lv1!

  \ stanza

  gl5 lv0! stimex stimex stimex stimex




  gl_ lv2!
  gl_ lv3!

  gl_ lv1!
  gl_ lv2!

  gl_ lv0!
  gl5 lv1!

  \ stanza

  gl2 lv0! stimex stimex stimex stimex




  gl_ lv2!
  gl_ lv3!

  gl_ lv1!
  gl5 lv2!

  gl_ lv0!
  gl2 lv1!

  \ stanza

  gl4 lv0! stimex stimex stimex stimex



  gl_ lv2!
  gl5 lv3!

  gl_ lv1!
  gl2 lv2!

  gl_ lv0!
  gl4 lv1!

  \ stanza

  gl8 lv0! stimex stimex stimex stimex


  gl_ lv2!
  gl2 lv3!

  gl_ lv1!
  gl4 lv2!

  gl_ lv0!
  gl8 lv1!

  \ stanza

  gl1 lv0! stimex stimex stimex stimex


  gl_ lv2!
  gl4 lv3!

  gl_ lv1!
  gl8 lv2!

  gl_ lv0!
  gl1 lv1!

  \ stanza

  gl2 lv0! stimex stimex stimex stimex


  gl_ lv2!
  gl8 lv3!

  gl_ lv1!
  gl1 lv2!

  gl_ lv0!
  gl2 lv1!

  \ stanza

  gl3 lv0! stimex stimex stimex stimex

  gl_ lv2!
  gl1 lv3!

  gl_ lv1!
  gl2 lv2!

  gl_ lv0!
  gl3 lv1!

  \ stanza

  gl_ lv0! stimex stimex stimex stimex

  gl_ lv2!
  gl2 lv3!

  gl_ lv1!
  gl3 lv2!

  gl_ lv0!
  gl_ lv1!

  \ stanza

  gl_ lv0! stimex stimex stimex stimex


  gl_ lv2!
  gl3 lv3!

  gl_ lv1!
  gl_ lv2!

  gl_ lv0!
  gl_ lv1!

  \ stanza

  gl_ lv0! stimex stimex stimex stimex

  gl_ lv2!
  gl_ lv3!

  gl_ lv1!
  gl_ lv2!

  gl_ lv0!
  gl_ lv1!

  \ stanza

  gl_ lv0! stimex stimex stimex stimex

  again

;

: run n867 ;

\ end.
