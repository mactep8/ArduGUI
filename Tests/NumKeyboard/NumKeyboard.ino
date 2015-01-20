#include <UTFT.h>
#include <UTouch.h>
#include <SD.h>
#include "Display.h"
#include "NumEdit.h"

// Редактируемое число
uint16_t Value = 0;

void setup()
{
  Serial.begin(9600);
  // инициализация дисплея
  InitDisplay();
  // читаем числовой редактор
  LoadScreenFromFile("NumEdit.scr", 120, 50);
}

int buttonid = 0;
void loop()
{
  buttonid = TouchPress();
  if (buttonid >=0) {
    Serial.print("ID = ");Serial.println(buttonid);
    NumEditButtonUpdate();
    if (!GUIScreen.Elements[buttonid].CanFocus)
      DrawButtonBorder(buttonid);
  }
  delay(10);
}


