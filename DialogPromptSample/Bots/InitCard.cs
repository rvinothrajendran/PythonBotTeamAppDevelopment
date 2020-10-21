using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdaptiveCardPromptSample.Dialogs;
using Microsoft.Bot.Builder;

namespace AdaptiveCardPromptSample.Bots
{
    public class InitCard : AdaptiveCardPromptBot<MainDialog>
    {
        public InitCard(ConversationState conversationState, UserState userState, MainDialog dialog) : base(conversationState, userState, dialog)
        {

        }
    }
}
