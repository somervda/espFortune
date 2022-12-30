### ESP32 Fortune Generator

I was making Maker Presents for Christmas, one idea I had was to make a fortune generator that would run on a esp32 microcontroller with a small OLED display. This turned out to be mostly a software project. I wrote some c# code to convert a file of fortunes (Over 2000) into 12 python files that can be loaded on demand. Each python file contained 150 fortunes, each formatted to fit on a 16\*5 OLED display.

![ESP32 Fortune](https://github.com/somervda/espFortune/blob/master/resources/demo.png)
