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

: timex 5000 #, for msec next ;

: blank
  gl_ lv3!
  gl_ lv2!
  gl_ lv1!
  gl_ lv0!
;

: msga \ no stack effects
  start
  gl7 lv3!
  gl6 lv2!
  gl5 lv1!
  gl4 lv0!
  6 #, cmd!
;

: msgb \ no stack effects
  start

  gll lv3!
  gle lv2!
  gl_ lv1!
  gl_ lv0!
  6 #, cmd!
  timex
  blank

  glf lv3!
  gl0 lv2!
  glc lv1!
  gla lv0!
  timex
  blank

  glc lv3!
  gla lv2!
  glf lv1!
  gle lv0!
  timex
  blank

  gl7 lv3!
  gl7 lv2!
  gl7 lv1!
  gl7 lv0!
  timex
  blank

  gl8 lv3!
  gl8 lv2!
  gl8 lv1!
  gl8 lv0!
  timex
  blank

  gl0 lv3!
  gl0 lv2!
  gl0 lv1!
  gl0 lv0!
  timex
  blank

  gl4 lv3!
  gl4 lv2!
  gl4 lv1!
  gl4 lv0!
  timex
  blank
;

: noppp 1 #, drop ;

\ rp2040-sh_regForth-a/rp2040-sh_regForth-bb/main.fs

: id ." Wed  2 Feb 15:05:53 UTC 2022" cr
     ." cerfaxita    74hc595 shift register" cr
     ." +fthenc +cmd +timing.cpp" cr \ +fthenc is forth encoding of glyphs
     ." rp2040-sh_regForth-bb" cr ;

turnkey decimal initGPIO interpret

\ END.
