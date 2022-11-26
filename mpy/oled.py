import ssd1306
import time
import random
def showFortune():
    fortuneFileNumber = random.randrange(1, 13)
    # the fortunes are broken into 12 python files
    # to save memory usage (Only one or 2 can be imported in avalable memory)
    # only one of them is imported at random
    if fortuneFileNumber==1 :
        import fortune1 as fortune
    if fortuneFileNumber==2 :
        import fortune2 as fortune
    if fortuneFileNumber==3 :
        import fortune3 as fortune
    if fortuneFileNumber==4 :
        import fortune4 as fortune
    if fortuneFileNumber==5 :
        import fortune5 as fortune
    if fortuneFileNumber==6 :
        import fortune6 as fortune
    if fortuneFileNumber==7 :
        import fortune7 as fortune
    if fortuneFileNumber==8 :
        import fortune8 as fortune
    if fortuneFileNumber==9 :
        import fortune9 as fortune
    if fortuneFileNumber==10 :
        import fortune10 as fortune
    if fortuneFileNumber==11 :
        import fortune11 as fortune
    if fortuneFileNumber==12 :
        import fortune12 as fortune
    from machine import Pin
    from machine import I2C
    # Heltec LoRa 32 with OLED Display
    oled_width = 128
    oled_height = 64
    # OLED reset pin
    i2c_rst = Pin(16, Pin.OUT)
    # Initialize the OLED display
    i2c_rst.value(0)
    time.sleep_ms(5)
    i2c_rst.value(1) # must be held high after initialization
    # Setup the I2C lines
    i2c_scl = Pin(15, Pin.OUT, Pin.PULL_UP)
    i2c_sda = Pin(4, Pin.OUT, Pin.PULL_UP)
    # Create the bus object
    i2c = I2C(scl=i2c_scl, sda=i2c_sda)
    # Create the display object
    oled = ssd1306.SSD1306_I2C(oled_width, oled_height, i2c)
    # Get a random number between 1 and 150, this is used to get one of the
    # fortunes in the fortune file
    fortuneNumber = random.randrange(1, 150)
    # print(fortuneNumber)
    fortuneLines = getattr(fortune, "F" +  str(fortuneNumber))
    # Get the particular fortune by referencing the variable dynamically
    # The fortune is a dictionary item broken into 5 lines
    oled.fill(0)
    oled.text(fortuneLines["L1"], 0, 0)
    oled.text(fortuneLines["L2"], 0, 13)
    oled.text(fortuneLines["L3"], 0, 26)
    oled.text(fortuneLines["L4"], 0, 39)
    oled.text(fortuneLines["L5"], 0, 52)
    oled.show()