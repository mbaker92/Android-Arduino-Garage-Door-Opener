#define ledPin 13
int state = 0;
void setup() {
  pinMode(ledPin, OUTPUT);
  digitalWrite(ledPin, LOW);
  Serial.begin(38400); // Default communication rate of the Bluetooth module
}
void loop() {
  while(Serial.available() > 0){ // Checks whether data is comming from the serial port
    state = Serial.read(); // Reads the data from the serial port
 
 if (state == 0) {
  digitalWrite(ledPin, LOW); // Turn LED OFF
  state = 0;
 }
 else if (state == 1) {
  digitalWrite(ledPin, HIGH);
  state = 0;
  } 
 }
}
