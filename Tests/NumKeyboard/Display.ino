
uint16_t ReadTwoBytes()
{
  byte b1 = scrFile.read();
  byte b2 = scrFile.read();
  return (b1<<8)|b2;
}

void InitDisplay()
{
  GUIScreen.ElementsCount = 0;
  GUIScreen.screenWidth = 400;
  GUIScreen.screenHeight = 240;
  GUIScreen.screenBkColor = VGA_BLACK;
  GUIScreen.screenFntColor = VGA_WHITE;
  GUIScreen.screenLeft = 0;
  GUIScreen.screenTop = 0;
  GUIScreen.activeBorderColor = VGA_RED;
  GUIScreen.passiveBorderColor = VGA_WHITE;
  
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
    LoadScreenSetup(aLeft, aTop);
    LoadElements();
    UpdateActiveElements();
    scrFile.close();
  }
}

byte es = 0;
byte ea = 0;

void LoadScreenSetup(uint16_t aLeft, uint16_t aTop)
{
  byte sSize = scrFile.read();
  GUIScreen.screenWidth = ReadTwoBytes();
  GUIScreen.screenHeight = ReadTwoBytes();
  GUIScreen.screenBkColor = ReadTwoBytes();
  GUIScreen.screenFntColor = ReadTwoBytes();
  es = scrFile.read();
  ea = scrFile.read();
  GUIScreen.Elements = (tActiveElement *)malloc(sizeof(tActiveElement) * ea);
  GUIScreen.ElementsCount = ea;
  
  uint16_t xmax = Screen.getDisplayXSize();
  uint16_t ymax = Screen.getDisplayYSize();
  GUIScreen.screenLeft = aLeft;
  GUIScreen.screenTop = aTop;
  if (aLeft + GUIScreen.screenWidth > xmax) GUIScreen.screenLeft = xmax-GUIScreen.screenWidth;
  if (aTop + GUIScreen.screenHeight > ymax) GUIScreen.screenTop = ymax-GUIScreen.screenHeight;
  if (GUIScreen.screenLeft==0 && GUIScreen.screenTop==0 && xmax==GUIScreen.screenWidth && ymax==GUIScreen.screenHeight)
  {
    Screen.clrScr();
    Screen.setBackColor(GUIScreen.screenBkColor);
    Screen.setColor(GUIScreen.screenFntColor);
  }
  else
  {
    Screen.setColor(GUIScreen.screenBkColor);
    Screen.drawRoundRect(
      GUIScreen.screenLeft,
      GUIScreen.screenTop,
      GUIScreen.screenLeft + GUIScreen.screenWidth,
      GUIScreen.screenTop + GUIScreen.screenHeight);
    Screen.setColor(GUIScreen.screenFntColor);
  }
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

void LoadElements()
{
  uint16_t i = 0;
  while (i<es+ea)
  {
    LoadElement();
    i++;
  }
}

void UpdateActiveElements()
{
  Serial.println("Drawing Elements...");
  for(byte i=0;i<GUIScreen.ElementsCount;i++)
    UpdateElement(i);
}

void UpdateElement(byte indx)
{
  Screen.setBackColor(GUIScreen.Elements[indx].BkColor);
  Screen.setColor(GUIScreen.Elements[indx].FntColor);
  Screen.drawRoundRect(
    GUIScreen.Elements[indx].X,
    GUIScreen.Elements[indx].Y,
    GUIScreen.Elements[indx].X + GUIScreen.Elements[indx].width,
    GUIScreen.Elements[indx].Y + GUIScreen.Elements[indx].height);
  uint8_t fsx = (GUIScreen.Elements[indx].width - Screen.cfont.x_size * GUIScreen.Elements[indx].txt_len)/2;
  uint8_t fsy = (GUIScreen.Elements[indx].height - Screen.cfont.y_size)/2;
  Screen.print(GUIScreen.Elements[indx].txt,GUIScreen.Elements[indx].X + fsx,GUIScreen.Elements[indx].Y + fsy);
}

int TouchPress()
{
  if (myTouch.dataAvailable())
  {
    myTouch.read();
    x_touch=myTouch.getX();
    y_touch=myTouch.getY();
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

void DrawButtonBorder(uint8_t buttonID)
{
  if (LastButton == buttonID)
  {
    Screen.setColor(GUIScreen.activeBorderColor);
  }
  else
  {
    Screen.setColor(GUIScreen.passiveBorderColor);
  }
  Screen.drawRoundRect(
    GUIScreen.Elements[buttonID].X,
    GUIScreen.Elements[buttonID].Y,
    GUIScreen.Elements[buttonID].X + GUIScreen.Elements[buttonID].width,
    GUIScreen.Elements[buttonID].Y + GUIScreen.Elements[buttonID].height);
}

void CloseScreen()
{
  for (uint8_t i=0;i<GUIScreen.ElementsCount;i++)
    free(GUIScreen.Elements[i].txt);
  free(GUIScreen.Elements);
}
