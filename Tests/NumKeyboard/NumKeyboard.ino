#include <UTFT.h>
#include <UTouch.h>
#include <SD.h>
#include "Display.h"

// Редактируемое число
uint16_t Value = 0;

void setup()
{
  Serial.begin(9600);
  // инициализация дисплея
  InitDisplay();
  // читаем числовой редактор
  LoadScreenFromFile("NumberKey.scr", 120, 50);
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
