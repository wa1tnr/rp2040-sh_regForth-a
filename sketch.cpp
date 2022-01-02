// sketch.cpp
// Sun  2 Jan 18:38:07 UTC 2022

#warning sketch.cpp seen

#include <Arduino.h>
#include "memory.h"
#include "compatibility.h"
#include <Wire.h>
#include <Keyboard.h>
#include "rp2040.h" // rp2040.cpp has routines that belong in main .ino file

// Forth registers
uint32_t T=0; // cached top of data stack
uint32_t N=0; // not cached, another stack item
uint16_t I=0; // VM interpreter pointer
uint16_t W=0; // VM working register
uint8_t S=0; // data stack pointer
uint8_t R=0; // return stack pointer
uint16_t P=0; // program memory pointer
uint16_t A=0; // RAM pointer
unsigned long elapsed=0; // for counter timer
uint64_t D=0; // for double result of multiplication
uint8_t FL=0; // flags

// data stack
#define STKSIZE 16
uint32_t stack[STKSIZE];
#define DROP T=stack[--S]
#define DUP stack[S++]=T

// return stack
#define RSTKSIZE 16
uint32_t rstack[RSTKSIZE];
#define PUSH rstack[R++]=T
#define POP T=rstack[--R]

// RAM
#define RAMSIZE 2048
uint8_t ram[RAMSIZE];

// Forth code words

void _off(){
    int pin = T;
    digitalWrite(pin, LOW);
    DROP;
}

void _on(){
    int pin = T;
    digitalWrite(pin, HIGH);
    DROP;
}

void _emit(){
    Serial.write(T);
    DROP;
}

void _key(){
    while(!Serial.available());
    DUP;
    T=Serial.read();
}

void _enter(){
    rstack[R++]=I;
    I=W;
}

void _exit(){
    I=rstack[--R];
}

void _quit(){
    R=0;
    I=memory[0];
}

void _abort(){
    S=0;
    _quit();
}

void _branch(){
    I=memory[I];
}


void _zbranch(){
    if(T==0){
        I=memory[I];
        return;
    }
    I+=1;
}

void _dropzbranch(){
    if(T==0){
        DROP;
        I=memory[I];
        return;
    }
    DROP;
    I+=1;
}

void _pbranch(){
    if(T&0x80000000){
        I+=1;
        return;
    }
    I=memory[I];
}

// read 32 bits inline
void _lit(){
    DUP;
    T=memory[I++]+(memory[I++]<<16);
}

void _next(){
    N=rstack[R-1];
    if(N){
        rstack[R-1]=N-1;
        I=memory[I];
        return;
    }
    R-=1;
    I+=1;
}

void _tor(){
    PUSH;
    DROP;
}

void _rfrom(){
    DUP;
    POP;
}

void _rfetch(){
    DUP;
    T=rstack[R-1];
}

void _variable(){
    DUP;
    T=memory[W];
}

void _dotsh(){
    Serial.print(" ");
    switch(S){
    case 0:
        Serial.print("empty ");
        return;
    case 1:
        Serial.print(T, HEX);
        Serial.print(' ');
        return;
    default:
        for(uint8_t i=1; i<S; i++){
            Serial.print(stack[i], HEX);
            Serial.print(' ');
        }
        Serial.print(T, HEX);
        Serial.print(' ');
    }
}

void _dnumber(){
    DUP;
    T=Serial.parseInt(SKIP_WHITESPACE);
}

void _counter(){
    elapsed=millis();
}

void _timer(){
    Serial.print(millis()-elapsed);
}

void _dup(){
    DUP;
}

void _drop(){
    DROP;
}

void _swap(){
    N=stack[S-1];
    stack[S-1]=T;
    T=N;
}

void _over(){
    stack[S++]=T;
    T=stack[S-2];
}

void _plus(){
    T+=stack[--S];
}

void _minus(){
    T=stack[--S]-T;
}

void _ms(){
    delay(T);
    DROP;
}

void _cr(){
    Serial.println();
}

void _and(){
    T&=stack[--S];
}

void _or(){
    T|=stack[--S];
}

void _xor(){
    T^=stack[--S];
}

void _invert(){
    T^=(-1);
}

void _negate(){
    T^=(-1);
    T+=1;
}

void _abs(){
    if(T&0x80000000) _negate();
}

void _twostar(){
    T=T<<1;
}

void _cfetch(){
    T=ram[T];
}

void _fetch(){
    W=T+3;
    T=ram[W--];
    T=T<<8;
    T+=ram[W--];
    T=T<<8;
    T+=ram[W--];
    T=T<<8;
    T+=ram[W];
}

void _fetchplus(){
    DUP;
    T=ram[A++];
    T+=(ram[A++]<<8);
    T+=(ram[A++]<<16);
    T+=(ram[A++]<<24);
}

