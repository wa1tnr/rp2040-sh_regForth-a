\ main.fs
\  Sun  2 Jan 23:24:31 UTC 2022

target
turnkey
    decimal
    initGPIO

: test  ." this is going to be good " ;

: demob stop 8000 #, ms
       start 2000 #, ms
        stop 8000 #, ms
       start 2000 #, ms
        stop 8000 #, ms
       start 2000 #, ms
       stop
       ." demo complete. "
;

\ parent branch was rp2040-dvlp-dd-multc-a

: id ." Tue  4 Jan 16:15:05 UTC 2022" cr
     ." lorkadees    74hc595 shift register" cr
     ." rp2040-sh_regForth-a" cr ;

turnkey decimal initGPIO interpret

\ END.
