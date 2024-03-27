using Fusion;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;

// should double check since it used to use PUN 

namespace SiaX
{
    public class VideoControlls : NetworkBehaviour
    {
        private VideoPlayer player;

        private Coroutine m_SyncCoroutine = null;

        public bool startPaused;
        private void Start()
        {
            player = GetComponent<VideoPlayer>();

            if (player == null)
                return;

            if (startPaused)
            {
                player.Pause();
            }
            else
            {
                RPC_RequestMasterVideoStateSync();
            }
        }

        private void Update()
        {
            if (HasStateAuthority)
            {
                if (m_SyncCoroutine == null)
                {
                    m_SyncCoroutine = StartCoroutine(SyncVideo());
                }
            }
        }

        IEnumerator SyncVideo()
        {

            RPC_SyncVideo(player.time);
            yield return new WaitForSeconds(3);
            m_SyncCoroutine = null;
        }

        public void PlayPauseControl()
        {
            Debug.Log("should trigger PlayPauseControl");
            if (player.isPlaying)
            {
                RPC_PauseVideo();
            }
            else
            {
                RPC_PlayVideo();
            }
        }

        #region RPC's

        [Rpc]
        void RPC_PlayVideo()
        {
            player.Play();
        }


        [Rpc]
        void RPC_PauseVideo()
        {
            player.Pause();
        }


        [Rpc]
        void RPC_SyncVideo(double _SyncTime)
        {
            double currentTimeDiff = player.time - _SyncTime;

            // If out of sync for more or less than 3 seconds, then sync
            if (currentTimeDiff >= 3 || currentTimeDiff <= -3)
            {
                Debug.Log("Videos out of sync by: " + currentTimeDiff + "seconds, Syncing To Master.");
                player.time = _SyncTime;
            }
        }

        [Rpc]
        void RPC_RequestMasterVideoStateSync()
        {
            if (HasStateAuthority)
            {
                if (!player.isPaused)
                {
                    RPC_PlayVideo();
                }
                else
                {
                    RPC_PauseVideo();
                }
            }
        }

        #endregion
    }
}