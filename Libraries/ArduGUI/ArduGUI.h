#ifndef AGUI
#define AGUI

#include <SD.h>
#include <UTFT.h>
#include <UTouch.h>

extern uint8_t SmallFont[];

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
  uint16_t activeBorderColor;
  uint16_t passiveBorderColor;
} tGUIScreen;

class tArduGUI
{
	public:
		tArduGUI(UTFT *ptrUTFT, UTouch *ptrUTouch);
		void InitGUI();
		void LoadScreenFromFile(char * aFileName, uint16_t aLeft, uint16_t aTop);
		int TouchPress();
		void DrawButtonBorder(uint8_t buttonID);
		void CloseScreen();
		tActiveElement * GetElement(uint8_t indx);
		void UpdateElement(byte indx);
	protected:
		UTFT *Screen;
		UTouch *myTouch;
		tGUIScreen GUIScreen;
		void loadBitmap(int x, int y, int sx, int sy, char *filename);
	private:
		int LastButton;
		int x_touch, y_touch;
		uint8_t csSDpin;
		File scrFile;
		byte es;
		byte ea;

		void LoadScreenSetup(uint16_t aLeft, uint16_t aTop);
		uint16_t TFT2TOUCH_X(uint16_t x);
		uint16_t TFT2TOUCH_Y(uint16_t x);
		uint16_t ReadTwoBytes();
		void LoadElement();
		void LoadElements();
		void UpdateActiveElements();
};

#endif AGUI