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
  }
  delay(10);
}

void NumEditButtonUpdate()
{
  switch(buttonid)
  {
    case NUM_VALUE: break;
    case NUM_1: break;
    case NUM_2: break;
    case NUM_3: break;
    case NUM_4: break;
    case NUM_5: break;
    case NUM_6: break;
    case NUM_7: break;
    case NUM_8: break;
    case NUM_9: break;
    case NUM_0: break;
    case NUM_RESET: break;
    case NUM_CANCEL: break;
    case NUM_ENTER: break;
  }
}
