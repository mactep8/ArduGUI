#include "ArduGUI.h"

#define DEBUG

uint16_t tArduGUI::TFT2TOUCH_X(uint16_t x){ return map(GUIScreen.screenWidth-(x),0,GUIScreen.screenWidth,0,320); }
uint16_t tArduGUI::TFT2TOUCH_Y(uint16_t x){ return x; }

tArduGUI::tArduGUI(UTFT *ptrUTFT, UTouch *ptrUTouch)
{
	Screen = ptrUTFT;
	myTouch = ptrUTouch;
	LastButton = -1;
	csSDpin = 53;
}

void tArduGUI::InitGUI()
{
	Screen->InitLCD(LANDSCAPE);
	Screen->setFont(SmallFont);
	myTouch->InitTouch(LANDSCAPE);
	myTouch->setPrecision(PREC_MEDIUM);

	GUIScreen.ElementsCount = 0;
	GUIScreen.screenWidth = (uint16_t)Screen->getDisplayXSize();
	GUIScreen.screenHeight = (uint16_t)Screen->getDisplayYSize();
	GUIScreen.screenBkColor = VGA_BLACK;
	GUIScreen.screenFntColor = VGA_WHITE;
	GUIScreen.screenLeft = 0;
	GUIScreen.screenTop = 0;
	GUIScreen.activeBorderColor = VGA_RED;
	GUIScreen.passiveBorderColor = VGA_WHITE;

	pinMode(csSDpin, OUTPUT);
	if (!SD.begin())
#ifdef DEBUG
		Serial.println("SD failed...");
	else Serial.println("SD Ok.");
#else
	;
#endif
}

void tArduGUI::LoadScreenFromFile(char * aFileName, uint16_t aLeft, uint16_t aTop)
{
  scrFile = SD.open(aFileName);
  
  if (scrFile) 
  {
    es=0;
    ea=0;
    LoadScreenSetup(aLeft, aTop);
    LoadElements();
    UpdateActiveElements();
    scrFile.close();
  }
}

void tArduGUI::LoadScreenSetup(uint16_t aLeft, uint16_t aTop)
{
  byte sSize = scrFile.read();
  if (sSize > 1)
  {
    GUIScreen.screenWidth = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.screenWidth = ");Serial.println(GUIScreen.screenWidth);
#endif
  }
  
  if (sSize > 1)
  {
    GUIScreen.screenHeight = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.screenHeight = ");Serial.println(GUIScreen.screenHeight);
#endif
  }
  if (sSize > 1)
  {
    GUIScreen.screenBkColor = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.screenBkColor = ");Serial.println(GUIScreen.screenBkColor);
#endif
  }
  if (sSize > 1)
  {
    GUIScreen.screenFntColor = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.screenFntColor = ");Serial.println(GUIScreen.screenFntColor);
#endif
  }
  
  if (sSize > 1)
  {
    GUIScreen.activeBorderColor = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.activeBorderColor = ");Serial.println(GUIScreen.activeBorderColor);
#endif
  }
  
  if (sSize > 1)
  {
    GUIScreen.passiveBorderColor = ReadTwoBytes();
    sSize -=2;
#ifdef DEBUG
    Serial.print("GUIScreen.passiveBorderColor = ");Serial.println(GUIScreen.passiveBorderColor);
#endif
  }
  
  es = scrFile.read();
  ea = scrFile.read();
  
  GUIScreen.Elements = (tActiveElement *)malloc(sizeof(tActiveElement) * ea);
  GUIScreen.ElementsCount = ea;
  
  uint16_t xmax = Screen->getDisplayXSize();
  uint16_t ymax = Screen->getDisplayYSize();
  GUIScreen.screenLeft = aLeft;
  GUIScreen.screenTop = aTop;
  if (aLeft + GUIScreen.screenWidth > xmax) GUIScreen.screenLeft = xmax-GUIScreen.screenWidth;
  if (aTop + GUIScreen.screenHeight > ymax) GUIScreen.screenTop = ymax-GUIScreen.screenHeight;
  if (GUIScreen.screenLeft==0 && GUIScreen.screenTop==0 && xmax==GUIScreen.screenWidth && ymax==GUIScreen.screenHeight)
  {
    Screen->clrScr();
    Screen->setBackColor(GUIScreen.screenBkColor);
    Screen->setColor(GUIScreen.screenFntColor);
  }
  else
  {
    Screen->setColor(GUIScreen.screenBkColor);
    Screen->drawRoundRect(
      GUIScreen.screenLeft,
      GUIScreen.screenTop,
      GUIScreen.screenLeft + GUIScreen.screenWidth,
      GUIScreen.screenTop + GUIScreen.screenHeight);
    Screen->setColor(GUIScreen.screenFntColor);
  }

  uint8_t dt_img_lng = scrFile.read();
  #ifdef DEBUG
    Serial.print("dt_img_lng = ");Serial.println(dt_img_lng);
  #endif
  char * dt_img;
  if (dt_img_lng > 0)
  {
	  dt_img = (char *)malloc(dt_img_lng + 5);
	  for (int i = 0; i < dt_img_lng; i++)
	  {
		  dt_img[i] = scrFile.read();
	  }
	  dt_img[dt_img_lng] = '.';
	  dt_img[dt_img_lng+1] = 'r';
	  dt_img[dt_img_lng+2] = 'a';
	  dt_img[dt_img_lng+3] = 'w';
	  dt_img[dt_img_lng+4] = '\0';
	  #ifdef DEBUG
	      Serial.print("dt_img = ");Serial.println(dt_img);
	  #endif
	  loadDesctop(GUIScreen.screenLeft, GUIScreen.screenTop, GUIScreen.screenWidth, GUIScreen.screenHeight, dt_img);
	  free(dt_img);
  }
}

