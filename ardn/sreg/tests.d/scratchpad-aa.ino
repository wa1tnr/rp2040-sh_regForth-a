// comment

/*
#if 0
const int latchPin = 2;
const int clockPin = 3;
const int dataPin = 4;
byte uleds = 0;
byte pos = 15; // rightmost
#endif
*/

void setup() {
  Serial1.begin(115200);
  Serial1.println("Begin.");
/*
#if 0
  pinMode(latchPin, OUTPUT);
  pinMode(clockPin, OUTPUT);
  pinMode(dataPin,  OUTPUT);
#endif
*/
}

/*
#if 0
void _digitSelect(void) {
    uleds = pos;
    shiftOut(dataPin, clockPin, MSBFIRST, uleds);
}
#endif
*/

int count = -1;
char buffer[8];
int line_reset;

void loop() {
  count++;
  char * buf_ptr = & buffer[0];
  snprintf(buf_ptr, sizeof(buffer), " %02X %s", count, " ");
  Serial1.print(buffer);
  line_reset++;
  if (line_reset > 7) {
    line_reset = 0;
    Serial1.print('\n');
  }
  delay(444);
}


/**********   d o c u m e n t a t i o n   **********/

#if 0

 [ http://www.cplusplus.com/reference/cstdio/snprintf/]

int snprintf ( char * s, size_t n, const char * format, ... );

Write formatted output to sized buffer
Composes a string with the same text that
would be printed if format was used on
printf, but instead of being printed, the
content is stored as a C string in the
buffer pointed by s (taking n as the maximum
buffer capacity to fill).

#endif

#if 0
3V3 is the main 3.3V supply to RP2040 and
its I/O, generated by the on-board SMPS.

This pin can be used to power external circuitry
(maximum output current will depend on
RP2040 load and VSYS voltage,
it is recommended to keep the load on
this pin less than 300mA).
#endif
