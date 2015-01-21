#include <UTFT.h>
#include <UTouch.h>
#include <ArduGUI.h>
#include "NumEdit.h"

// Редактируемое число
uint16_t Value = 0;

UTFT        Screen(ITDB32WD,38,39,40,41);
UTouch      myTouch(6,5,4,3,2);
tArduGUI    myGUI(&Screen, &myTouch);

void setup()
{
  Serial.begin(9600);
  myGUI.InitGUI();
  // читаем числовой редактор
  myGUI.LoadScreenFromFile("NumEdit.scr", 120, 50);
}

int buttonid = 0;
void loop()
{
  buttonid = myGUI.TouchPress();
  if (buttonid >=0) {
    Serial.print("ID = ");Serial.println(buttonid);
    NumEditButtonUpdate();
    if (!myGUI.GetElement(buttonid)->CanFocus)
      myGUI.DrawButtonBorder(buttonid);
  }
  delay(10);
}


