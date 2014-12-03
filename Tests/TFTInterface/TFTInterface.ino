#include "Display.h"

void setup()
{
  Serial.begin(9600);
  InitDisplay();
}

int buttonid = 0;
void loop()
{
  buttonid = TouchPress();
  if (buttonid >=0) {
    Serial.print("ID = ");Serial.println(buttonid);
  }
  delay(10);
}