uint16_t tArduGUI::ReadTwoBytes()
{
  byte b1 = scrFile.read();
  byte b2 = scrFile.read();
  return (b1<<8)|b2;
}

void tArduGUI::LoadElement()
{
  uint16_t eSize = ReadTwoBytes();
  byte itemtype = scrFile.read();
  byte itemid = scrFile.read();
#ifdef DEBUG
  Serial.print("itemtype ="); Serial.println(itemtype);
  Serial.print("itemid ="); Serial.println(itemid);
#endif
  uint16_t X = ReadTwoBytes() + GUIScreen.screenLeft;
  uint16_t Y = ReadTwoBytes() + GUIScreen.screenTop;
  byte width = scrFile.read();
  byte height = scrFile.read();
  uint16_t bk_color = ReadTwoBytes();
  uint16_t fnt_color = ReadTwoBytes();
  byte CanSelect = scrFile.read();
  byte lng = scrFile.read();
#ifdef DEBUG
  Serial.print("lng ="); Serial.println(lng);
#endif
  char * txt = (char *)malloc(lng+1);
  for (byte i=0;i<lng;i++)
    txt[i] = scrFile.read();
  txt[lng] = '\0';
  if (itemtype == 3)
  {
    Screen->print(txt, X, Y);
    free(txt);
  }
  else
  {
    GUIScreen.Elements[itemid].X = X;
    GUIScreen.Elements[itemid].Y = Y;
    GUIScreen.Elements[itemid].width = width;
    GUIScreen.Elements[itemid].height = height;
    GUIScreen.Elements[itemid].BkColor = bk_color;
    GUIScreen.Elements[itemid].FntColor = fnt_color;
    GUIScreen.Elements[itemid].CanFocus = CanSelect;
    GUIScreen.Elements[itemid].txt_len = lng;
    GUIScreen.Elements[itemid].txt = txt;
  }
  
}

void tArduGUI::LoadElements()
{
  uint16_t i = 0;
  while (i<es+ea)
  {
    LoadElement();
    i++;
  }
}

void tArduGUI::UpdateActiveElements()
{
#ifdef DEBUG
  Serial.println("Drawing Elements...");
#endif
  for(byte i=0;i<GUIScreen.ElementsCount;i++)
    UpdateElement(i);
}

