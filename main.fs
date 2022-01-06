\ main.fs
\  Tue  4 Jan 18:15:19 UTC 2022

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
    lv0!  lv1!  lv2!  lv3!
    6 #, cmd!
    \ timex
    \ 3 #, for timex next
;

: id ." Thu  6 Jan 12:25:56 UTC 2022" cr
     ." lorkadees    74hc595 shift register" cr
     ." +cmd +timing.cpp" cr
     ." rp2040-sh_regForth-a" cr ;

turnkey decimal initGPIO interpret

\ END.
