using System;
using System.Threading;
using System.Threading.Tasks;
using Bot.Builder.Community.Dialogs.Prompts;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace AdaptiveCardPromptSample.Dialogs
{
    public class DialogAdaptive : Dialog
    {
        private readonly string _dialogId;
        private readonly AdaptiveCardPrompt _adaptiveCardPrompt;
        public DialogAdaptive(string dialogId, AdaptiveCardPrompt prompt) : base(nameof(dialogId))
        {
            _dialogId = dialogId;
            _adaptiveCardPrompt = prompt;
            
        }

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            if (dc == null)
            {
                throw new ArgumentException(nameof(dc));
            }
            
            return await dc.BeginDialogAsync(_adaptiveCardPrompt.Id, null,cancellationToken);
        }

        public override async Task<DialogTurnResult> ResumeDialogAsync(DialogContext dc, DialogReason reason, object result = null,
            CancellationToken cancellationToken = new CancellationToken())
        {
            await base.ResumeDialogAsync(dc, reason, result, cancellationToken);
            return await dc.EndDialogAsync(null, cancellationToken);
        }
    }
}