void _wfetchplus(){
    DUP;
    T=ram[A++];
    T+=(ram[A++]<<8);
}

void _wfetch(){
    W=T;
    T=ram[W++];
    T+=(ram[W]<<8);
}

void _fetchpplus(){
    DUP;
    T=memory[P++];
}

void _twoslash(){
    T=T>>1;
}

void _a(){
    DUP;
    T=A;
}

void _astore(){
    A=T;
    DROP;
}

void _p(){
    DUP;
    T=P;
}

void _pstore(){
    P=T;
    DROP;
}

void _fetchp(){
    T=memory[T];
}

void _wstoreplus(){
    ram[A++]=(T&0xff);
    ram[A++]=((T>>8)&0xff);
    DROP;
}

void _wstore(){
    W=T;
    DROP;
    ram[W++]=(T&0xff);
    ram[W]=((T>>8)&0xff);
    DROP;
}

void _cstore(){
    W=T;
    DROP;
    ram[W]=T&0xff;
    DROP;
}
    
void _store(){
    W=T;
    DROP;
    ram[W++]=T&0xff;
    T=T>>8;
    ram[W++]=T&0xff;
    T=T>>8;
    ram[W++]=T&0xff;
    T=T>>8;
    ram[W++]=T&0xff;
    DROP;
}

void _cstoreplus(){
    ram[A++]=T&0xff;
    DROP;
}

void _storeplus(){
    ram[W++]=T&0xff;
    ram[W++]=(T>>8)&0xff;
    ram[W++]=(T>>8)&0xff;
    ram[W++]=(T>>8)&0xff;
    DROP;
}

void _depth(){
    DUP;
    T=S-1;
}

void _huh(){
    Serial.write(" ?\n");
    _abort();
}

void _cfetchplus(){
    DUP;
    T=ram[A++];
}

void _umstar(){
    D=T;
    DROP;
    D*=T;
    T=D&0xffffffff;
    DUP;
    T=(D>>32)&0xffffffff;
}

void _umslashmod(){
    N=T;
    DROP;
    D=T;
    D=D<<32;
    DROP;
    D+=T;
    T=D%N;
    DUP;
    T=D/N;
}

void _dnegate(){
    D=T;
    DROP;
    D=D<<32;
    D+=T;
    D=(-D);
    T=D&0xffffffff;
    DUP;
    T=(D>>32)&0xffffffff;
}

void _squote(){
    DUP;
    T=I+1;
    DUP;
    T=memory[I++];
    I+=T;
}

void _nip(){
    S-=1;
}

void _initMCP23017(){
    Wire.begin();
    Wire.beginTransmission(0x20);
    Wire.write(0x0c); // GPPUA
    Wire.write(0xff);
    Wire.write(0xff);
    Wire.endTransmission();
}

// only one port expander in the system
void _fetchMCP23017(){
    DUP;
    Wire.beginTransmission(0x20);
    Wire.write(0x12); // GPIOA
    Wire.endTransmission();
    Wire.requestFrom(0x20, 2);
    T=Wire.read();
    W=Wire.read();
    T|=W<<8;
    T^=0xffff;
    T&=0xffff;
}

#ifdef RP2040_VARIANT
// all the I/O pins needed for the steno keyboard
void _initGPIO(){
    pinMode(LED_BUILTIN, OUTPUT);
    pinMode(9, INPUT_PULLUP);
    pinMode(10, INPUT_PULLUP);
    pinMode(11, INPUT_PULLUP);
    pinMode(12, INPUT_PULLUP);
    pinMode(A1, INPUT_PULLUP);
    pinMode(A2, INPUT_PULLUP);
    pinMode(A3, INPUT_PULLUP);
 // pinMode(A4, INPUT_PULLUP); // is4fse
 // pinMode(A5, INPUT_PULLUP);
}

void _fetchGPIO(){
    DUP;
    T=digitalRead(9);
    T|=digitalRead(10)<<1;
    T|=digitalRead(11)<<2;
    T|=digitalRead(12)<<3;
    T|=digitalRead(A1)<<4;
    T|=digitalRead(A2)<<5;
    T|=digitalRead(A3)<<6;
 // T|=digitalRead(A4)<<7;
 // T|=digitalRead(A5)<<8;
    T^=0x01ff;
}
#endif // #ifdef RP2040_VARIANT

#ifndef RP2040_VARIANT
void _initGPIO(){
    pinMode(LED_BUILTIN, OUTPUT);
    pinMode(9, INPUT_PULLUP);
    pinMode(10, INPUT_PULLUP);
    pinMode(11, INPUT_PULLUP);
    pinMode(12, INPUT_PULLUP);
    pinMode(A1, INPUT_PULLUP);
    pinMode(A2, INPUT_PULLUP);
    pinMode(A3, INPUT_PULLUP);
    pinMode(A4, INPUT_PULLUP);
    pinMode(A5, INPUT_PULLUP);
}

