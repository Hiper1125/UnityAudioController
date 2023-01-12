# 🔊 Unity Audio Controller

An audio manager written in C#, made for Unity Engine, capable to play 2D and 3D Sounds! 🎵

- If you need help with this project, you can join my discord server by just clicking [here](https://discord.gg/hKFFG2JD9M).
- If you don't have any development knowledge, I suggest you to join my Discord server to get help.*

### 🎼 Features

Here are some of the main features of the AudioController

- Access to it without a reference (AudioManager.`Instance`)
- Place it in your main scene, it won't be destroyed when the scene change.
- It will get rid of multiples AudioListner and it will work as the main audio listener.
- Play a 2D Sound (AudioManager.Instance`.Play2DSound(AudioClip, Volume)`).
- Play a 3D Sound (AudioManager.Instance`.Play3DSound(AudioClip, Position, Volume)`).
- Play a looping Music (AudioManager.Instance`.PlayMusic(AudioClip, Volume)`).
- Stop a looping Music (AudioManager.Instance`.StopMusic(AudioSource)`).
- All the tracks will be played in two separates channel, one for the music and one for the sound effects. The general mixer channel will control both of them, 
- Change volume of a Mixer Channel (AudioManager.Instance`.SetVolume(MixerChannel, Volume)`).

### ⚡ Configuration

No configuration is needed, it's everything ready to use. Just add the namespace in the class where you want to access the AudioManager script. 

```c#
using AudioController;

class MyClass
{
   void MyVoid
   {
      AudioManager.Instance.PlaySound2D(Clip, Volume);
   }
}
```

### 💻 Environement

To run the project correctly you don't need to import any external libraries.

- TextMeshPro is required to run the test scene already built in for you.

# 📑 License
This project run on a standard [MIT](https://github.com/Hiper1125/UnityAudioController/blob/main/LICENSE) license.
Please do not withdraw the license and keep the credits on this project.

# 👤 Authors
Made by [HIPER#1125](https://github.com/Hiper1125) with ❤️