void tArduGUI::UpdateElement(byte indx)
{
  Screen->setBackColor(GUIScreen.Elements[indx].BkColor);
  Screen->setColor(GUIScreen.Elements[indx].FntColor);
  Screen->drawRoundRect(
    GUIScreen.Elements[indx].X,
    GUIScreen.Elements[indx].Y,
    GUIScreen.Elements[indx].X + GUIScreen.Elements[indx].width,
    GUIScreen.Elements[indx].Y + GUIScreen.Elements[indx].height);
  uint8_t fsx = (GUIScreen.Elements[indx].width - Screen->cfont.x_size * GUIScreen.Elements[indx].txt_len)/2;
  uint8_t fsy = (GUIScreen.Elements[indx].height - Screen->cfont.y_size)/2;
  Screen->print(GUIScreen.Elements[indx].txt,GUIScreen.Elements[indx].X + fsx,GUIScreen.Elements[indx].Y + fsy);
}

int tArduGUI::TouchPress()
{
  if (myTouch->dataAvailable())
  {
    myTouch->read();
    x_touch=myTouch->getX();
    y_touch=myTouch->getY();
    LastButton = -1;
    
    for(uint8_t i = 0;i<GUIScreen.ElementsCount;i++)
    {
      if (x_touch<TFT2TOUCH_X(GUIScreen.Elements[i].X) 
        && x_touch>TFT2TOUCH_X(GUIScreen.Elements[i].X+GUIScreen.Elements[i].width) 
        && y_touch>GUIScreen.Elements[i].Y 
        && y_touch<GUIScreen.Elements[i].Y+GUIScreen.Elements[i].height) {
        LastButton = i;
      }
      DrawButtonBorder(i);
    }
    return -1;
  }
  else
  {
    int res = LastButton;
    LastButton = -1;
    return res;
  }
}

void tArduGUI::DrawButtonBorder(uint8_t buttonID)
{
  if (LastButton == buttonID )
  {
    Screen->setColor(GUIScreen.activeBorderColor);
  }
  else
  {
    Screen->setColor(GUIScreen.passiveBorderColor);
  }
  Screen->drawRoundRect(
    GUIScreen.Elements[buttonID].X,
    GUIScreen.Elements[buttonID].Y,
    GUIScreen.Elements[buttonID].X + GUIScreen.Elements[buttonID].width,
    GUIScreen.Elements[buttonID].Y + GUIScreen.Elements[buttonID].height);
}

void tArduGUI::CloseScreen()
{
  for (uint8_t i=0;i<GUIScreen.ElementsCount;i++)
    free(GUIScreen.Elements[i].txt);
  free(GUIScreen.Elements);
}

tActiveElement * tArduGUI::GetElement(uint8_t indx)
{
	return &(GUIScreen.Elements[indx]);
}

void tArduGUI::loadDesctop(int x, int y, int sx, int sy, char *filename)
{
	uint8_t ch, cl;
	uint16_t xxx;
	uint16_t lines, line_pos;
	byte * buf = (byte *)malloc(GUIScreen.screenHeight * 2);
	scrFile = SD.open(filename);
	if (scrFile)
	{
		lines = 0;
		line_pos = 0;
		cbi(Screen->P_CS, Screen->B_CS);

		/*if (Screen->orient==PORTRAIT)
		{
			Screen->setXY(x, y, x+sx-1, y+sy-1);
		}*/
		while (scrFile.available())
		{
			//ch=scrFile.read();
			//cl=scrFile.read();
			scrFile.read(buf, GUIScreen.screenHeight * 2);

			//if (Screen->orient!=PORTRAIT)
			{
				if (line_pos==0) Screen->setXY(x+lines, y, x+lines, y+sy-1);
			}
			for (int pcol=0;pcol<GUIScreen.screenHeight;pcol++)
			{
				xxx = pcol * 2;
				ch = buf[xxx];
				xxx++;
				cl = buf[xxx];
				Screen->LCD_Write_DATA((char)ch, (char)cl);
			}
			lines++;
		}
		scrFile.close();
		Screen->clrXY();
		sbi(Screen->P_CS, Screen->B_CS);
	}
	free(buf);
}