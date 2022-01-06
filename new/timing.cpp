// timing.cpp
// Tue  4 Jan 18:15:19 UTC 2022

#include <Arduino.h>
#include <pico/time.h>

// https://github.com/earlephilhower/arduino-pico/blob/master/cores/rp2040/millis.cpp

#if 0
#include <pico/time.h>

extern "C" unsigned long millis() {
    return to_ms_since_boot(get_absolute_time());
}

extern "C" unsigned long micros() {
    return time_us_32();
}
#endif

void _wait_10_usec(void) {
    unsigned long ut;
    ut = micros();
    while(
        (micros() - ut < 10)
    );
}

void _wait_1000_usec(void) {
    unsigned long mttt;
    mttt = micros();
    while(
        (micros() - mttt < 1000)
    );
}

// END.
