# Velleman 8090 relay CSharp API GUI
C# code for the Velleman 8090 relay, a GUI and some API

The manufacturer provides a 2008 Visual Basic code example. That is not very useful. This project exposes a C# interface for interacting with the controller.

- Auto detects COM port
- Resets relay on first connect
- Saves internal state of which relays are turned off and on
- On/Off relay
- Toggle relay
- Provide simple relay index from 0-7 (no more byte masking!)

Meant for the 8 relay module, but can be adapted to others.
