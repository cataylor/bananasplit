using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;
using BananaSplit.Data;

namespace BananaSplit.Helpers
{
    public class Communication
    {
        public static void SendSmsLoginResponse(Member member)
        {
            // set our AccountSid and AuthToken
            var AccountSid = "AC8bc41784ae75963713052f18956296bd";
            var AuthToken = "da0459307590d485c0e810b49f951de0";
            var fromNumber = "646-783-2676"; 

            // instantiate a new Twilio Rest Client
            var client = new TwilioRestClient(AccountSid, AuthToken);
            // make an associative array of people we know, indexed by phone number
            String[] peopleToReceiveSms = { "310-489-0841", "310-658-9646" };
            //
            // iterate over all our friends
            foreach (var toNumber in peopleToReceiveSms)
            {
                // Send a new outgoing SMS by POSTing to the SMS resource */
                client.SendSmsMessage(
                    fromNumber, // From number, must be an SMS-enabled Twilio number
                    toNumber, // To number, if using Sandbox see note above
                    // message content
                    String.Format("Banana Split has setup your account successfully {0} {1}", member.FirstName, member.LastName)
                );
            }
        }

        //If they don't use SMS go to push notification

        //instructions to manually setup Quartz service
        //Latest GitHub
        //Dlls that I've used
        //

        //smtpout.secureserver.net - info@bananasplit.us - reply-to: welcome@bananasplit.us


        //When a team you've been in a partnership on has a new season added - SMS or notify

        //SETUP HMAILSERVER 
        
        
    }
}