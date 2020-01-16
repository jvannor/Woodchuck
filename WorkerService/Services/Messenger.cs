using System;
using System.Collections.Generic;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using UtilityLib.Models;

namespace WorkerService.Services
{
    public class Messenger
    {
        public Messenger(
            string username,
            string password,
            string telephone,
            List<NotificationSettings> subscribers)
        {
            _username = username;
            _password = password;
            _telephone = telephone;
            _subscribers = subscribers;
        }

        public void SendMessage(string message, List<string> attachments)
        {
            if (!string.IsNullOrEmpty(_username) &&
                !string.IsNullOrEmpty(_password) && 
                !string.IsNullOrEmpty(_telephone) &&
                _subscribers != null &&
                _subscribers.Count > 0)
            {
                TwilioClient.Init(_username, _password);
                foreach(var subscriber in _subscribers)
                {
                    var f = new Twilio.Types.PhoneNumber(_telephone);
                    var s = new Twilio.Types.PhoneNumber(subscriber.Telephone);

                    MessageResource header = MessageResource.Create(
                        body: message,
                        mediaUrl: new List<Uri> { new Uri(attachments[0]) },
                        from: f,
                        to: s);
                  }
            }
        }

        private List<Uri> ConvertStringListToUriList(List<string> source)
        {
            List<Uri> destination = new List<Uri>();
            foreach(var s in source)
            {
                destination.Add(new Uri(s));
            }
            return destination;
        }

        private string _username;
        private string _password;
        private string _telephone;
        private List<NotificationSettings> _subscribers;
    }
}
