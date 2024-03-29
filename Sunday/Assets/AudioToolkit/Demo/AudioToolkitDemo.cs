using UnityEngine;
using System.Collections;

#pragma warning disable 1591 // undocumented XML code warning

public class AudioToolkitDemo : MonoBehaviour
{
    public AudioClip customAudioClip;

    float musicVolume = 1;
    bool musicPaused = false;

    void OnGUI()
    {
        DrawGuiLeftSide();
        DrawGuiRightSide();
        DrawGuiBottom();
    }

    void DrawGuiLeftSide()
    {
        var headerStyle = new GUIStyle( GUI.skin.label );
        headerStyle.normal.textColor = new UnityEngine.Color( 1, 1, 0.5f );
        GUI.Label( new Rect( 22, 10, 300, 20 ), "ClockStone Audio Toolkit - Demo", headerStyle );

        int ypos = 10;
        int yposOff = 35;
        int buttonWidth = 200;

        ypos += 50;

        GUI.Label( new Rect( 250, ypos, buttonWidth, 30 ), "Global Volume" );

        AudioController.SetGlobalVolume( GUI.HorizontalSlider( new Rect( 250, ypos + 20, buttonWidth, 30 ), AudioController.GetGlobalVolume(), 0, 1 ) );


        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Cross-fade to music track 1" ) )
        {
            AudioController.PlayMusic( "MusicTrack1" );
        }

        ypos += yposOff;

        GUI.Label( new Rect( 250, ypos + 10, buttonWidth, 30 ), "Music Volume" );

        float musicVolumeNew = GUI.HorizontalSlider( new Rect( 250, ypos + 30, buttonWidth, 30 ), musicVolume, 0, 1 );

        if ( musicVolumeNew != musicVolume )
        {
            musicVolume = musicVolumeNew;
            AudioController.SetCategoryVolume( "Music", musicVolume );
        }

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Cross-fade to music track 2" ) )
        {
            AudioController.PlayMusic( "MusicTrack2" );
        }

        ypos += yposOff;

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Stop Music" ) )
        {
            AudioController.StopMusic( 0.3f );
        }

        ypos += yposOff;

        bool musicPausedNew = GUI.Toggle( new Rect( 20, ypos, buttonWidth, 30 ), musicPaused, "Pause Music" );

        if ( musicPausedNew != musicPaused )
        {
            musicPaused = musicPausedNew;

            if ( musicPaused )
            {
                AudioController.PauseMusic();
            }
            else
                AudioController.UnpauseMusic();
        }


        ypos += yposOff;
        ypos += yposOff;

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Play Sound with random pitch" ) )
        {
            AudioController.Play( "RandomPitchSound" );
        }
        ypos += yposOff;

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Play Sound with alternatives" ) )
        {
            AudioObject soundObj = AudioController.Play( "AlternativeSound" );
            if ( soundObj != null ) soundObj.completelyPlayedDelegate = OnAudioCompleteleyPlayed;
        }
        ypos += yposOff;

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Play Both" ) )
        {
            AudioObject soundObj = AudioController.Play( "RandomAndAlternativeSound" );
            if ( soundObj != null ) soundObj.completelyPlayedDelegate = OnAudioCompleteleyPlayed;
        }
        ypos += yposOff;

        ypos += yposOff;

        if ( GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Play Music Playlist" ) )
        {
            AudioController.PlayMusicPlaylist();
        }

        ypos += yposOff;

        if ( AudioController.IsPlaylistPlaying() && GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Next Track on Playlist" ) )
        {
            AudioController.PlayNextMusicOnPlaylist();
        }

        ypos += 32;

        if ( AudioController.IsPlaylistPlaying() && GUI.Button( new Rect( 20, ypos, buttonWidth, 30 ), "Previous Track on Playlist" ) )
        {
            AudioController.PlayPreviousMusicOnPlaylist();
        }

        ypos += yposOff;
        AudioController.Instance.loopPlaylist = GUI.Toggle( new Rect( 20, ypos, buttonWidth, 30 ), AudioController.Instance.loopPlaylist, "Loop Playlist" );
        ypos += yposOff;
        AudioController.Instance.shufflePlaylist = GUI.Toggle( new Rect( 20, ypos, buttonWidth, 30 ), AudioController.Instance.shufflePlaylist, "Shuffle Playlist " );

    }

    bool wasClipAdded = false;
    bool wasCategoryAdded = false;

    void DrawGuiRightSide()
    {
        int ypos = 50;
        int yposOff = 35;
        int buttonWidth = 300;

        if ( !wasCategoryAdded )
        {
            if ( customAudioClip != null && GUI.Button( new Rect( Screen.width - ( buttonWidth + 20 ), ypos, buttonWidth, 30 ), "Create new category with custom AudioClip" ) )
            {
                var category = AudioController.NewCategory( "Custom Category" );
                AudioController.AddToCategory( category, customAudioClip, "CustomAudioItem" );
                wasClipAdded = true;
                wasCategoryAdded = true;
            }
        }
        else
        {
            if ( GUI.Button( new Rect( Screen.width - ( buttonWidth + 20 ), ypos, buttonWidth, 30 ), "Play custom AudioClip" ) )
            {
                AudioController.Play( "CustomAudioItem" );
            }

            if ( wasClipAdded )
            {

                ypos += yposOff;

                if ( GUI.Button( new Rect( Screen.width - ( buttonWidth + 20 ), ypos, buttonWidth, 30 ), "Remove custom AudioClip" ) )
                {
                    if ( AudioController.RemoveAudioItem( "CustomAudioItem" ) )
                    {
                        wasClipAdded = false;
                    }
                }
            }
        }

        ypos = 130;

        if ( GUI.Button( new Rect( Screen.width - ( buttonWidth + 20 ), ypos, buttonWidth, 30 ), "Play gapless audio loop" ) )
        {
            AudioController.Play( "GaplessLoopTest" ).Stop(1,7);
        }

    }

    void DrawGuiBottom()
    {
        if ( GUI.Button( new Rect( Screen.width / 2 - 150, Screen.height - 40, 300, 30 ), "Video tutorial & more info..." ) )
        {
            Application.OpenURL( "http://unity.clockstone.com" );
        }
    }
    void OnAudioCompleteleyPlayed( AudioObject audioObj )
    {
        Debug.Log( "Finished playing " + audioObj.audioID + " with clip " + audioObj.audio.clip.name );
    }
}
