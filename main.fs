\ main.fs
\  Thu  6 Jan 17:55:54 UTC 2022

target
turnkey
    decimal
    initGPIO

: test  ." this is going to be good " ;

: tsecxpj tusec ; \ wait 10 usec   - 10 microseconds
: tsecrrr msec ;  \ wait 1000 usec - 1 millisecond

: timed 10000 #, for tusec next ;

: blinque led on timed  led off timed timed timed ;

: demc $1B #, for blinque next ;

: demob stop 8000 #, ms
       start 2000 #, ms
        stop 8000 #, ms
       start 2000 #, ms
        stop 8000 #, ms
       start 2000 #, ms
       stop
       ." demo complete. "
;

: timex 15000 #, for msec next ;

: demd \ no stack effects
    3 #, for
        7 #, cmd!
        timex
        8 #, cmd!
        timex
    next
;

: deme \ no stack effects
    4  #,  lv0!
    8  #,  lv1!
    16 #,  lv2!
    32 #,  lv3!
    6  #,  cmd!
;

: demf \ no stack effects
;

: demnoneaa

    127 #,  lv0!
      0 #,  lv1!
      0 #,  lv2!
      0 #,  lv3!
      6 #,  cmd!
    timex
      1 #,  cmd!

      0 #,  lv0!
    127 #,  lv1!
      0 #,  lv2!
      0 #,  lv3!
      6 #,  cmd!
    timex
      1 #,  cmd!

      0 #,  lv0!
      0 #,  lv1!
    127 #,  lv2!
      0 #,  lv3!
      6 #,  cmd!
    timex
      1 #,  cmd!

      0 #,  lv0!
      0 #,  lv1!
      0 #,  lv2!
    127 #,  lv3!
      6 #,  cmd!
    timex
      1 #,  cmd!
;

: blank
  gl_ lv3!
  gl_ lv2!
  gl_ lv1!
  gl_ lv0!
;

: demg \ no stack effects
  gl7 lv3!
  gl6 lv2!
  gl5 lv1!
  gl4 lv0!
  6 #, cmd!
;

: noppp 1 #, drop ;

: id ." Thu  6 Jan 17:55:54 UTC 2022" cr
     ." cerfaxita    74hc595 shift register" cr
     ." +fthenc +cmd +timing.cpp" cr \ +fthenc is forth encoding of glyphs
     ." rp2040-sh_regForth-a" cr ;

turnkey decimal initGPIO interpret

\ END.
