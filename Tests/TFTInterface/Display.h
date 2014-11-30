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
#define SCREEN_FILENAME "test.scr"

typedef struct {
  uint16_t X;
  uint16_t Y;
  uint8_t width;
  uint8_t height;
  uint16_t BkColor;
  uint16_t FntColor;
  uint8_t txt_len;
  char * txt;
}tActiveElement;

tActiveElement * Elements;
byte ElementsCount = 0;
#endif
