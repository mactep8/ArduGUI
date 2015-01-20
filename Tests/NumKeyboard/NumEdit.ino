void UpdateValue(uint8_t addVal)
{
  if (Value == 0) Value = addVal;
  else
  {
    Value = Value * 10 + addVal;
  }
}

void NumEditButtonUpdate()
{
  switch(buttonid)
  {
    case NUM_VALUE: break;
    case NUM_1: UpdateValue(1);break;
    case NUM_2: UpdateValue(2);break;
    case NUM_3: UpdateValue(3);break;
    case NUM_4: UpdateValue(4);break;
    case NUM_5: UpdateValue(5);break;
    case NUM_6: UpdateValue(6);break;
    case NUM_7: UpdateValue(7);break;
    case NUM_8: UpdateValue(8);break;
    case NUM_9: UpdateValue(9);break;
    case NUM_0: UpdateValue(0);break;
    case NUM_RESET: {
      Value = 0; 
      Screen.setColor(GUIScreen.Elements[NUM_VALUE].BkColor);
      uint8_t fsx = (GUIScreen.Elements[NUM_VALUE].width - Screen.cfont.x_size * 5)/2;
      uint8_t fsy = (GUIScreen.Elements[NUM_VALUE].height - Screen.cfont.y_size)/2;
      Screen.print("     ",GUIScreen.Elements[NUM_VALUE].X + fsx,GUIScreen.Elements[NUM_VALUE].Y + fsy);
    }break;
    case NUM_CANCEL: {
      Value = Value/10; 
      Screen.setColor(GUIScreen.Elements[NUM_VALUE].BkColor);
      uint8_t fsx = (GUIScreen.Elements[NUM_VALUE].width - Screen.cfont.x_size * 5)/2;
      uint8_t fsy = (GUIScreen.Elements[NUM_VALUE].height - Screen.cfont.y_size)/2;
      Screen.print("     ",GUIScreen.Elements[NUM_VALUE].X + fsx,GUIScreen.Elements[NUM_VALUE].Y + fsy);
    }break;
    case NUM_ENTER: Serial.print("Value = ");Serial.println(Value);break;
  }
//  GUIScreen.Elements[buttonid].txt_len
  UpdateStr();
  UpdateElement(NUM_VALUE);
}

void UpdateStr()
{
  
    uint16_t tmp = Value;
    uint8_t z=0;
    if (Value>0)
    {
      while(tmp>0)
      {
        tmp=tmp/10;
        z++;
      }
    }
    else z=1;

    free(GUIScreen.Elements[NUM_VALUE].txt);
    GUIScreen.Elements[NUM_VALUE].txt = (char *)malloc(z+1);
    String(Value).toCharArray(GUIScreen.Elements[NUM_VALUE].txt, z+1);
    GUIScreen.Elements[NUM_VALUE].txt[z] = '\0';
    GUIScreen.Elements[NUM_VALUE].txt_len = z;
    Serial.print("z = ");Serial.println(z);
    Serial.print("Value = ");Serial.println(Value);
    Serial.print("GUIScreen.Elements[NUM_VALUE].txt = ");Serial.println(GUIScreen.Elements[NUM_VALUE].txt);

}