void _fetchGPIO(){
    DUP;
    T=digitalRead(9);
    T|=digitalRead(10)<<1;
    T|=digitalRead(11)<<2;
    T|=digitalRead(12)<<3;
    T|=digitalRead(A1)<<4;
    T|=digitalRead(A2)<<5;
    T|=digitalRead(A3)<<6;
    T|=digitalRead(A4)<<7;
    T|=digitalRead(A5)<<8;
    T^=0x01ff;
}
#endif // #ifndef RP2040_VARIANT

void _lshift(){
    W=T;
    DROP;
    T=T<<W;
}

void _rshift(){
    W=T;
    DROP;
    T=T>>W;
}

void _Keyboard_begin(){
    Keyboard.begin();
}

void _Keyboard_press(){
    Keyboard.press(T);
    DROP;
}

void _Keyboard_release(){
    Keyboard.release(T);
    DROP;
}

void _Keyboard_releaseAll(){
    Keyboard.releaseAll();
}

void _Keyboard_end(){
    Keyboard.end();
}

void _flstore(){
    FL=T;
    DROP;
}

void _flfetch(){
    DUP;
    T=FL;
}

void _cpl() {
    bool state = digitalRead(T);
    state = !state;
    digitalWrite(T, state);
}

void _execute();

void (*function[])()={
    _enter , _exit , _abort , _quit , // 3
    _emit , _key , _lit , // 6
    _branch , _zbranch , _pbranch ,  _next , // 10
    _tor , _rfrom , _rfetch , _variable , // 14
    _dotsh , _dnumber , _counter , _timer , // 18
    _dup , _drop , _swap , _over , _plus , _minus , // 24
    _ms , _cr , _and , _or , _xor ,  // 29
    _invert , _negate , _abs , _twostar ,  _twoslash , // 34
    _cfetch , _fetch , _fetchplus , _fetchpplus , // 38
    _a , _astore , _p , _pstore , // 42
    _wstoreplus , _fetchp , _cstore , // 45
    _store , _cstoreplus , _storeplus , // 48
    _depth , _execute , _huh , _cfetchplus , // 52
    _wfetchplus , _umstar , _umslashmod , // 55
    _wfetch , _wstore , _dnegate , // 58
    _squote , _nip , _initMCP23017 , _fetchMCP23017 , // 62
    _initGPIO , _fetchGPIO , _lshift , _rshift , // 66
    _Keyboard_begin , _Keyboard_press , // 68
    _Keyboard_release , _Keyboard_releaseAll , _Keyboard_end , // 71
    _blink_led , // 72 simple integer count
    _reflashing , // 73
    _on , // 74
    _off , // 75
    _flfetch, // 76
    _flstore, // 77
    _cpl, // 78
    _dropzbranch , // 79
};

void _execute(){
    W=T;
    DROP;
    (*function[memory[W++]])();
}

#define PRE_LAUNCH_T 27000
void blink_core_1(void) {
    if(
        (millis() >= PRE_LAUNCH_T) // initial system delay
      ) {
        if ((millis() - elapsed) > 1000) {
            DUP ; T=LED_BUILTIN ; _cpl(); DROP ;
            elapsed = millis();
        }
    }
}

void noop(void) { }

void time_out_blinker(void) {
    for (volatile uint32_t sh_t = // 7732144;

    // 7732144;
    1732144;

    sh_t > 0; sh_t--) {
        noop();
    }
}

extern void setup_sr(void);

// arduino initialization
void setup(){
    Serial.begin(9600);
    delay(800);
    pinMode(LED_BUILTIN, 1);
    digitalWrite(LED_BUILTIN, LOW); // inverted
    setup_sr();
}

void setup1(){
    delay(1);
    for (volatile int pq = (3 + 2);  pq > 0; pq--) {
        DUP ; T=LED_BUILTIN ; _cpl(); DROP ;
        time_out_blinker();
        DUP ; T=LED_BUILTIN ; _cpl(); DROP ;
        time_out_blinker();
        time_out_blinker();
        time_out_blinker();
    }
    digitalWrite(LED_BUILTIN, LOW);
    time_out_blinker();
}

// arduino main loop
void loop_forth(){
abort:
    S=0;
quit:
    R=0;
    I=memory[0];
next:
    W=memory[I++];
    (*function[memory[W++]])();
    goto next;
}

extern void loop_sr(void);

void loop() {
    loop_sr();
    loop_forth();
}
// second core

/*
#define INHIBIT 172700

void inhibit_blink_awhile(void) {
    if ((millis() - elapsed) > INHIBIT) {
        elapsed = millis();
    }
}
*/

void loop1(){
    if (FL != 179) { // stop blinking
        blink_core_1();
    }
}
