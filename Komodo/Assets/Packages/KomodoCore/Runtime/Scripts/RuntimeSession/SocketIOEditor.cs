using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Komodo.Utilities;
using SocketIOClient;
using SocketIOClient.Newtonsoft.Json;
using UnityEngine;

namespace Komodo.Runtime
{
    public struct KomodoMessageWithMetadata
    {
        public int sessionId;
        public int clientId;
        public string type;
        public string message;
        public DateTime timeStamp;
        
        public KomodoMessageWithMetadata(int sessionId, int clientId, string type, string message, DateTime timeStamp)
        {
            this.sessionId = sessionId;
            this.clientId = clientId;
            this.type = type;
            this.message = message;
            this.timeStamp = timeStamp;
        }
    }
    
    public class SocketIOEditor : SingletonComponent<SocketIOEditor>
    {
    public static SocketIOEditor Instance
    {
        get { return (SocketIOEditor) _Instance; }
        set { _Instance = value; }
    }
    
        public int SUCCESS = 0;

        public int FAILURE = 1;

        public string sessionDetails = @"{""assets"":[{""id"":111550,""name"":""GraceGremer"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/8f5fef97-a735-4c2e-8d28-fc3badfe09a3/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111576,""name"":""GarmentSetup1"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/1843c37c-dc40-4520-91cb-ad1cdc70d72e/model.glb"",""isWholeObject"":false,""scale"":1},{""id"":111577,""name"":""GarmentSetup2"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/cb3464eb-f96e-48c6-b774-e4dfbdc8ab78/model.glb"",""isWholeObject"":false,""scale"":1},{""id"":111578,""name"":""GarmentSetup3"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/e8567627-ade7-493e-a017-e8b1b61e71af/model.glb"",""isWholeObject"":false,""scale"":1},{""id"":111579,""name"":""GarmentSetup4"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/9a133a69-c8ba-4cf8-9539-a9a2b2827226/model.glb"",""isWholeObject"":false,""scale"":1},{""id"":111580,""name"":""GarmentSetup5"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/0529709a-803e-4f61-86d1-092dabf0c2cb/model.glb"",""isWholeObject"":false,""scale"":1},{""id"":111589,""name"":""ShanenHaigler"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/7bb5d74d-39f3-4b3d-aa56-4708b62bda95/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111597,""name"":""Garment O'Donnell"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/ee63a354-b427-4ca3-a2a0-6e230efabe55/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111602,""name"":""GraceGremer"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/14bacabd-7f09-4120-b420-ccfa09b23e03/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111604,""name"":""CarleeIhde"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/de622ac8-eabd-4d3a-ab8f-ab56ee415813/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111605,""name"":""CarleeI"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/29394a64-0e31-4824-86f8-133297f9f84c/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111607,""name"":""G-SarahMiranda"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/f3f1442c-f9cc-454b-8b32-8f3597d79272/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111608,""name"":""SarahMirandaMood"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/0b5036d8-37f0-4c32-b65a-7030cef4718d/model.glb"",""isWholeObject"":true,""scale"":1},{""id"":111609,""name"":""G-ShanenHaigleer"",""url"":""https://s3.us-east-2.amazonaws.com/vrcat-assets/5539b18d-ed67-48cb-8a87-164fc161319a/model.glb"",""isWholeObject"":true,""scale"":1}],""app_and_build"":""/test/Brandon-develop-2021-10-19-15xx/"",""course_id"":3,""create_at"":""2021-03-26T01:00:58.000Z"",""description"":""(No description added)"",""end_time"":""2021-03-31T19:03:00.000Z"",""session_id"":141,""session_name"":""SP21 - Critique Group C"",""start_time"":""2021-03-31T18:03:00.000Z"",""users"":[]}";

        private float[] _arrayPointer;

        private int _relayUpdateSize;

        private int _posCursor;

        public string InstantiationManagerName = "InstantiationManager";

        public string NetworkManagerName = "NetworkManager";

        private NetworkUpdateHandler _NetworkUpdateHandler;

        public SocketIOUnity socket;

        public void OnReceiveStateCatchUp(string jsonStringifiedData)
        {

            Debug.LogError("Need to call SocketIOAdapter.OnReceiveStateCatchup(jsonStringifiedData); here");
        }

        public int SendStateCatchUpRequest()
        {
            socket.Emit("state", "{ session_id: session_id, client_id: client_id }");

            return SocketIOJSLib.SUCCESS;
        }

        public void OnJoined(int clientId)
        {
            ClientSpawnManager.Instance.AddNewClient(clientId);
        }

        public void OnDisconnected(int clientId)
        {
            ClientSpawnManager.Instance.RemoveClient(clientId);
        }

        public void OnMicText(string jsonStringifiedData)
        {
            ClientSpawnManager.Instance.OnReceiveSpeechToTextSnippet(jsonStringifiedData);
        }

        public int SetChatEventListeners()
        {
            //todo(Brandon): call OnMicText with data

            return SocketIOJSLib.SUCCESS;
        }

        public void OnDraw(float[] data)
        {
        }

        public void InitReceiveDraw(float[] arrayPointer, int size)
        {
            // int drawCursor = 0;
            //todo(Brandon): call OnDraw with data and pass in drawCursor also
        }

        public void SendDraw(float[] arrayPointer, int size)
        {
            socket.Emit("draw", arrayPointer.ToString());
        }

        public int GetClientIdFromBrowser()
        {
            return 456;
        }

        public int GetSessionIdFromBrowser()
        {
            return 123;
        }

        public int GetIsTeacherFlagFromBrowser()
        {
            return 1;
        }

        public void SocketIOSendPosition(float[] array, int size)
        {
            socket.Emit("update", array.ToString());
        }

        public void SocketIOSendInteraction(int[] array, int size)
        {
            socket.Emit("interact", array.ToString());
        }

        /**
            InitSocketIOReceivePosition: function(arrayPointer, size) {
                if (sync) {
                    var posCursor = 0;

                    // NOTE(rob):
                    // we use "arrayPointer >> 2" to change the pointer location on the module heap
                    // when interpreted as float32 values ("HEAPF32[]").
                    // for example, an original arrayPointer value (which is a pointer to a
                    // position on the module heap) of 400 would right shift to 100
                    // which would be the correct corresponding index on the heap
                    // for elements of 32-bit size.

                    sync.on('relayUpdate", (data) => {
                        if (data.length + posCursor > size) {
                            posCursor = 0;
                        }
                        for (var i = 0; i < data.length; i++) {
                            HEAPF32[(arrayPointer >> 2) + posCursor + i] = data[i];
                        }
                        posCursor += data.length;
                    });
                }
            },
        */
        public void InitSocketIOReceivePosition(float[] arrayPointer, int size)
        {
            _arrayPointer = arrayPointer;


            _relayUpdateSize = size;
            //  var posCursor = 0;
            //todo(Brandon): call OnRelayUpdate, passing in data, and updating posCursor
        }

        /**
         * See the body of InitSocketIOReceivePosition for the relayUpdate
         * event listener.
         */
        public void RelayPositionUpdate(float[] data)
        {

            if (data.Length + _posCursor > _relayUpdateSize)
            {
                _posCursor = 0;
            }

            for (var i = 0; i < data.Length; i++)
            {
                _arrayPointer[_posCursor + i] = data[i];
            }

            _posCursor += data.Length;
        }

        public void OnInteractionUpdate(float[] data)
        {
        }

        public void InitSocketIOReceiveInteraction(int[] arrayPointer, int size)
        {
            // var intCursor = 0;
            //todo(Brandon): call OnInteractionUpdate, passing in data, and updating intCursor
        }

        public void ToggleCapture(int operation, int session_id)
        {
            if (operation == 0)
            {
                socket.Emit("start_recording", session_id.ToString());
            }
            else
            {
                socket.Emit("end_recording", session_id.ToString());
            }
        }

        /**
            GetSessionDetails: function() {
                if (details) {
                    var serializedDetails = JSON.stringify(details);
                    if (serializedDetails) {
                        var bufferSize = lengthBytesUTF8(serializedDetails) + 1;
                        var buffer = _malloc(bufferSize);
                        stringToUTF8(serializedDetails, buffer, bufferSize);
                        return buffer;
                    } else {
                        console.log("Unable to serialize details: " + details)
                        var bufferSize = lengthBytesUTF8("{}") + 1;
                        var buffer = _malloc(bufferSize);
                        stringToUTF8("", buffer, bufferSize);
                        return buffer;
                    }
                } else {
                    // var bufferSize = lengthBytesUTF8("{details:{}}") + 1;
                    // var buffer = _malloc(bufferSize);
                    // stringToUTF8("", buffer, bufferSize);
                    // return buffer;
                    return null;
                }
            },
        **/
        public string GetSessionDetails()
        {
            //TODO -- extend this with a public static boolean to account for multiple code paths above.

            return sessionDetails;
        }

        public void BrowserEmitMessage(string type, string message)
        {
            KomodoMessageWithMetadata kMessage = new KomodoMessageWithMetadata(123, 456, type, message, DateTime.Now);

            socket.Emit("message", kMessage);
        }

        public int CloseSyncConnection()
        {

            return SocketIOJSLib.SUCCESS;
        }

        public int CloseChatConnection()
        {

            return SocketIOJSLib.SUCCESS;
        }

        public int SetSyncEventListeners()
        {
            socket.OnConnected += (sender, e) =>
            {
                int[] joinIds = { 2, 345 };
                socket.Emit("join", joinIds);
                
                string syncSocketId = e.ToString();
            
                SocketIOAdapter.Instance.OnConnect(syncSocketId);
            };

            socket.OnUnityThread("serverName", (data) =>
            {
                string serverName = data.ToString();
                SocketIOAdapter.Instance.OnServerName(serverName);
            });

            socket.OnDisconnected += (sender, e) =>
            {
                string reason = e.ToString();
                SocketIOAdapter.Instance.OnDisconnect(reason);
            };

            socket.OnError += (sender, e) =>
            {
                SocketIOAdapter.Instance.OnError(e);
            };

        socket.OnUnityThread("connect_error", (data) =>
        {
            string error = data.ToString();
            SocketIOAdapter.Instance
                .OnConnectError(error); //TODO(Brandon): continue changing sendmessage so that it sends to socketIOAdapter instead of networkManager or instantiationManager
        });

        socket.OnUnityThread("connect_timeout", (data) =>
        {
            SocketIOAdapter.Instance.OnConnectTimeout();
        });

        socket.OnReconnected += (sender, e) =>
        {
            SocketIOAdapter.Instance.OnReconnectSucceeded();
        };

        socket.OnReconnectAttempt += (sender, attemptNumber) =>
        {
            string details = attemptNumber.ToString();
            SocketIOAdapter.Instance.OnReconnectAttempt(details);
        };

        // NOTE(rob): If the sync gets disconnected, don't cache the updates.
        // Just purge the sendBuffer and resume the updates from current position. 
        socket.OnReconnectAttempt += (sender, attemptNumber) =>
        {
            //socket.sendBuffer = [];
            SocketIOAdapter.Instance.OnReconnectAttempt(attemptNumber.ToString());
        };

        socket.OnReconnectError += (sender, e) =>
        {
            SocketIOAdapter.Instance.OnReconnectError(e.ToString());
        };

        socket.OnReconnectFailed += (sender, e) =>
        {
            SocketIOAdapter.Instance.OnReconnectFailed();
        };

        socket.OnPing += (sender, e) =>
        {
            SocketIOAdapter.Instance.OnPing();
        };

        socket.OnUnityThread("pong", (data) =>
        {
            int latency = -1;
            
            SocketIOAdapter.Instance.OnPong(latency);
        });

        //Receive session info from the server. Request it with the SendSessionInfoRequest function. Komodo function.
        socket.OnUnityThread("sessionInfo", (data) =>
        {
            string info = data.ToString();

            SocketIOAdapter.Instance.OnSessionInfo(info);
        });

        // Handle when the server gives us a state catch-up event.
        socket.OnUnityThread("state", (data) =>
        {
            string packedData = data.ToString();
            
            SocketIOAdapter.Instance.OnReceiveStateCatchup(packedData);
        });

        // Handle when we are (or someone else is) successfully joined to a session.
        socket.OnUnityThread("joined", (data) =>
        {
            Debug.Log(data.ToString());
            int client_id = data.GetValue<int>();
            
            SocketIOAdapter.Instance.OnClientJoined(client_id);
        });

        // Handle when we successfully joined a session.
        socket.OnUnityThread("successfullyJoined", (data) =>
        {
            int session_id = data.GetValue<int>();

            SocketIOAdapter.Instance.OnOwnClientJoined(session_id);
        });

        // Handle when we failed to join a session.
        socket.OnUnityThread("failedToJoin", (data) =>
        {
            int session_id = data.GetValue<int>();

            SocketIOAdapter.Instance.OnFailedToJoin(session_id);
        });

        // A client other than us left the session.
        socket.OnUnityThread("left", (data) =>
        {
            int client_id = data.GetValue<int>();

            SocketIOAdapter.Instance.OnOtherClientLeft(client_id);
        });

        // We failed to leave the session.
        socket.OnUnityThread("failedToLeave", (data) =>
        {
            int session_id = data.GetValue<int>();
            
            SocketIOAdapter.Instance.OnFailedToLeave(session_id);
        });

        // We successfully left the session.
        socket.OnUnityThread("successfullyLeft", (data) =>
        {
            int session_id = data.GetValue<int>();

            SocketIOAdapter.Instance.OnOwnClientLeft(session_id);
        });

        // A client other than us disconnected
        socket.OnDisconnected += (sender, e) =>
        {
            int client_id = 999;
            
            SocketIOAdapter.Instance.OnOtherClientDisconnected(client_id);
        };

        // Receive messages.
        socket.OnUnityThread("message", (data) =>
        {
            var kMessage = data.GetValue<KomodoMessageWithMetadata>();

            var message = kMessage.message;
            
            var type = kMessage.type;

            var typeAndMessage = type + "|" + message;

            // call the Unity runtime "SendMessage" (unrelated to KomodoMessage stuff) routine to pass data to our "ProcessMessage" routine. 
            SocketIOAdapter.Instance.OnMessage(typeAndMessage);
        });

        // Receive messages.
        socket.OnUnityThread("sendMessageFailed", (data) =>
        {
            string reason = data.GetValue<string>();
            // call the Unity runtime "SendMessage" (unrelated to KomodoMessage stuff) routine to pass data to our "ProcessMessage" routine. 
            SocketIOAdapter.Instance.OnSendMessageFailed(reason);
        });

        // Handle the case where the server forcibly removed the socket.
        socket.OnUnityThread("bump", (data) =>
        {
            int session_id = data.GetValue<int>();
            SocketIOAdapter.Instance.OnBump(session_id);
        });

        socket.OnUnityThread("captureStarted", (data) =>
        {
            Debug.LogError("not implemented");
            // SocketIOAdapter.Instance.OnCaptureStarted();
        });

        // Perform actions when the user closes the tab
        // addEventListener('beforeunload", (data) =>
        // {
        //     // See https://developer.mozilla.org/en-US/docs/Web/API/ventHandlers/onbeforeunload
        //
        //     SocketIOAdapter.Instance.OnTabClosed();
        //
        //     delete e['returnValue'];
        // });

        return SocketIOJSLib.SUCCESS;
    }

public int OpenSyncConnection() {
            //TODO: check the Uri if Valid.
            var uri = new Uri("http://localhost:80/sync");
            socket = new SocketIOUnity(uri, new SocketIOOptions
            {
                Query = new Dictionary<string, string>
                {
                    { "token", "UNITY" }
                },
                EIO = EngineIO.V4,
                Transport = SocketIOClient.Transport.TransportProtocol.WebSocket,
            });
            socket.JsonSerializer = new NewtonsoftJsonSerializer();

            return SocketIOJSLib.SUCCESS;
        }

        public int OpenChatConnection() {

            return SocketIOJSLib.SUCCESS;
        }

        public int EnableVRButton () {

            return SocketIOJSLib.SUCCESS;
        }

        public int JoinSyncSession ()
        {
            socket.Connect();

            return SocketIOJSLib.SUCCESS;
        }

        public int JoinChatSession () {

            return SocketIOJSLib.SUCCESS;
        }

        public int LeaveSyncSession () {

            return SocketIOJSLib.SUCCESS;
        }

        public int LeaveChatSession () {

            return SocketIOJSLib.SUCCESS;
        }

        public string SetSocketIOAdapterName (string name)
        {
            return SocketIOAdapter.Instance.gameObject.name;
        }

        [ContextMenu("Ping Example")]
        public void PingExample () {
            SocketIOAdapter.Instance.OnPing();
        }

        [ContextMenu("Pong Example")]
        public void PongExample () {
            SocketIOAdapter.Instance.OnPong(56789);
        }
    }
}