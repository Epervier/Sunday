==============================================================
  Audio Toolkit v5.1 - (c) 2012 by ClockStone Software GmbH
==============================================================

Summary:
--------

Audio Toolkit provides an easy-to-use and performance optimized framework 
to play and manage music and sound effects in Unity.

Features include:

 - ease of use: play audio files with a simple static function call, creation 
    of required AudioSource objects is handled automatically 
 - conveniently define audio assets in categories
 - play audios from within the inspector
 - set properties such as the volume for the entire category
 - change the volume of all playing audio objects within a category at any time
 - define alternative audio clips that get played with a specified 
   probability or order
 - advanced audio pick modes such as "RandomNotSameTwice", "TwoSimultaneously", etc.
 - uses audio object pools for optimized performance particuliarly on mobile devices
 - set audio playing parameters conveniently, such as: 
       + random pitch & volume
       + minimum time difference between play calls
       + delay
       + looping
 - fade out / in 
 - special functions for music including cross-fading 
 - music track playlist management with shuffle, loop, etc.
 - delegate event call if audio was completely played
 - audio event log
 - audio overview window


Package Folders:
----------------

- AudioToolkit: The C# script files of the Audio Toolkit

- Shared Auxiliary Code: additional general purpose script files 
	required by the Audio Toolkit. These files might also be used 
	by other toolkits made by ClockStone available in the Unity Asset 
	Store.

- Reference Documentation file (Windows CHM format)

- Demo: A scene demonstrating the use of the toolkit



Quick Guide:
------------

We recommend to watch the video tutorial on http://unity.clockstone.com

Usage:
 - create a unique GameObject named "AudioController" with the 
   AudioController script component added.
 - Create an prefab named "AudioObject" containing the following components: Unity's AudioSource, 
   the AudioObject script, and the PoolableObject script (if pooling is wanted). 
   Then set your custom AudioSource parameters in this prefab. Next, specify this prefab 
   as the "Audio Object Prefab" in the AudioController.
 - create your audio categories in the AudioController using the Inspector, e.g. "Music", "SFX", etc.
 - for each audio to be played by a script create an 'audio item' with a unique name. 
 - specify any number of audio sub-items (= the AudioClip plus parameters in CLIP mode) 
   within an audio item. 
 - to play an audio item call the static function 
   AudioController.Play( "MyUniqueAudioItemName" )
 - Use AudioController.PlayMusic( "MusicAudioItemName" ) to play music. This function 
   assures that only one music file is played at a time and handles cross fading automatically 
   according to the configuration in the AudioController instance

If you want to access the AudioToolkit from Java Scripts please move the AudioToolkit script files
to a subfolder of either the Assets/Plugins or the Assets/Standard Assets directory. 
See http://docs.unity3d.com/Documentation/ScriptReference/index.Script_compilation_28Advanced29.html for
more infos about script compilation order.


For an up-to-date, detailed documentation please visit: http://unity.clockstone.com


Poolable Audio Objects: 
-----------------------

When audio object pooling is enabled you must be aware of the following:

- If audio object are attached to a parent object e.g. by calling AudioController.Play( audioID, parentTransform ) 
  then the parent object must be destroyed using ObjectPoolController.Destroy(), otherwise the 
  audio object will not be moved back to the pool correctly

- if you save an AudioObject reference and access it later in time, use PoolableReference to make sure the reference
  is not in the pool and still belongs to the original AudioObject

  Example:

  var soundFX = new PoolableReference( AudioController.Play( "someSFX" ) );

  // some other part of the code executed later when the sound may have stopped playing 
  // and was moved back to the pool
  AudioObject audioObject = soundFX.Get();
  if( audioObject.Get() != null )
  {
	// it is safe to access audioObject here
	audioObject.Stop();
  }


Gapless Audio Looping:
----------------------

Please be aware that due to the limitations imposed by Unity's audio engine the gapless loop mode has 
the following implications:

- it consumes some extra performance, use it wisely on mobile platforms
- it consumes etra memory, you can not use audio clips compressed in memory
- make sure that all specified clips have the same number of channels and the same frequency


Building for Flash:
-------------------
Due to the limitations imposed by Unity (current state: Unity v3.5.6) the following features are not 
available when building for Flash:

- (random) pitch changes
- gapless audio looping


Changelog:
----------

v2.2: initial release on asset store 

v2.3:
- playlist with cross fading
- new function PlayPreviousMusicOnPlaylist()

v2.4:
- implementation without default method parameters for MonoDevelop .NET 3.5 compatibility

v2.5:
- several inspector view bugfixes

v2.6 
- new feature: AudioObjectPrefab can be set for each category
- new feature: OverrideClipLength
- new feature: "Add all items to playlist" - button
- inspector view bugfix: changed data not saves correctly 

v3.0
- maximum instance count
- audio log
- audio overview
- new subitem pick modes: random-not-same-twice, sequence, all, two
- subitem modes: CLIP (play audio clip) or ITEM (play audio item)
- reworked GUI design 
- play audio assets from within the Unity inspector

v3.1
- refined GUI design
- new subitem feature: Random Start Position
- new subitem feature: Start / End Position
- bugfix: audio log refreshed correctly

v3.2
- bugfix: inspector null reference errors on audio controllers without categories or items
- playlist can still be resumed after StopMusic() 

v3.3
- new subitem feature: Random Delay, Fade-in, Fade-out
- pooling system: bugfix for poolable object parented to other poolable object
- bugfix: probability not working with 'RandomNotSameTwice'-mode
- inspector: keyboard input focus released when changing items

v3.4
- Flash support
- new AudioController option: Persist Scene Load
- new AudioController functions: GetPlayingAudioObjectsInCategory, PauseAll, UnpauseAll,
  PauseCategory, UnpauseCategory, IsValidAudioID
- add new categories and audio items per script functions: 
  NewCategory, RemoveCategory, AddToCategory
- high precision system time used instead of Unity's game time ( e.g. for fading)
- Object Pooling System: poolable objects parented to poolable objects now correctly handled
- bugfix: music correctly handled when changing scene with none-persistent AudioController

v3.5
- AudioObject.Stop() fades out audio with sub item "Fade-Out" parameter 
- bugfix: FadeIn start volume 

v3.6
- bugfix: inspector changes saved correctly
- no error if pooling is disabled and AudioObject does not have the PoolableObject component 

v4.0
- Unity 4 compatibility
- new function AudioController.RemoveAudioItem

v5.0
- AudioSource override parameters (min/max distance)
- pooling system bugfixes and improvements (new messages)
- new parameter: AudioObject.Stop( float fadeOutLength, float startToFadeTime ) 
- new loop modes: Loop Sequence, Loop Sequence Gapless
- sub / parent categories
- music fade-in/out can be specified separately
- Unity inspector undo working

v5.1
- bugfixes for Flash build
- bugfix: volume change of parent category
- audio item is rename changes playlist accordingly