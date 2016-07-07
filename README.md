# SerialPortLogger
A simple Windows application to open a serial port and read data from it.

I've built this program as a simple solution to log data coming from my Arduinos. It can be run as:

```
SerialPortLogger.exe COM4 9600 data.log
```

The current implementation works best if your device is producing new lines for an logged data. This would be a typical output of this application:

```
2016-07-06T21:20:18.381|analogRead=452
2016-07-06T21:20:19.436|analogRead=458
2016-07-06T21:20:12.521|analogRead=456
```

where the data actually being produced by the Arduino would look like:

```
analogRead=452
analogRead=458
analogRead=456
```

The idea here is that you can make your Arduino code simple and leave the time logging to your PC.

I haven't found any ready made solution for that and it looked simpler to just build this tool. The Arduino IDE has the `Serial Monitor` feature, but I didn't find a way for it to add timestamps to the data.

Please, feel free to contribute and send pull requests if you wish. Some of the features I think would be important or nice to have will be listed on the `Issues` tab. Thanks,
