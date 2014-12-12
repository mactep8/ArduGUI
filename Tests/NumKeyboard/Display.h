#ifndef DISPLAYS
#define DISPLAYS

#define TFT2TOUCH_X(x) map(400-(x),0,400,0,320)
#define TFT2TOUCH_Y(x) x

UTFT        Screen(ITDB32WD,38,39,40,41);
UTouch      myTouch(6,5,4,3,2);
extern uint8_t SmallFont[];

int LastButton = -1;
int x_touch, y_touch;

File scrFile;

typedef struct {
  uint16_t X;
  uint16_t Y;
  uint8_t width;
  uint8_t height;
  uint16_t BkColor;
  uint16_t FntColor;
  uint8_t CanFocus;
  uint8_t txt_len;
  char * txt;
}tActiveElement;

typedef struct {
  tActiveElement * Elements;
  byte ElementsCount;
  uint16_t screenWidth;
  uint16_t screenHeight;
  uint16_t screenBkColor;
  uint16_t screenFntColor;
  uint16_t screenLeft;
  uint16_t screenTop;
  // not in file
  uint16_t activeBorderColor;
  uint16_t passiveBorderColor;
} tGUIScreen;

tGUIScreen GUIScreen;



  
#endif
