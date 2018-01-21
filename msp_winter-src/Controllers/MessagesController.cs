using System;
using System.Threading.Tasks;
using System.Web.Http;

using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Resource;
using System.Web.Http.Description;
using System.Net.Http;
using System.Diagnostics;



using System.Collections.Generic;

using System.Linq;
using System.Net;



using System.Web.Http.Description;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace Microsoft.Bot.Sample.QnABot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        /// <summary>
        /// POST: api/Messages
        /// receive a message from a user and send replies
        /// </summary>
        /// <param name="activity"></param>
        [ResponseType(typeof(void))]
        
        
        
        
        public virtual async Task<HttpResponseMessage> Post([FromBody] Activity activity)
        {
            // check if activity is of type message
            if (activity.GetActivityType() == ActivityTypes.Message)
            {
                
                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));
                Activity reply = activity.CreateReply();
                
                //第一個跳出來的
                if (activity.Text == "GET_STARTED_PAYLOAD")
                {
                    
                    List<Attachment> att = new List<Attachment>();
            att.Add(new HeroCard() //建立fb ui格式的api
            {
                Title = "我們是微軟學生大使！",
                Subtitle = "來和我們聊聊天吧^_<",
                Images = new List<CardImage>() { new CardImage("https://scontent.ftpe4-2.fna.fbcdn.net/v/t31.0-8/23551020_1710867345610987_5728193079878435145_o.png?oh=0ff1da211976f633a33320d6370ca060&oe=5ADCFB5C") },
                Buttons = new List<CardAction>()
                {
                    new CardAction(){Title = "哈囉！想問一些問題", Type= ActionTypes.ImBack, Value= "哈囉！想問一些問題" },
                    new CardAction(){Title = "來 MSP 的網頁逛逛吧！", Type= ActionTypes.OpenUrl, Value= "https://www.microsoft.com/taiwan/msp/default.aspx" },

                }
            }.ToAttachment());
            reply.Attachments = att;
            
            
            await connector.Conversations.ReplyToActivityAsync(reply);
            

                }
                //三個按鈕
                if (activity.Text == "按按鈕"){
   
                    reply.Text = "請選擇按鈕";
                    reply.SuggestedActions = new SuggestedActions()
                    {
                        Actions = new List<CardAction>()
                            {
                                new CardAction(){Title="哈囉", Type=ActionTypes.ImBack, Value="哈囉"},
                                new CardAction(){Title="哈哈", Type=ActionTypes.ImBack, Value="哈哈"},
                                new CardAction(){Title="講一個笑話", Type=ActionTypes.ImBack, Value="講一個笑話"},
                            }
                    };
                    await connector.Conversations.ReplyToActivityAsync(reply);
                
                }else{
                
                    await Conversation.SendAsync(activity, () => new RootDialog());
                }
                
                
                
               
               
               
            }
            else
            {
                HandleSystemMessage(activity);
            }
            return new HttpResponseMessage(System.Net.HttpStatusCode.Accepted);
        }
        
       
        
        
        
        
        //
        private Activity HandleSystemMessage(Activity message)
        {
            if (message.Type == ActivityTypes.DeleteUserData)
            {
                // Implement user deletion here
                // If we handle user deletion, return a real message
            }
            else if (message.Type == ActivityTypes.ConversationUpdate)
            {
                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels
            }
            else if (message.Type == ActivityTypes.ContactRelationUpdate)
            {
                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened
            }
            else if (message.Type == ActivityTypes.Typing)
            {
                // Handle knowing tha the user is typing
            }
            else if (message.Type == ActivityTypes.Ping)
            {
            }

            return null;
        }
    }
}
