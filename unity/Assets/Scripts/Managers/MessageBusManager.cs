using NATS.Client;
using NATSTesting.Core;
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NATSTesting.Managers
{
    public class MessageBusManager : MonoBehaviourSingleton<MessageBusManager>
    {
        public Text messages;
        public string defaultNATSUrl = "nats://127.0.0.1:4222";
        private ConnectionFactory connectionFactory;
        private Options connectionOptions;
        private IConnection connection;

        private bool receivingMessage;
        private string message;

        void Start()
        {
            SetupMessageBus();
        }

        private void Initialize()
        {
            connectionFactory = new ConnectionFactory();
            connectionOptions = ConnectionFactory.GetDefaultOptions();
            connectionOptions.Url = defaultNATSUrl;
            connectionOptions.Secure = false;
        }

        private void Update()
        {
            if (receivingMessage)
            {
                messages.text = message;
                receivingMessage = false;
            }
        }

        private bool OpenConnection()
        {
            Initialize();

            try
            {
                connection = connectionFactory.CreateConnection(connectionOptions);
            }
            catch (Exception e)
            {
                messages.text = e.ToString();
                return false;
            }
            return true;
        }

        private void CloseConnection()
        {
            if (connection != null)
            {
                connection.Close();
            }
        }

        private void SetupMessageBus()
        {
            if (OpenConnection())
            {
                IAsyncSubscription subscriber = connection.SubscribeAsync("request_subject", (sender, args) =>
                {
                    Debug.Log(args.Message);
                    message = $"Subject: {args.Message.Subject} | PayLoad: {Encoding.UTF8.GetString(args.Message.Data)}";
                    receivingMessage = true;
                    connection.Publish("unity_subject", Encoding.UTF8.GetBytes("Message to Platform from Unity"));
                });


            }
        }

        void OnApplicationQuit()
        {
            CloseConnection();
            messages.text = string.Empty;
        }
    }

}