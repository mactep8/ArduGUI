
uint16_t ReadTwoBytes()
{
  byte b1 = scrFile.read();
  byte b2 = scrFile.read();
  return (b1<<8)|b2;
}

void InitDisplay()
{
  Screen.InitLCD(LANDSCAPE);
  Screen.setFont(SmallFont);
  myTouch.InitTouch(LANDSCAPE);
  myTouch.setPrecision(PREC_MEDIUM);
  
  pinMode(53, OUTPUT);
  if (!SD.begin()) Serial.println("SD failed...");
  else Serial.println("SD Ok.");
}

void LoadScreenFromFile(char * aFileName, uint16_t aLeft, uint16_t aTop)
{
  scrFile = SD.open(aFileName);
  
  if (scrFile) 
  {
    LoadScreenSetup();
    LoadElements();
    UpdateElements(aLeft, aTop);
    scrFile.close();
  }
}

byte es = 0;
byte ea = 0;

void LoadScreenSetup()
{
  byte sSize = scrFile.read();
  screenWidth = ReadTwoBytes();
  screenHeight = ReadTwoBytes();
  screenBkColor = ReadTwoBytes();
  screenFntColor = ReadTwoBytes();
  es = scrFile.read();
  ea = scrFile.read();
  Elements = (tActiveElement *)malloc(sizeof(tActiveElement) * ea);
  ElementsCount = ea;
  Serial.print("Elements Count ="); Serial.println(ElementsCount);
}

void LoadElement()
{
  uint16_t eSize = ReadTwoBytes();
  byte itemtype = scrFile.read();
  byte itemid = scrFile.read();
  Serial.print("itemtype ="); Serial.println(itemtype);
  Serial.print("itemid ="); Serial.println(itemid);
  uint16_t X = ReadTwoBytes();
  uint16_t Y = ReadTwoBytes();
  byte width = scrFile.read();
  byte height = scrFile.read();
  uint16_t bk_color = ReadTwoBytes();
  uint16_t fnt_color = ReadTwoBytes();
  byte CanSelect = scrFile.read();
  byte lng = scrFile.read();
  char * txt = (char *)malloc(lng+1);
  for (byte i=0;i<lng;i++)
    txt[i] = scrFile.read();
  txt[lng] = '\0';
  if (itemtype == 3)
  {
    Screen.print(txt, X, Y);
    free(txt);
  }
  else
  {
    Elements[itemid].X = X;
    Elements[itemid].Y = Y;
    Elements[itemid].width = width;
    Elements[itemid].height = height;
    Elements[itemid].BkColor = bk_color;
    Elements[itemid].FntColor = fnt_color;
    Elements[itemid].CanFocus = CanSelect;
    Elements[itemid].txt_len = lng;
    Elements[itemid].txt = txt;
    Serial.println("Element stored.");
  }
  
}

void LoadElements()
{
  uint16_t i = 0;
  while (i<es+ea)
  {
    LoadElement();
    i++;
  }
}

void UpdateElements(uint16_t aLeft, uint16_t aTop)
{
  uint16_t xmax = Screen.getDisplayXSize();
  uint16_t ymax = Screen.getDisplayYSize();
  screenLeft = aLeft;
  screenTop = aTop;
  if (aLeft + screenWidth > xmax) screenLeft = xmax-screenWidth;
  if (aTop + screenHeight > ymax) screenTop = ymax-screenHeight;
  if (screenLeft==0 && screenTop==0 && xmax==screenWidth && ymax==screenHeight)
  {
    Screen.clrScr();
    Screen.setBackColor(screenBkColor);
    Screen.setColor(screenFntColor);
  }
  else
  {
    Screen.setColor(screenBkColor);
    Screen.drawRoundRect(
      screenLeft,
      screenTop,
      screenLeft + screenWidth,
      screenTop + screenHeight);
    Screen.setColor(screenFntColor);
  }


  Serial.println("Drawing Elements...");
  for(byte i=0;i<ElementsCount;i++)
    UpdateElement(i);
}

void UpdateElement(byte indx)
{
  Screen.setBackColor(Elements[indx].BkColor);
  Screen.setColor(Elements[indx].FntColor);
  Screen.drawRoundRect(
    Elements[indx].X,
    Elements[indx].Y,
    Elements[indx].X + Elements[indx].width,
    Elements[indx].Y + Elements[indx].height);
  uint8_t fsx = (Elements[indx].width - Screen.cfont.x_size * Elements[indx].txt_len)/2;
  uint8_t fsy = (Elements[indx].height - Screen.cfont.y_size)/2;
  Screen.print(Elements[indx].txt,Elements[indx].X + fsx,Elements[indx].Y + fsy);
}

int TouchPress()
{
  if (myTouch.dataAvailable())
  {
    myTouch.read();
    x_touch=myTouch.getX();
    y_touch=myTouch.getY();
    LastButton = -1;
    
    for(uint8_t i = 0;i<ElementsCount;i++)
    {
      if (x_touch<TFT2TOUCH_X(Elements[i].X) 
        && x_touch>TFT2TOUCH_X(Elements[i].X+Elements[i].width) 
        && y_touch>Elements[i].Y 
        && y_touch<Elements[i].Y+Elements[i].height) {
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

void DrawButtonBorder(uint8_t buttonID)
{
  if (LastButton == buttonID)
  {
    Screen.setColor(VGA_RED);
  }
  else
  {
    Screen.setColor(VGA_WHITE);
  }
  Screen.drawRoundRect(
    Elements[buttonID].X,
    Elements[buttonID].Y,
    Elements[buttonID].X + Elements[buttonID].width,
    Elements[buttonID].Y + Elements[buttonID].height);
}

void CloseScreen()
{
  for (uint8_t i=0;i<ElementsCount;i++)
    free(Elements[i].txt);
  free(Elements);
}
