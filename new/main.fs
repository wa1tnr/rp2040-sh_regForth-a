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

: id ." Tue  4 Jan 18:15:19 UTC 2022" cr
     ." lorkadees    74hc595 shift register" cr
     ." +timing.cpp" cr
     ." rp2040-sh_regForth-a" cr ;

turnkey decimal initGPIO interpret

\ END.
