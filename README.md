# AFPlay

Allows you to play music, for MacOS, through a standard utility,
allows you to use the functionality of afplay and afinfo, and extends them

### Example

```C#
var player = new AFPlayer();
// player.Volume = 50;
player.Play(path);

// extensions:

player.Pause();
player.Resume();
player.Stop();
```
of get info
           
```C#
var player = new AFPlayer();
var info = player.GetInfo(path);
```

### Links

https://ss64.com/osx/afplay.html
